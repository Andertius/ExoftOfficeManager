using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
            => _repository = repository;

        public async Task<User> Find(long id, IEnumerable<string> inclusion)
            => await _repository.Find(id, inclusion);

        public IEnumerable<User> GetAll(IEnumerable<string> inclusion)
            => _repository.GetAll(inclusion).ToList();
        
        public async Task Add(User user)
        {
            await _repository.Add(user);
            await _repository.Commit();
        }
    }
}
