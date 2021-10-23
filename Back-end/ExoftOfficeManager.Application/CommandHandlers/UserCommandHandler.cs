using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers
{
    public class UserCommandHandler : IUserCommandHandler
    {
        private readonly IRepository<User> _repository;

        public UserCommandHandler(IRepository<User> repo)
        {
            _repository = repo;
        }

        public async Task AddCommand(User user)
        {
            await _repository.Add(user);
            await _repository.Commit();
        }
    }
}
