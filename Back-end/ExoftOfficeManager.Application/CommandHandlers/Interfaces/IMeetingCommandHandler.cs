using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers.Interfaces
{
    public interface IMeetingCommandHandler
    {
        Task<bool> AddCommand(Meeting meet);

        Task<Meeting> UpdateCommand(Meeting meet);

        Task RemoveCommand(long meetingId);
    }
}
