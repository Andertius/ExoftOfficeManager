using MediatR;

namespace ExoftOfficeManager.Application.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<UsersQueryResponse[]>
    {
    }
}
