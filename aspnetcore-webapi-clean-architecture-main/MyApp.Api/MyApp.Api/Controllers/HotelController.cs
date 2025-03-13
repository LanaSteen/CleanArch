
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.Queries;
using MyApp.Core.Entities;
using System;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelController(ISender sender) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateHotelAsync([FromBody] HotelEntity hotel)
        {
            var result = await sender.Send(new CreateHotelCommand(hotel));
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllHotelsAsync()
        {
            var result = await sender.Send(new GetAllHotelsQuery());
            return Ok(result);
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelByIdAsync([FromRoute] int hotelId)
        {
            var result = await sender.Send(new GetHotelByIdQuery(hotelId));
            return Ok(result);
        }

        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotelAsync([FromRoute] int hotelId, [FromBody] HotelEntity hotel)
        {
            var result = await sender.Send(new UpdateHotelCommand(hotelId, hotel));
            return Ok(result);
        }

        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotelAsync([FromRoute] int hotelId)
        {
            var result = await sender.Send(new DeleteHotelCommand(hotelId));
            return Ok(result);
        }
    }
}
