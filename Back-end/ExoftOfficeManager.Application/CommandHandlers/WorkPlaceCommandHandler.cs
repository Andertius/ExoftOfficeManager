using System;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Application.Utilities;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Application.CommandHandlers
{
    public class WorkPlaceCommandHandler : IWorkPlaceCommandHandler
    {
        private readonly IRepository<WorkPlace> _repository;
        private readonly IRepository<User> _userRepository;

        public WorkPlaceCommandHandler(IRepository<WorkPlace> repo, IRepository<User> userRepository)
        {
            _repository = repo;
            _userRepository = userRepository;
        }

        public async Task BookCommand(long id, long developerId, BookingType type, DateTime date, int days)
        {
            if (type == BookingType.Available)
            {
                throw new ArgumentException($"Cannot book with status '{type}'.");
            }

            var place = await _repository.Find(id);

            if (IsBookedHelper.IsBooked(place, date))
            {
                throw new ArgumentException($"The work place with id = {id} is already fully booked");
            }
            else
            {
                if (place.Bookings.Where(x => x.Date == date && x.Type == type).Any())
                {
                    throw new ArgumentException($"Cannot book with status '{type}', because the work place already has that status.");
                }

                if (days > 1)
                {
                    for (int i = 0; i < days; i++)
                    {
                        place.Bookings.Add(new Booking
                        {
                            Date = new DateTime(date.Year, date.Month, date.Day + i),
                            Type = type,
                            Status = BookingStatus.Pending,
                            User = await _userRepository.Find(developerId),
                            WorkPlace = await _repository.Find(id),
                            DayNumber = days,
                        });
                    }
                }
                else
                {
                    place.Bookings.Add(new Booking
                    {
                        Date = new DateTime(date.Year, date.Month, date.Day),
                        Type = type,
                        Status = BookingStatus.Approved,
                        User = await _userRepository.Find(developerId),
                        WorkPlace = await _repository.Find(id),
                        DayNumber = days,
                    });
                }

                _repository.Update(place);
                await _repository.Commit();
            }
        }

        public async Task UpdateCommand(WorkPlace place)
        {
            _repository.Update(place);
            await _repository.Commit();
        }

        public async Task RemoveCommand(long id)
        {
            _repository.Remove(id);
            await _repository.Commit();
        }

        
    }
}
