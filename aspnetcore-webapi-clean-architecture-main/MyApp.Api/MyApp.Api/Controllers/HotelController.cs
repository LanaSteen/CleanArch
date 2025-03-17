using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.Queries;
using MyApp.Core.Entities;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace HotelManagementSystem.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelController> _logger;

        // Constructor with dependency injection
        public HotelController(ISender sender, IMapper mapper, ILogger<HotelController> logger)
        {
            _sender = sender;
            _mapper = mapper;
            _logger = logger;
        }

        // POST api/hotels
        [HttpPost("")]
        public async Task<IActionResult> CreateHotelAsync([FromBody] CreateHotelRequest hotelRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Convert request DTO to entity using AutoMapper
            var hotelEntity = _mapper.Map<HotelEntity>(hotelRequest);

            // Send command to create hotel
            var createdHotel = await _sender.Send(new CreateHotelCommand(hotelEntity));

            // Convert entity to DTO before returning
            var hotelDto = _mapper.Map<HotelDto>(createdHotel);

            // Return Ok instead of CreatedAtAction
            return Ok(hotelDto);
        }


        // GET api/hotels
        [HttpGet("")]
        public async Task<IActionResult> GetAllHotelsAsync()
        {
            var result = await _sender.Send(new GetAllHotelsQuery());
            return Ok(result);
        }

        // GET api/hotels/{hotelId}
        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelByIdAsync([FromRoute] int hotelId)
        {
            // Debugging: Log the hotelId to verify it's being passed correctly
            _logger.LogInformation($"GetHotelByIdAsync called with hotelId: {hotelId}");

            var result = await _sender.Send(new GetHotelByIdQuery(hotelId));
            return Ok(result);
        }

        // PUT api/hotels/{hotelId}
        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotelAsync([FromRoute] int hotelId, [FromBody] HotelEntity hotel)
        {
            var result = await _sender.Send(new UpdateHotelCommand(hotelId, hotel));
            return Ok(result);
        }

        // DELETE api/hotels/{hotelId}
        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotelAsync([FromRoute] int hotelId)
        {
            var result = await _sender.Send(new DeleteHotelCommand(hotelId));
            return Ok(result);
        }
    }
}
