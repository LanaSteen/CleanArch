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

            if (!string.IsNullOrEmpty(request.ManagerRequest.Email) &&
                request.ManagerRequest.Email != managerEntity.Email &&
                await _managerRepository.EmailExistsAsync(request.ManagerRequest.Email))
            {
                throw new ApplicationException("Email is already in use.");
            }

            _mapper.Map(request.ManagerRequest, managerEntity);

            var updatedManager = await _managerRepository.UpdateAsync(managerEntity);

            return _mapper.Map<ManagerDto>(updatedManager);
        }
    }
}
