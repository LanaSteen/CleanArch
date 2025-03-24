using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Manager;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Manager
{
    public record CreateManagerCommand(CreateManagerRequest ManagerRequest) : IRequest<ManagerDto>;

    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, ManagerDto>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public CreateManagerCommandHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<ManagerDto> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var existingManager = await _managerRepository.GetManagerByHotelIdAsync(request.ManagerRequest.HotelId);
            if (existingManager != null)
            {
                throw new InvalidOperationException("This hotel already has a manager.");
            }

            if (await _managerRepository.EmailExistsAsync(request.ManagerRequest.Email))
            {
                throw new ApplicationException("Email is already in use.");
            }

  


            var managerEntity = _mapper.Map<ManagerEntity>(request.ManagerRequest);

            var createdManager = await _managerRepository.AddAsync(managerEntity, request.ManagerRequest.Password);

            return _mapper.Map<ManagerDto>(createdManager);
        }
    }

}
