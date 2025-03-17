using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Room;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Room
{
    public record CreateRoomCommand(CreateRoomRequest RoomRequest) : IRequest<RoomDto>;

    public class CreateRoomCommandHandler(IRoomRepository roomRepository, IMapper mapper)
        : IRequestHandler<CreateRoomCommand, RoomDto>
    {
        public async Task<RoomDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomEntity = mapper.Map<RoomEntity>(request.RoomRequest);
            var createdRoom = await roomRepository.CreateRoomAsync(roomEntity);
            return mapper.Map<RoomDto>(createdRoom);
        }
    }
}
