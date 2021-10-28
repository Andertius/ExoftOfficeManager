using ExoftOfficeManager.Domain.Dtos;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Commands.UpdateWorkPlace
{
    public class UpdateWorkPlaceCommand : IRequest
    {
        public UpdateWorkPlaceCommand(WorkPlaceDto place)
        {
            WorkPlace = place;
        }

        public WorkPlaceDto WorkPlace { get; set; }
    }
}
