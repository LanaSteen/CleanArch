using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Guest;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Guest
{
    public record CreateGuestCommand(CreateGuestRequest GuestRequest) : IRequest<GuestDto>;
    public class CreateGuestCommandHandler : IRequestHandler<CreateGuestCommand, GuestDto>
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public CreateGuestCommandHandler(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        public async Task<GuestDto> Handle(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestEntity = _mapper.Map<GuestEntity>(request.GuestRequest);
            var createdGuest = await _guestRepository.AddAsync(guestEntity);
            return _mapper.Map<GuestDto>(createdGuest);
        }
    }

}
