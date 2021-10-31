using ExoftOfficeManager.Domain.Entities;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Commands.UpdateWorkPlace
{
    public class UpdateWorkPlaceCommand : IRequest
    {
        public UpdateWorkPlaceCommand(WorkPlace place)
        {
            WorkPlace = place;
        }

        public WorkPlace WorkPlace { get; set; }
    }
}
