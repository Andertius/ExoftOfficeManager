namespace ExoftOfficeManager.Domain.Exceptions.Booking
{
    public class BookingException : DatabaseException
    {
        public BookingException(string message)
            : base(message)
        {
        }
    }
}
