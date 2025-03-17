using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Core.Interfaces;

namespace MyApp.Application.Commands.Room
{
    public record DeleteRoomCommand(int RoomId) : IRequest<bool>;

    public class DeleteRoomCommandHandler(IRoomRepository roomRepository)
        : IRequestHandler<DeleteRoomCommand, bool>
    {
        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            return await roomRepository.DeleteRoomAsync(request.RoomId);
        }
    }
}
