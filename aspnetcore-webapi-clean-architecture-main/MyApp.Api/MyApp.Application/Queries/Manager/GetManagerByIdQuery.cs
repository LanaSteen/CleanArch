using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Manager;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Manager
{
    public record GetManagerByIdQuery(int Id) : IRequest<ManagerDto>;
    public class GetManagerByIdQueryHandler : IRequestHandler<GetManagerByIdQuery, ManagerDto>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public GetManagerByIdQueryHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<ManagerDto> Handle(GetManagerByIdQuery request, CancellationToken cancellationToken)
        {
            var managerEntity = await _managerRepository.GetByIdAsync(request.Id);
            return _mapper.Map<ManagerDto>(managerEntity);
        }
    }
}
