using System.Threading.Tasks;

using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers.Interfaces
{
    public interface IBookingCommandHandler
    {
        Task RemoveCommand(long bookingId);

        Task UpdateCommand(Booking booking);

        Task UpdateCommand(long id, BookingStatus status);
    }
}
