using System;

namespace ExoftOfficeManager.Domain.Entities
{
    public abstract class EntityBase
    {
        public long Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }


        //public string CreatedBy { get; set; }

        //public string UpdatedBy { get; set; }
    }
}