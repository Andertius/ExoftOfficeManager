using System.Linq;
using System.Threading.Tasks;

namespace ExoftOfficeManager.DataAccess.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> GetAll();

        T Find(long id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task Remove(T entity);

        Task Remove(long id);

        Task Commit();
    }
}
