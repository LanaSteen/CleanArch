using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Room;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Queries.Room
{
    public record GetRoomByIdQuery(int RoomId) : IRequest<RoomDto>;

    public class GetRoomByIdQueryHandler(IRoomRepository roomRepository, IMapper mapper)
        : IRequestHandler<GetRoomByIdQuery, RoomDto>
    {
        public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await roomRepository.GetRoomByIdAsync(request.RoomId);
            return mapper.Map<RoomDto>(room);
        }
    }
}
