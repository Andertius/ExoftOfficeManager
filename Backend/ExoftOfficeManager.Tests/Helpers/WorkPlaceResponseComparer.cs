using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using ExoftOfficeManager.Application.WorkPlaces.Queries;

namespace ExoftOfficeManager.Tests.Helpers
{
    class WorkPlaceResponseComparer : IEqualityComparer<WorkPlacesQueryResponse>
    {
        public bool Equals(WorkPlacesQueryResponse x, WorkPlacesQueryResponse y)
        {
            return x.WorkPlace.Id == y.WorkPlace.Id;
        }

        public int GetHashCode([DisallowNull] WorkPlacesQueryResponse obj)
        {
            return obj.WorkPlace.GetHashCode();
        }
    }
}
