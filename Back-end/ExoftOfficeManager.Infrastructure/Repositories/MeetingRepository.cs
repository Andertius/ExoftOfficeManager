using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Dtos;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly AppDbContext _context;

        public MeetingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<MeetingDto>> GetAllMeetings(DateTime meetingDate)
        {
            return await _context.Meetings
                .Where(x => x.DateAndTime.Date == meetingDate)
                .Select(x => MeetingMapper.MapIntoDto(x))
                .ToListAsync();
        }

        public async Task<MeetingDto> FindMeetingById(Guid meetingId)
        {
            return MeetingMapper.MapIntoDto(await _context.Meetings.FindAsync(meetingId));
        }

        public async Task AddMeeting(MeetingDto meetingDto)
        {
            await _context.AddAsync(MeetingMapper.MapFromDto(meetingDto));
        }

        public async Task RemoveMeeting(Guid meetingId)
        {
            var meeting = await _context.Meetings.FindAsync(meetingId);
            _context.Remove(meeting);
        }

        public void UpdateMeeting(MeetingDto meetingDto)
        {
            _context.Update(MeetingMapper.MapFromDto(meetingDto));
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
