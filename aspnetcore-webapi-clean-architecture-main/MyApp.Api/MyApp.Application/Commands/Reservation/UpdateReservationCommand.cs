using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Guest;
using MyApp.Application.DTOs.Guest;
using MyApp.Application.DTOs.Reservation;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Reservation
{
    public record UpdateReservationCommand(int ReservationId, UpdateReservationRequest ReservationRequest) : IRequest<ReservationDto>;
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, ReservationDto>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public UpdateReservationCommandHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ReservationDto> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservationEntity = await _reservationRepository.GetByIdAsync(request.ReservationId);
            if (reservationEntity == null) return null;

            _mapper.Map(request.ReservationRequest, reservationEntity);
            var updatedReservation = await _reservationRepository.UpdateAsync(reservationEntity);
            return _mapper.Map<ReservationDto>(updatedReservation);
        }
    }

}
