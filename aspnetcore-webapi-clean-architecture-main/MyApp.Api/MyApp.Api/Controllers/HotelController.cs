using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Hotel;
using MyApp.Application.Queries.Hotel;
using MyApp.Core.Entities;
using MyApp.Application.DTOs.Hotel;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using FluentValidation;

namespace HotelManagementSystem.Controllers
{
    [Route("api/hotel/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelController> _logger;

        public HotelController(ISender sender, IMapper mapper, ILogger<HotelController> logger)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("")]
        //[Authorize(Policy = "AdminOnly")] 
        public async Task<IActionResult> CreateHotelAsync([FromBody] CreateHotelRequest hotelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var hotelEntity = _mapper.Map<HotelEntity>(hotelRequest);

                var createdHotel = await _sender.Send(new CreateHotelCommand(hotelEntity));

                var hotelDto = _mapper.Map<HotelDto>(createdHotel);

                return Ok(hotelDto); 
            }
            catch (ValidationException ex)
            {
                _logger.LogError($"Validation failed: {ex.Message}");

                return BadRequest(new { message = $"Validation failed : {ex.Message}", details = ex.Errors });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");

                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllHotelsAsync()
        {
            var result = await _sender.Send(new GetAllHotelsQuery());
            return Ok(result); 
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelByIdAsync([FromRoute] int hotelId)
        {
            _logger.LogInformation($"GetHotelByIdAsync called with hotelId: {hotelId}");
            try
            {
                var result = await _sender.Send(new GetHotelByIdQuery(hotelId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed: {ex.Message}");

                return BadRequest(new { message = $"Failed:", details = ex.Message });
            }
        }

        [HttpPut("{hotelId}")]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateHotelAsync(
      [FromRoute] int hotelId,
      [FromBody] UpdateHotelRequest updateHotelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingHotelDto = await _sender.Send(new GetHotelByIdQuery(hotelId));
                if (existingHotelDto == null)
                {
                    return NotFound();
                }

                var existingHotelEntity = _mapper.Map<HotelEntity>(existingHotelDto);
                _mapper.Map(updateHotelRequest, existingHotelEntity);

                var updatedHotelEntity = await _sender.Send(new UpdateHotelCommand(hotelId, existingHotelEntity));

                var hotelDto = _mapper.Map<HotelDto>(updatedHotelEntity);
                return Ok(hotelDto);
            }
            catch (ValidationException ex)
            {
                _logger.LogError($"Validation failed: {ex.Message}");

                return BadRequest(new { message = $"Validation failed: {ex.Message}", details = ex.Errors });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");

                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        [HttpDelete("{hotelId}")]
        //[Authorize(Policy = "AdminOnly")] 
        public async Task<IActionResult> DeleteHotelAsync([FromRoute] int hotelId)
        {
            var message = await _sender.Send(new DeleteHotelCommand(hotelId));

            
            if (message.StartsWith("Hotel cannot") || message.StartsWith("Hotel not found"))
            {
                return BadRequest(message);
            }
            try
            {
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");

                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
          
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetHotelsByFilterAsync(
            [FromQuery] string? country,
            [FromQuery] string? city,
            [FromQuery] int? minRating,
            [FromQuery] int? maxRating)
        {
            var hotels = await _sender.Send(new GetHotelsByFilterQuery(country, city, minRating, maxRating));
            return Ok(hotels);
        }
    }
}
