using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Hotel;
using MyApp.Application.Queries.Hotel;
using MyApp.Core.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection;
using MyApp.Application.DTOs.Hotel;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagementSystem.Controllers
{
    [Route("api/hotel/hotels")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> CreateHotelAsync([FromBody] CreateHotelRequest hotelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelEntity = _mapper.Map<HotelEntity>(hotelRequest);

            var createdHotel = await _sender.Send(new CreateHotelCommand(hotelEntity));

            var hotelDto = _mapper.Map<HotelDto>(createdHotel);

            return Ok(hotelDto);
        }


        [HttpGet("")]
        public async Task<IActionResult> GetAllHotelsAsync()
        {
            var result = await _sender.Send(new GetAllHotelsQuery());
            return Ok(result);
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelByIdAsync([FromRoute] int hotelId)
        {
            _logger.LogInformation($"GetHotelByIdAsync called with hotelId: {hotelId}");

            var result = await _sender.Send(new GetHotelByIdQuery(hotelId));
            return Ok(result);
        }

        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotelAsync([FromRoute] int hotelId, [FromBody] UpdateHotelRequest updateHotelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedHotelEntity = _mapper.Map<HotelEntity>(updateHotelRequest);

            var result = await _sender.Send(new UpdateHotelCommand(hotelId, updateHotelRequest));

            return Ok(result);
        }
        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotelAsync([FromRoute] int hotelId)
        {
            var message = await _sender.Send(new DeleteHotelCommand(hotelId));

            if (message.StartsWith("Hotel cannot") || message.StartsWith("Hotel not found"))
            {
                return BadRequest(message);
            }

            return Ok(message);
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
