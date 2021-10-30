using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IWorkPlaceRepository
    {
        Task<IList<WorkPlaceDto>> GetAllWorkPlaces();

        Task<IList<WorkPlaceDto>> GetAllBookedWorkPlaces(DateTime bookingDate);

        Task<IList<WorkPlaceDto>> GetAllAvailableWorkPlaces(DateTime bookingDate);

        Task<WorkPlaceDto> FindWorkPlaceById(Guid placeId);

        /// <summary>
        /// Attempts to get a workplace from the database by <paramref name="placeId"/>.
        /// </summary>
        /// <returns>
        /// (<see langword="true"/>, <see cref="WorkPlace"/>) if the workplace is available for the <paramref name="bookingDate"/>,
        /// otherwise (<see langword="false"/>, <see langword="null"/>).
        /// </returns>
        Task<(bool, WorkPlace)> TryFindAvailableWorkPlace(Guid placeId, DateTime bookingDate);

        void UpdateWorkPlace(WorkPlaceDto place);

        Task Commit();
    }
}
