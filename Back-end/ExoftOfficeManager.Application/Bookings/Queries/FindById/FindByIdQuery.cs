using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.FindById
{
    public class FindByIdQuery : IRequest<BookingsQueryResponse>
    {
        public FindByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
