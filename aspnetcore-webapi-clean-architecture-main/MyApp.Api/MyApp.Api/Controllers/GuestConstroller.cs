using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Guest;
using MyApp.Application.DTOs.Guest;
using MyApp.Application.Queries;
using MyApp.Application.Queries.Guest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Api.Controllers
{
    [Route("api/hotel/guests")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public GuestController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateGuestAsync([FromBody] CreateGuestRequest guestRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _sender.Send(new CreateGuestCommand(guestRequest));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuestsAsync()
        {
            var guests = await _sender.Send(new GetAllGuestsQuery());
            var guestsDto = _mapper.Map<List<GuestDto>>(guests);
            return Ok(guestsDto);
        }

        [HttpGet("{guestId}")]
        public async Task<IActionResult> GetGuestByIdAsync([FromRoute] string guestId)
        {
            var result = await _sender.Send(new GetGuestByIdQuery(guestId));
            if (result == null)
            {
                return NotFound(new { message = $"Guest with ID {guestId} not found." });
            }
            return Ok(result);
        }

        [HttpPut("{guestId}")]
        public async Task<IActionResult> UpdateGuestAsync([FromRoute] string guestId, [FromBody] UpdateGuestRequest guestRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _sender.Send(new UpdateGuestCommand(guestId, guestRequest));
            return result is null ? NotFound(new { message = $"Guest with ID {guestId} not found." }) : Ok(result);
        }

        [HttpDelete("{guestId}")]
        public async Task<IActionResult> DeleteGuestAsync([FromRoute] string guestId)
        {
            var result = await _sender.Send(new DeleteGuestCommand(guestId));
            return result ? NoContent() : NotFound(new { message = $"Guest with ID {guestId} not found." });
        }
    }
}
