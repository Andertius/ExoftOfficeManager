using ExoftOfficeManager.Domain.Entities;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Commands.AddUser
{
    public class AddUserCommand : IRequest
    {
        public AddUserCommand(User user)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
