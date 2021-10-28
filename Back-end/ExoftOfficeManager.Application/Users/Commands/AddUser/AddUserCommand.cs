using ExoftOfficeManager.Domain.Dtos;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Commands.AddUser
{
    public class AddUserCommand : IRequest
    {
        public AddUserCommand(UserDto user)
        {
            User = user;
        }

        public UserDto User { get; set; }
    }
}
