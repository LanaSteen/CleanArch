using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Guest;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Commands.Guest
{
    public record UpdateGuestCommand(string GuestId, UpdateGuestRequest GuestRequest) : IRequest<GuestDto>;

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
            var existingGuest = await _guestRepository.GetByIdAsync(request.GuestId);
            if (existingGuest == null)
            {
                return null; 
            }

            if (!string.IsNullOrEmpty(request.GuestRequest.Email) &&
                request.GuestRequest.Email != existingGuest.Email &&
                await _guestRepository.EmailExistsAsync(request.GuestRequest.Email))
            {
                throw new ApplicationException("Email is already in use.");
            }

            var updates = new Dictionary<string, object>
            {
                { nameof(GuestEntity.FirstName), request.GuestRequest.FirstName },
                { nameof(GuestEntity.LastName), request.GuestRequest.LastName },
                { nameof(GuestEntity.PersonalNumber), request.GuestRequest.PersonalNumber },
                { nameof(GuestEntity.Email), request.GuestRequest.Email },
                { nameof(GuestEntity.PhoneNumber), request.GuestRequest.PhoneNumber }
            };

            var updatedGuest = await _guestRepository.UpdateAsync(request.GuestId, updates);

            return _mapper.Map<GuestDto>(updatedGuest);
        }
    }
}