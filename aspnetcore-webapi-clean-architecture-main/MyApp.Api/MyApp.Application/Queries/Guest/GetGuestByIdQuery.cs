using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Guest;
using MyApp.Core.Interfaces;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Guest
{
    public record GetGuestByIdQuery(int GuestId) : IRequest<GuestDto>;

    public class GetGuestByIdQueryHandler : IRequestHandler<GetGuestByIdQuery, GuestDto>
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public GetGuestByIdQueryHandler(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        public async Task<GuestDto> Handle(GetGuestByIdQuery request, CancellationToken cancellationToken)
        {
            var guest = await _guestRepository.GetByIdAsync(request.GuestId);
            return _mapper.Map<GuestDto>(guest);
        }
    }
}
