using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Guest;
using MyApp.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Guest
{
    public record GetAllGuestsQuery() : IRequest<List<GuestDto>>;

    public class GetAllGuestsQueryHandler : IRequestHandler<GetAllGuestsQuery, List<GuestDto>>
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public GetAllGuestsQueryHandler(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        public async Task<List<GuestDto>> Handle(GetAllGuestsQuery request, CancellationToken cancellationToken)
        {
            var guests = await _guestRepository.GetAllAsync();
            return guests.Select(_mapper.Map<GuestDto>).ToList();
        }
    }
}
