using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Guest;
using MyApp.Application.DTOs.Guest;
using MyApp.Application.Exceptions;
using MyApp.Application.Queries.Guest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Api.Controllers
{
    [Route("api/hotel/[controller]")]
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _sender.Send(new CreateGuestCommand(guestRequest));
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("All")]

        //[Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAllGuestsAsync()
        {
            var guests = await _sender.Send(new GetAllGuestsQuery());
            var guestsDto = _mapper.Map<List<GuestDto>>(guests);
            return Ok(guestsDto);
        }

        [HttpGet("{guestId}")]
        //[Authorize(Roles = "Admin,Manager")]
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
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateGuestAsync([FromRoute] string guestId, [FromBody] UpdateGuestRequest guestRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _sender.Send(new UpdateGuestCommand(guestId, guestRequest));
                return result is null ? NotFound(new { message = $"Guest with ID {guestId} not found." }) : Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{guestId}")]
        //[Authorize(Roles = "Admin,Manager")
     
        public async Task<IActionResult> DeleteGuest(string guestId)
        {
            try
            {
                var result = await _sender.Send(new DeleteGuestCommand(guestId));
                return result ? NoContent() : NotFound();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (BusinessRuleException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }
    }
}