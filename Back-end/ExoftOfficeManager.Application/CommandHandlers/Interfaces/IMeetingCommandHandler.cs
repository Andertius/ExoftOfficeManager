using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers.Interfaces
{
    public interface IMeetingCommandHandler
    {
        Task AddCommand(Meeting meet);

        Task UpdateCommand(Meeting meet);

        Task RemoveCommand(long meetingId);
    }
}
