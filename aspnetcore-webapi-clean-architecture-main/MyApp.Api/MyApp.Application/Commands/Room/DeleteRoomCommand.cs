using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Core.Interfaces;
using MyApp.Application.Exceptions;

namespace MyApp.Application.Commands.Room
{
    public record DeleteRoomCommand(int RoomId) : IRequest<bool>;

    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, bool>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;

        public DeleteRoomCommandHandler(
            IRoomRepository roomRepository,
            IReservationRepository reservationRepository)
        {
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            // First check if room exists
            var roomExists = await _roomRepository.ExistsAsync(request.RoomId);
            if (!roomExists)
            {
                throw new NotFoundException("Room not found");
            }

            // Check for any reservations
            var hasReservations = await _reservationRepository.HasReservationsForRoomAsync(request.RoomId);
            if (hasReservations)
            {
                throw new InvalidOperationException("Cannot delete room with existing reservations");
            }

            // If no reservations, proceed with deletion
            return await _roomRepository.DeleteRoomAsync(request.RoomId);
        }
    }
}
