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
        private readonly IHotelRepository _hotelRepository;

        public CreateManagerCommandHandler(IManagerRepository managerRepository, IMapper mapper, IHotelRepository hotelRepository)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
            _hotelRepository = hotelRepository;
        }

        public async Task<ManagerDto> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var existingManager = await _managerRepository.GetManagerByHotelIdAsync(request.ManagerRequest.HotelId);
            if (existingManager != null)
            {
                throw new InvalidOperationException("This hotel already has a manager.");
            }
            var managerEntity = _mapper.Map<ManagerEntity>(request.ManagerRequest);

            var createdManager = await _managerRepository.AddAsync(managerEntity);

            return _mapper.Map<ManagerDto>(createdManager);
        }
    }
}
