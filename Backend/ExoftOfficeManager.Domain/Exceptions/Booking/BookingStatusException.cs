using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Domain.Exceptions.Booking
{
    public class BookingStatusException : BookingException
    {
        public BookingStatusException(BookingType bookingType)
               : base($"Cannot book with status '{bookingType}', because the work place already has that status.")
        {
        }

        public BookingStatusException(string message)
               : base(message)
        {
        }
    }
}
