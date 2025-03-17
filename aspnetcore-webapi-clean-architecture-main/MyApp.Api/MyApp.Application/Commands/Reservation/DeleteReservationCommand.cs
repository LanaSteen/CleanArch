using MediatR;
using MyApp.Application.Commands.Guest;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Reservation
{
    public record DeleteReservationCommand(int ReservationId) : IRequest<bool>;

    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, bool>
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationCommandHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservationEntity = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservationEntity == null) return false;

            return await _reservationRepository.DeleteAsync(reservationEntity);
        }
    }

}
