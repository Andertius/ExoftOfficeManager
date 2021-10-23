using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers
{
    public class BookingCommandHandler : IBookingCommandHandler
    {
        public Task RemoveCommand(long bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> UpdateCommand(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> UpdateCommand(long id, BookingStatus status)
        {
            throw new NotImplementedException();
        }
    }
}
