using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Reservation;
using MyApp.Core.Interfaces;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Reservation
{
    public record GetReservationByIdQuery(int ReservationId) : IRequest<ReservationDto>;

    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDto>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationByIdQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ReservationDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
            return _mapper.Map<ReservationDto>(reservation);
        }
    }
}
