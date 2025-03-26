using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Room;
using MyApp.Application.Exceptions;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Room
{
    public record UpdateRoomCommand(int RoomId, UpdateRoomRequest RoomRequest) : IRequest<RoomDto>;

    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, RoomDto>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(
            IRoomRepository roomRepository,
            IHotelRepository hotelRepository,
            IMapper mapper)
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<RoomDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomEntity = await _roomRepository.GetRoomByIdAsync(request.RoomId);
            if (roomEntity == null) return null;

            if (request.RoomRequest.HotelId.HasValue)
            {
                var hotelExists = await _hotelRepository.GetHotelByIdAsync(request.RoomRequest.HotelId.Value);
                if (hotelExists == null)
                {
                    throw new NotFoundException($"Hotel with ID {request.RoomRequest.HotelId} not found.");
                }
                roomEntity.HotelId = request.RoomRequest.HotelId.Value;
            }

            if (request.RoomRequest.Name != null)
                roomEntity.Name = request.RoomRequest.Name;

            if (request.RoomRequest.IsAvailable.HasValue)
                roomEntity.IsAvailable = request.RoomRequest.IsAvailable.Value;

            if (request.RoomRequest.Price.HasValue)
                roomEntity.Price = request.RoomRequest.Price.Value;

            var updatedRoom = await _roomRepository.UpdateRoomAsync(roomEntity);
            return _mapper.Map<RoomDto>(updatedRoom);
        }
    }
}
