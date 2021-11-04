using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IMeetingRepository
    {
        Task<IList<Meeting>> GetAllMeetings(DateTime meetingDate);

        Task<Meeting> FindMeetingById(Guid meetingId);

        Task AddMeeting(Meeting meeting);

        Task RemoveMeeting(Guid meetingId);

        void UpdateMeeting(Meeting meeting);

        Task Commit();
    }
}
