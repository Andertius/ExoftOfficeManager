using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Entities;

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

        public async Task<IList<Meeting>> GetAllMeetings(DateTime meetingDate)
        {
            return await _context.Meetings
                .Where(x => x.DateAndTime.Date == meetingDate)
                .ToListAsync();
        }

        public async Task<Meeting> FindMeetingById(Guid meetingId)
        {
            return await _context.Meetings.FindAsync(meetingId);
        }

        public async Task AddMeeting(Meeting meeting)
        {
            await _context.AddAsync(meeting);
        }

        public async Task RemoveMeeting(Guid meetingId)
        {
            var meeting = await _context.Meetings.FindAsync(meetingId);
            _context.Remove(meeting);
        }

        public void UpdateMeeting(Meeting meeting)
        {
            _context.Update(meeting);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
