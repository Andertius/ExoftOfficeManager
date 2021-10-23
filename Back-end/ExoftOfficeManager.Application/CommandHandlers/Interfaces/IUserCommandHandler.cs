using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers.Interfaces
{
    public interface IUserCommandHandler
    {
        Task AddCommand(User user);
    }
}
