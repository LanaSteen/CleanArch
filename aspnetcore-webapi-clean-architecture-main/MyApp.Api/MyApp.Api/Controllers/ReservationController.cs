using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Reservation;
using MyApp.Application.DTOs.Reservation;
using MyApp.Application.Queries.Reservation;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace MyApp.Api.Controllers
{
    [Route("api/hotel/reservations")]
    [ApiController]
    //[Authorize(Policy = "GuestOnly")]
    [Authorize(Roles = "Admin,Manager,Guest")]
    public class ReservationController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ReservationController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync([FromBody] CreateReservationRequest reservationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _sender.Send(new CreateReservationCommand(reservationRequest));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var reservations = await _sender.Send(new GetAllReservationsQuery());
            var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);
            return Ok(reservationsDto);
        }

        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetReservationByIdAsync([FromRoute] int reservationId)
        {
            var result = await _sender.Send(new GetReservationByIdQuery(reservationId));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("{reservationId}")]
        public async Task<IActionResult> UpdateReservationAsync([FromRoute] int reservationId, [FromBody] UpdateReservationRequest reservationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _sender.Send(new UpdateReservationCommand(reservationId, reservationRequest));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> DeleteReservationAsync([FromRoute] int reservationId)
        {
            var result = await _sender.Send(new DeleteReservationCommand(reservationId));
            return result ? NoContent() : NotFound();
        }
    }
}
