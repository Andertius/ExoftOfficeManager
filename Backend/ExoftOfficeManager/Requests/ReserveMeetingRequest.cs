using System;

namespace ExoftOfficeManager.Requests
{
    public class ReserveMeetingRequest
    {
        public DateTime DateAndTime { get; set; }

        public int DurationMinutes { get; set; }

        public int RoomNumber { get; set; }

        public string MeetingPurpose { get; set; }
    }
}
