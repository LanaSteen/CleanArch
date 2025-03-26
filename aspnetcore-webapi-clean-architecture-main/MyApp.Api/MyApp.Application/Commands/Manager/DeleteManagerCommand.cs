using MediatR;
using MyApp.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Commands.Manager
{
    public record DeleteManagerCommand(int Id) : IRequest<bool>;

    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand, bool>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;

        public DeleteManagerCommandHandler(
            IManagerRepository managerRepository,
            IUserRepository userRepository) 
        {
            _managerRepository = managerRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var managerEntity = await _managerRepository.GetByIdAsync(request.Id);
            if (managerEntity == null)
                return false;

            var user = await _managerRepository.GetUserByManagerIdAsync(request.Id);

            var managerDeleted = await _managerRepository.DeleteAsync(managerEntity);

            if (user != null && managerDeleted)
            {
                await _userRepository.DeleteAsync(user);
            }

            return managerDeleted;
        }
    }
}