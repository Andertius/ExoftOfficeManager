using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly IUserRepository _repository;

        public AddUserCommandHandler(IUserRepository repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddUser(request.User);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}
