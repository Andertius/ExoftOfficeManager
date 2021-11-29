using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Queries.FindUserByEmail
{
    public class FindUserByEmailQueryHandler : IRequestHandler<FindUserByEmailQuery, UsersQueryResponse>
    {
        private readonly IUserRepository _repository;

        public FindUserByEmailQueryHandler(IUserRepository repo)
        {
            _repository = repo;
        }

        public async Task<UsersQueryResponse> Handle(FindUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindUserByEmail(request.Email);
            return new UsersQueryResponse(UserMapper.MapIntoDto(user));
        }
    }
}
