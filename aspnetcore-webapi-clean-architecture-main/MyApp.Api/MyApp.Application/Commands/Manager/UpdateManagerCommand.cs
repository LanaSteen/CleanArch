using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Manager;
using MyApp.Application.Exceptions;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Manager
{
    public record UpdateManagerCommand(int Id, UpdateManagerRequest ManagerRequest) : IRequest<ManagerDto>;

    public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand, ManagerDto>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public UpdateManagerCommandHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<ManagerDto> Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
        {
            var managerEntity = await _managerRepository.GetByIdAsync(request.Id);
            if (managerEntity == null)
            {
                throw new NotFoundException("Manager not found.");
            }

            var user = await _managerRepository.GetUserByManagerIdAsync(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User account not found for this manager.");
            }

            if (!string.IsNullOrEmpty(request.ManagerRequest.Email) &&
                request.ManagerRequest.Email != managerEntity.Email)
            {
                if (await _managerRepository.EmailExistsAsync(request.ManagerRequest.Email))
                {
                    throw new ApplicationException("Email is already in use.");
                }

                managerEntity.Email = request.ManagerRequest.Email;
                user.Email = request.ManagerRequest.Email;
            }

            if (!string.IsNullOrEmpty(request.ManagerRequest.FirstName))
                managerEntity.FirstName = request.ManagerRequest.FirstName;

            if (!string.IsNullOrEmpty(request.ManagerRequest.LastName))
                managerEntity.LastName = request.ManagerRequest.LastName;

            if (!string.IsNullOrEmpty(request.ManagerRequest.PersonalNumber))
                managerEntity.PersonalNumber = request.ManagerRequest.PersonalNumber;

            if (request.ManagerRequest.HotelId.HasValue)
                managerEntity.HotelId = request.ManagerRequest.HotelId.Value;

            if (!string.IsNullOrEmpty(request.ManagerRequest.Password))
            {
                var newPasswordHash = request.ManagerRequest.Password;
                managerEntity.PasswordHash = newPasswordHash;
                user.PasswordHash = newPasswordHash;
            }

            var updatedManager = await _managerRepository.UpdateAsync(managerEntity);
            await _managerRepository.UpdateUserAsync(user);

            return _mapper.Map<ManagerDto>(updatedManager);
        }
    }
}
