using System;

namespace ExoftOfficeManager
{
    public class Meeting
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the start time by hours
        /// </summary>
        public int StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time by hours
        /// </summary>
        public int EndTime { get; set; }

        public int RoomNumber { get; set; }
    }
}
