using MediatR;

namespace ExoftOfficeManager.Application.Users.Queries.FindUserByEmail
{
    public class FindUserByEmailQuery: IRequest<UsersQueryResponse>
    {
        public FindUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
