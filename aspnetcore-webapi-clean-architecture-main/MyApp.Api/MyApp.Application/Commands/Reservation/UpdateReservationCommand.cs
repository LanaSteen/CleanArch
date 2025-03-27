using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Guest;
using MyApp.Application.DTOs.Reservation;
using MyApp.Application.Exceptions;
using MyApp.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Reservation
{
    public record UpdateReservationCommand(int ReservationId, UpdateReservationRequest ReservationRequest)
        : IRequest<ReservationDto>;

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, ReservationDto>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public UpdateReservationCommandHandler(
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            IHotelRepository hotelRepository,
            IRoomRepository roomRepository,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<ReservationDto> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);
                if (reservation == null)
                {
                    throw new NotFoundException("Reservation", request.ReservationId);
                }
           
                var newCheckIn = request.ReservationRequest.CheckInDate ?? reservation.CheckInDate;
                var newCheckOut = request.ReservationRequest.CheckOutDate ?? reservation.CheckOutDate;

                if (newCheckIn >= newCheckOut)
                {
                    throw new BusinessRuleException("Check-out date must be after check-in date");
                }

                var roomIdToCheck = request.ReservationRequest.RoomId ?? reservation.RoomId;
                var hasOverlap = await _reservationRepository.HasOverlappingReservationAsync(
                    roomIdToCheck,
                    newCheckIn,
                    newCheckOut,
                    request.ReservationId); 

                if (hasOverlap)
                {
                    throw new BusinessRuleException("Room is already booked for the selected dates");
                }

                if (request.ReservationRequest.CheckInDate.HasValue)
                    reservation.CheckInDate = request.ReservationRequest.CheckInDate.Value;

                if (request.ReservationRequest.CheckOutDate.HasValue)
                    reservation.CheckOutDate = request.ReservationRequest.CheckOutDate.Value;

                if (request.ReservationRequest.RoomId.HasValue)
                    reservation.RoomId = request.ReservationRequest.RoomId.Value;

                if (request.ReservationRequest.HotelId.HasValue)
                    reservation.HotelId = request.ReservationRequest.HotelId.Value;

                if (request.ReservationRequest.GuestId != null)
                    reservation.GuestId = request.ReservationRequest.GuestId;

                var updatedReservation = await _reservationRepository.UpdateAsync(reservation);
                return _mapper.Map<ReservationDto>(updatedReservation);
            }
            catch (BusinessRuleException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
  
    }
}