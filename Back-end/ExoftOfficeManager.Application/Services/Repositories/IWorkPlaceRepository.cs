using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Dtos;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IWorkPlaceRepository
    {
        Task<IList<WorkPlaceDto>> GetAllWorkPlaces();

        Task<IList<WorkPlaceDto>> GetAllBookedWorkPlaces(DateTime bookingDate);

        Task<IList<WorkPlaceDto>> GetAllAvailableWorkPlaces(DateTime bookingDate);

        Task<WorkPlaceDto> FindWorkPlaceById(Guid placeId);

        void UpdateWorkPlace(WorkPlaceDto place);

        Task Commit();
    }
}
