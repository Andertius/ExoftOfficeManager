using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers
{
    public class UserQueryHandler : IUserQueryHandler
    {
        private readonly IRepository<User> _repository;

        public UserQueryHandler(IRepository<User> repo)
        {
            _repository = repo;
        }

        public async Task<User> FindQuery(long id)
            => await _repository.Find(id);

        public IEnumerable<User> GetAllQuery()
            => _repository.GetAll().ToList();
    }
}
