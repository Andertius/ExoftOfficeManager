using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess.Entities;

namespace ExoftOfficeManager.DataAccess.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        IQueryable<T> GetAll(IEnumerable<string> include);

        Task<T> Find(long id, IEnumerable<string> include);

        Task<T> Add(T entity);

        T Update(T entity);

        void Remove(T entity);

        Task Remove(long id);

        Task Commit();
    }
}
