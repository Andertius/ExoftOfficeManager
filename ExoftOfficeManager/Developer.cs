using System.Collections.Generic;

namespace ExoftOfficeManager
{
    public class Developer
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }


        public ICollection<WorkPlace> WorkPlaces { get; set; }
    }
}
