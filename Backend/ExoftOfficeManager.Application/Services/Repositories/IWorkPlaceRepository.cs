using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IWorkPlaceRepository
    {
        Task<IList<WorkPlace>> GetAllWorkPlaces();

        Task<IList<WorkPlace>> GetAllBookedWorkPlaces(DateTime bookingDate);

        Task<IList<WorkPlace>> GetAllAvailableWorkPlaces(DateTime bookingDate);

        Task<WorkPlace> FindWorkPlaceById(Guid placeId);

        Task<WorkPlace> FindWorkPlaceByPlaceNumber(int place, int floor);

        /// <summary>
        /// Attempts to get a workplace from the database by <paramref name="placeId"/>.
        /// </summary>
        /// <returns>
        /// (<see langword="true"/>, <see cref="WorkPlace"/>) if the workplace is available for the <paramref name="bookingDate"/>,
        /// otherwise (<see langword="false"/>, <see langword="null"/>).
        /// </returns>
        Task<(bool, WorkPlace)> TryFindAvailableWorkPlace(Guid placeId, DateTime bookingDate);

        void UpdateWorkPlace(WorkPlace place);

        Task Commit();
    }
}
