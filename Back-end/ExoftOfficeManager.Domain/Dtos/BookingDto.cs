using System;

using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Domain.Dtos
{
    public class BookingDto
    {
        public Guid Id { get; set; }

        public DateTime? Date { get; set; }

        public BookingType Type { get; set; }

        public BookingStatus Status { get; set; }

        public int? DayNumber { get; set; }

        public Guid UserId { get; set; }

        public UserDto User { get; set; }

        public Guid WorkPlaceId { get; set; }

        public WorkPlaceDto WorkPlace { get; set; }
    }
}
