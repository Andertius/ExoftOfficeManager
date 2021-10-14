using System.Collections.Generic;

namespace ExoftOfficeManager.Services.Interfaces
{
    public interface IMeetingService
    {
        IEnumerable<Meeting> GetAll();

        Meeting Find(long id);

        void Add(Meeting meet);

        bool Update(long id, Meeting meet);

        bool Remove(long id);
    }
}
