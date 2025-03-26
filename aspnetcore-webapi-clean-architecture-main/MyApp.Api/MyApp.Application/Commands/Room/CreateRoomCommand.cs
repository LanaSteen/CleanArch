using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Room;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Application.Exceptions;

namespace MyApp.Application.Commands.Room
{
    public record CreateRoomCommand(CreateRoomRequest RoomRequest) : IRequest<RoomDto>;

    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomDto>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(
            IRoomRepository roomRepository,
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<RoomDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.RoomRequest.HotelId);
            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with ID {request.RoomRequest.HotelId} not found.");
            }

            var roomEntity = _mapper.Map<RoomEntity>(request.RoomRequest);
            var createdRoom = await _roomRepository.CreateRoomAsync(roomEntity);
            return _mapper.Map<RoomDto>(createdRoom);
        }
    }
}