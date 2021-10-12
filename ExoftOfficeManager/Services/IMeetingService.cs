using System.Collections.Generic;

namespace ExoftOfficeManager.Services
{
    public interface IMeetingService
    {
        public IEnumerable<Meeting> GetAll();

        public Meeting Find(long id);

        public void Add(Meeting meet);

        public bool Update(long id, Meeting meet);

        public bool Remove(long id);
    }
}
