using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers
{
    public class BookingCommandHandler : IBookingCommandHandler
    {
        private readonly IRepository<Booking> _repository;

        public BookingCommandHandler(IRepository<Booking> repo)
        {
            _repository = repo;
        }

        public async Task RemoveCommand(long bookingId)
        {
            _repository.Remove(bookingId);
            await _repository.Commit();
        }

        public async Task UpdateCommand(Booking booking)
        {
            _repository.Update(booking);
            await _repository.Commit();
        }

        public async Task UpdateCommand(long id, BookingStatus status)
        {
            var result = await _repository.Find(id);
            result.Status = status;

            _repository.Update(result);
            await _repository.Commit();
        }
    }
}
