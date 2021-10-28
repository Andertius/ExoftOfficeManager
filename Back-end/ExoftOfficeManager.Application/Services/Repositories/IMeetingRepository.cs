using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Dtos;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IMeetingRepository
    {
        Task<IList<MeetingDto>> GetAllMeetings(DateTime meetingDate);

        Task<MeetingDto> FindMeetingById(Guid meetingId);

        Task AddMeeting(MeetingDto meeting);

        Task RemoveMeeting(Guid meetingId);

        void UpdateMeeting(MeetingDto meeting);

        Task Commit();
    }
}
