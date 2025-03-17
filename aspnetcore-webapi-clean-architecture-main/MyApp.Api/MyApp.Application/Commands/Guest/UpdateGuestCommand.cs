using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Guest;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Guest
{
    public record UpdateGuestCommand(int GuestId, UpdateGuestRequest GuestRequest) : IRequest<GuestDto>;
    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, GuestDto>
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public UpdateGuestCommandHandler(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        public async Task<GuestDto> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestEntity = await _guestRepository.GetByIdAsync(request.GuestId);
            if (guestEntity == null) return null;

            _mapper.Map(request.GuestRequest, guestEntity);
            var updatedGuest = await _guestRepository.UpdateAsync(guestEntity);
            return _mapper.Map<GuestDto>(updatedGuest);
        }
    }

}
