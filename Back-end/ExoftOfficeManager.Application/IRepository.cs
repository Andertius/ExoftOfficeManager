using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application
{
    public interface IRepository<T> where T : EntityBase
    {
        IQueryable<T> GetAll();

        Task<T> Find(long id);

        Task<T> Add(T entity);

        T Update(T entity);

        void Remove(T entity);

        void Remove(long id);

        Task Commit();
    }
}
