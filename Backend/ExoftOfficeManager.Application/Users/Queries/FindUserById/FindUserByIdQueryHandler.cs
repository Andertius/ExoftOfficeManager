using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Queries.FindUserById
{
    public class FindUserByIdQueryHandler : IRequestHandler<FindUserByIdQuery, UsersQueryResponse>
    {
        private readonly IUserRepository _repository;

        public FindUserByIdQueryHandler(IUserRepository repo)
        {
            _repository = repo;
        }

        public async Task<UsersQueryResponse> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindUserById(request.UserId);
            return new UsersQueryResponse(UserMapper.MapIntoDto(user));
        }
    }
}
