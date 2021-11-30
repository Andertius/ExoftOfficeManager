using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _repository;

        public UpdateUserCommandHandler(IUserRepository repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindUserById(request.Id);

            user.Avatar = request.Avatar;
            user.FullName = request.FullName;

            _repository.UpdateUser(user);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}
