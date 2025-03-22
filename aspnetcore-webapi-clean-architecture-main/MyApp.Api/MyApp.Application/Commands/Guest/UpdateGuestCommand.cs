using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Guest;
using MyApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Core.Entities;

namespace MyApp.Application.Commands.Guest
{
    public record UpdateGuestCommand(string GuestId, UpdateGuestRequest GuestRequest) : IRequest<GuestDto>;

    public class UpdateGuestCommandHandler : IRequestHandler<UpdateGuestCommand, GuestDto>
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager; 

        public UpdateGuestCommandHandler(IGuestRepository guestRepository, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
            _userManager = userManager; 
        }

        public async Task<GuestDto> Handle(UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestEntity = await _guestRepository.GetByIdAsync(request.GuestId);
            if (guestEntity == null) return null;

            var updates = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(request.GuestRequest.FirstName))
                updates["FirstName"] = request.GuestRequest.FirstName;

            if (!string.IsNullOrEmpty(request.GuestRequest.LastName))
                updates["LastName"] = request.GuestRequest.LastName;

            if (!string.IsNullOrEmpty(request.GuestRequest.PersonalNumber))
                updates["PersonalNumber"] = request.GuestRequest.PersonalNumber;

            if (!string.IsNullOrEmpty(request.GuestRequest.PhoneNumber))
                updates["PhoneNumber"] = request.GuestRequest.PhoneNumber;

            if (!string.IsNullOrEmpty(request.GuestRequest.Email))
                updates["Email"] = request.GuestRequest.Email;

            if (!string.IsNullOrEmpty(request.GuestRequest.Password))
            {
                var userEntity = await _guestRepository.GetByIdAsync(request.GuestId);
                if (userEntity == null) return null;

                var hashedPassword = _userManager.PasswordHasher.HashPassword(userEntity, request.GuestRequest.Password);  // Use HashPassword, not HashPasswordAsync
                updates["PasswordHash"] = hashedPassword; 
            }

      
            var updatedGuest = await _guestRepository.UpdateAsync(request.GuestId, updates);
            return _mapper.Map<GuestDto>(updatedGuest);
        }
    }
}
