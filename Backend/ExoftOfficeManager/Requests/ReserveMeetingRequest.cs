using System;
using System.ComponentModel.DataAnnotations;

namespace ExoftOfficeManager.Requests
{
    public class ReserveMeetingRequest
    {
        public DateTime DateAndTime { get; set; }

        public int DurationMinutes { get; set; }

        public int RoomNumber { get; set; }

        [Required]
        public string MeetingPurpose { get; set; }
    }
}
