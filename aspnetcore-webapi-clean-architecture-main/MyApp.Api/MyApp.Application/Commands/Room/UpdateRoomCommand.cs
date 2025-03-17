using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Room;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Room
{
    public record UpdateRoomCommand(int RoomId, UpdateRoomRequest RoomRequest) : IRequest<RoomDto>;

    public class UpdateRoomCommandHandler(IRoomRepository roomRepository, IMapper mapper)
        : IRequestHandler<UpdateRoomCommand, RoomDto>
    {
        public async Task<RoomDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomEntity = await roomRepository.GetRoomByIdAsync(request.RoomId);
            if (roomEntity == null) return null; 

            mapper.Map(request.RoomRequest, roomEntity);
            var updatedRoom = await roomRepository.UpdateRoomAsync(roomEntity);
            return mapper.Map<RoomDto>(updatedRoom);
        }
    }
}
