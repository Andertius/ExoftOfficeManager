using System;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public UpdateUserCommand(
            Guid id,
            string fullName,
            string avatar)
        {
            Id = id;
            FullName = fullName;
            Avatar = avatar;
        }

        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }
    }
}
