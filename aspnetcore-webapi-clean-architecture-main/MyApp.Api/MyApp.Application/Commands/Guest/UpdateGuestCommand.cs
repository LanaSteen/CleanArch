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
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly IPasswordHasher _passwordHasher;

            public UpdateGuestCommandHandler(
                IGuestRepository guestRepository,
                IUserRepository userRepository,
                IMapper mapper,
                IPasswordHasher passwordHasher)
            {
                _guestRepository = guestRepository;
                _userRepository = userRepository;
                _mapper = mapper;
                _passwordHasher = passwordHasher;
            }

        public async Task<GuestDto> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var existingGuest = await _guestRepository.GetByIdAsync(request.GuestId);
            if (existingGuest == null)
            {
                return null;
            }

            var user = await _guestRepository.GetUserByGuestIdAsync(request.GuestId);
            if (user == null)
            {
                throw new ApplicationException("Associated user account not found");
            }

            if (!string.IsNullOrEmpty(request.GuestRequest.Email) &&
                request.GuestRequest.Email != existingGuest.Email)
            {
                if (await _guestRepository.EmailExistsAsync(request.GuestRequest.Email))
                {
                    throw new ApplicationException("Email is already in use.");
                }

                existingGuest.Email = request.GuestRequest.Email;
                user.Email = request.GuestRequest.Email;
            }

            if (!string.IsNullOrEmpty(request.GuestRequest.Password))
            {
                var hashedPassword = _passwordHasher.HashPassword(request.GuestRequest.Password);
                existingGuest.PasswordHash = hashedPassword;
                user.PasswordHash = hashedPassword;
            }

            var updates = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(request.GuestRequest.FirstName))
                updates.Add(nameof(GuestEntity.FirstName), request.GuestRequest.FirstName);

            if (!string.IsNullOrEmpty(request.GuestRequest.LastName))
                updates.Add(nameof(GuestEntity.LastName), request.GuestRequest.LastName);

            if (!string.IsNullOrEmpty(request.GuestRequest.PersonalNumber))
                updates.Add(nameof(GuestEntity.PersonalNumber), request.GuestRequest.PersonalNumber);

            if (!string.IsNullOrEmpty(request.GuestRequest.PhoneNumber))
                updates.Add(nameof(GuestEntity.PhoneNumber), request.GuestRequest.PhoneNumber);

            var updatedGuest = await _guestRepository.UpdateAsync(request.GuestId, updates);

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<GuestDto>(updatedGuest);
        }

    }
}