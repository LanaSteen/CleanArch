using MediatR;
using MyApp.Application.Exceptions;
using MyApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Guest
{
    public record DeleteGuestCommand(string GuestId) : IRequest<bool>;

    public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand, bool>
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IReservationRepository _reservationRepository;

        public DeleteGuestCommandHandler(
            IGuestRepository guestRepository,
            IReservationRepository reservationRepository)
        {
            _guestRepository = guestRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            var guestEntity = await _guestRepository.GetByIdAsync(request.GuestId);
            if (guestEntity == null)
            {
                throw new NotFoundException($"Guest with ID {request.GuestId} not found.");
            }

            var hasReservations = await _reservationRepository.HasReservationsForGuestAsync(request.GuestId);
            if (hasReservations)
            {
                throw new BusinessRuleException("Cannot delete guest with existing reservations. Please cancel all reservations first.");
            }

            return await _guestRepository.DeleteAsync(guestEntity);
        }
    }
}