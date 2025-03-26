using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Room;
using MyApp.Application.DTOs.Room;
using MyApp.Application.Exceptions;
using MyApp.Application.Queries.Room;

namespace MyApp.Api.Controllers
{
    [Route("api/hotel/rooms")]
    [ApiController]
    //[Authorize(Roles = "Admin,Manager")]
    public class RoomController(ISender sender, IMapper mapper) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateRoomAsync([FromBody] CreateRoomRequest roomRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await sender.Send(new CreateRoomCommand(roomRequest));
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoomsAsync()
        {
            var rooms = await sender.Send(new GetAllRoomsQuery());
            var roomsDto = mapper.Map<List<RoomDto>>(rooms);
            return Ok(roomsDto);
        }

  

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomByIdAsync([FromRoute] int roomId)
        {
            var result = await sender.Send(new GetRoomByIdQuery(roomId));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("{roomId}")]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute] int roomId, [FromBody] UpdateRoomRequest roomRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await sender.Send(new UpdateRoomCommand(roomId, roomRequest));
                return result is null ? NotFound() : Ok(result);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoomAsync([FromRoute] int roomId)
        {
            try
            {
                var result = await sender.Send(new DeleteRoomCommand(roomId));
                return result ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
