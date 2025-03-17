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
    public record GetAllRoomsQuery() : IRequest<List<RoomDto>>;

    public class GetAllRoomsQueryHandler(IRoomRepository roomRepository, IMapper mapper)
        : IRequestHandler<GetAllRoomsQuery, List<RoomDto>>
    {
        public async Task<List<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await roomRepository.GetAllRoomsAsync();
            return rooms.Select(mapper.Map<RoomDto>).ToList();
        }
    }
}
