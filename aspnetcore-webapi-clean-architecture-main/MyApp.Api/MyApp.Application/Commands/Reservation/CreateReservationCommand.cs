using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Guest;
using MyApp.Application.DTOs.Reservation;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Reservation
{
    public record CreateReservationCommand(CreateReservationRequest ReservationRequest) : IRequest<ReservationDto>;

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ReservationDto>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public CreateReservationCommandHandler(
            IReservationRepository reservationRepository,
            IGuestRepository guestRepository,
            IHotelRepository hotelRepository,
            IRoomRepository roomRepository,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _guestRepository = guestRepository;
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public async Task<ReservationDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            // Updated GuestId to string
            var guest = await _guestRepository.GetByIdAsync(request.ReservationRequest.GuestId);
            if (guest == null)
            {
                throw new Exception($"Guest with ID {request.ReservationRequest.GuestId} not found.");
            }

            var hotel = await _hotelRepository.GetHotelByIdAsync(request.ReservationRequest.HotelId);
            if (hotel == null)
            {
                throw new Exception($"Hotel with ID {request.ReservationRequest.HotelId} not found.");
            }

            var room = await _roomRepository.GetRoomByIdAsync(request.ReservationRequest.RoomId);
            if (room == null)
            {
                throw new Exception($"Room with ID {request.ReservationRequest.RoomId} not found.");
            }

            if (room.HotelId != request.ReservationRequest.HotelId)
            {
                throw new Exception($"Room with ID {request.ReservationRequest.RoomId} does not belong to the specified Hotel with ID {request.ReservationRequest.HotelId}.");
            }

            var reservationEntity = _mapper.Map<ReservationEntity>(request.ReservationRequest);
            var createdReservation = await _reservationRepository.AddAsync(reservationEntity);

            return _mapper.Map<ReservationDto>(createdReservation);
        }
    }
}