using System;

namespace ExoftOfficeManager.Application.Meetings.Queries.GetAvailableHours
{
    public class GetAvailableHoursQueryResponse
    {
        public GetAvailableHoursQueryResponse(TimeSpan span)
        {
            AvailableHour = span;
        }

        public TimeSpan AvailableHour { get; private set; }
    }
}
