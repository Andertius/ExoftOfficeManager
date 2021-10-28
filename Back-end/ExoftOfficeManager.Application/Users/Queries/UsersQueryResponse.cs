using ExoftOfficeManager.Domain.Dtos;

namespace ExoftOfficeManager.Application.Users.Queries
{
    public class UsersQueryResponse
    {
        public UsersQueryResponse(UserDto user)
        {
            User = user;
        }

        public UserDto User { get; private set; }
    }
}
