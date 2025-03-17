using MediatR;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Manager
{
    public record DeleteManagerCommand(int Id) : IRequest<bool>;
    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand, bool>
    {
        private readonly IManagerRepository _managerRepository;

        public DeleteManagerCommandHandler(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<bool> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var managerEntity = await _managerRepository.GetByIdAsync(request.Id);
            if (managerEntity == null)
                return false;

            await _managerRepository.DeleteAsync(managerEntity);
            return true;
        }
    }
}
