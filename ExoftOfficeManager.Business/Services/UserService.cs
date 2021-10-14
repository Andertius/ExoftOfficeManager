using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;
using ExoftOfficeManager.DataAccess.Repositories;

namespace ExoftOfficeManager.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
            => _repository = repository;

        public User Find(long id)
            => _repository.Find(id);
    }
}
