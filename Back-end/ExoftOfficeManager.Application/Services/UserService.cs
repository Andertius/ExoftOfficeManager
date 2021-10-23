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

        public async Task<User> Find(long id)
            => await _repository.Find(id);

        public IEnumerable<User> GetAll()
            => _repository.GetAll().ToList();
        
        public async Task Add(User user)
        {
            await _repository.Add(user);
            await _repository.Commit();
        }
    }
}
