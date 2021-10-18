using System.Collections.Generic;

namespace ExoftOfficeManager.DataAccess
{
    public class User : IEntity
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }

        public UserRole Role { get; set; }


        //public ICollection<WorkPlace> WorkPlaces { get; set; }

        //public ICollection<Meeting> Meetings { get; set; }
    }
}
