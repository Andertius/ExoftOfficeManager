using System;

using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Requests
{
    public class BookWorkPlaceRequest
    {
        public Guid UserId { get; set; }

        public BookingType BookingType { get; set; }

        public DateTime BookingDate { get; set; }

        public int? Days { get; set; }
    }
}
