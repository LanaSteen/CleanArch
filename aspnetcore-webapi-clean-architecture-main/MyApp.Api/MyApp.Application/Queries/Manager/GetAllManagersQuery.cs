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
    public record GetAllManagersQuery() : IRequest<List<ManagerDto>>;

    public class GetAllManagersQueryHandler : IRequestHandler<GetAllManagersQuery, List<ManagerDto>>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public GetAllManagersQueryHandler(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        public async Task<List<ManagerDto>> Handle(GetAllManagersQuery request, CancellationToken cancellationToken)
        {
            var managers = await _managerRepository.GetAllAsync();
            return _mapper.Map<List<ManagerDto>>(managers);
        }
    }
}
