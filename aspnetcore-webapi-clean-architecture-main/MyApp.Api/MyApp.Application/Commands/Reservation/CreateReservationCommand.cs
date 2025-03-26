using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Reservation;
using MyApp.Application.Exceptions;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Reservation
{
    public record CreateReservationCommand(CreateReservationRequest ReservationRequest) : IRequest<ReservationDto>;

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ReservationDto>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository; // Changed from IGuestRepository
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public CreateReservationCommandHandler(
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

        public async Task<ReservationDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            // Validate guest/user exists
            var guest = await _userRepository.GetByIdAsync(request.ReservationRequest.GuestId);
            if (guest == null)
            {
                throw new NotFoundException("User", request.ReservationRequest.GuestId);
            }

            // Validate hotel exists
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.ReservationRequest.HotelId);
            if (hotel == null)
            {
                throw new NotFoundException("Hotel", request.ReservationRequest.HotelId);
            }

            // Validate room exists
            var room = await _roomRepository.GetRoomByIdAsync(request.ReservationRequest.RoomId);
            if (room == null)
            {
                throw new NotFoundException("Room", request.ReservationRequest.RoomId);
            }

            if (room.HotelId != request.ReservationRequest.HotelId)
            {
                throw new BusinessRuleException($"Room does not belong to the specified hotel");
            }

            // Check room availability
            if (!room.IsAvailable)
            {
                throw new BusinessRuleException("Room is not available for reservation");
            }

            // Validate date range
            if (request.ReservationRequest.CheckInDate >= request.ReservationRequest.CheckOutDate)
            {
                throw new BusinessRuleException("Check-out date must be after check-in date");
            }

            // Check for overlapping reservations
            var hasOverlap = await _reservationRepository.HasOverlappingReservationAsync(
                request.ReservationRequest.RoomId,
                request.ReservationRequest.CheckInDate,
                request.ReservationRequest.CheckOutDate);

            if (hasOverlap)
            {
                throw new BusinessRuleException("Room is already booked for the selected dates");
            }

            var reservationEntity = _mapper.Map<ReservationEntity>(request.ReservationRequest);
            var createdReservation = await _reservationRepository.AddAsync(reservationEntity);

            return _mapper.Map<ReservationDto>(createdReservation);
        }
    }
}