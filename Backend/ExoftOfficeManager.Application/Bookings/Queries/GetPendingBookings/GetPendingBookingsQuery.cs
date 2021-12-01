using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.GetPendingBookings
{
    public class GetPendingBookingsQuery : IRequest<BookingsQueryResponse[]>
    {
    }
}
