using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Queries.GetWorkPlaces
{
    public class GetWorkPlacesQuery : IRequest<WorkPlacesQueryResponse[]>
    {
    }
}
