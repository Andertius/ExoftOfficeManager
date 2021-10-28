using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersQueryResponse[]>
    {
        private readonly IUserRepository _repository;

        public GetUsersQueryHandler(IUserRepository repo)
        {
            _repository = repo;
        }

        public async Task<UsersQueryResponse[]> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var userDtos = await _repository.GetAllUsers();
            return userDtos.Select(x => new UsersQueryResponse(x)).ToArray();
        }
    }
}
