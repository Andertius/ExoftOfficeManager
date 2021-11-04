using System;

using MediatR;

namespace ExoftOfficeManager.Application.Users.Queries.FindUserById
{
    public class FindUserByIdQuery : IRequest<UsersQueryResponse>
    {
        public FindUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
