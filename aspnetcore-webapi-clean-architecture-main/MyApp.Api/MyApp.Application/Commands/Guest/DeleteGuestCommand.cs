using MediatR;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Guest
{
    public record DeleteGuestCommand(string GuestId) : IRequest<bool>;
    public class DeleteGuestCommandHandler : IRequestHandler<DeleteGuestCommand, bool>
    {
        private readonly IGuestRepository _guestRepository;

        public DeleteGuestCommandHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<bool> Handle(DeleteGuestCommand request, CancellationToken cancellationToken)
        {
            var guestEntity = await _guestRepository.GetByIdAsync(request.GuestId); // Expecting string Id
            if (guestEntity == null) return false;

            return await _guestRepository.DeleteAsync(guestEntity);
        }
    }

}
