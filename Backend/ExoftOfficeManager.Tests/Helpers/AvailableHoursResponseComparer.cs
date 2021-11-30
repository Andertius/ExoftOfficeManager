using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using ExoftOfficeManager.Application.Meetings.Queries.GetAvailableHours;

namespace ExoftOfficeManager.Tests.Helpers
{
    class AvailableHoursResponseComparer : IEqualityComparer<GetAvailableHoursQueryResponse>
    {
        public bool Equals(GetAvailableHoursQueryResponse x, GetAvailableHoursQueryResponse y)
        {
            return x.AvailableHour == y.AvailableHour;
        }

        public int GetHashCode([DisallowNull] GetAvailableHoursQueryResponse obj)
        {
            return obj.AvailableHour.GetHashCode();
        }
    }
}
