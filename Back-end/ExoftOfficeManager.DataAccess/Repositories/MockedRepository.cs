using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExoftOfficeManager.DataAccess.Repositories
{
    public abstract class MockedRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly List<TEntity> _entities;
        protected long highestId = 1;

        public MockedRepository(IEnumerable<TEntity> entities)
            => _entities = entities.ToList();

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            entity.Id = highestId;
            highestId++;

            await Task.Run(() => _entities.Add(entity));
            return entity;
        }

        public virtual async Task Commit()
        {
            string message = "Aye, this is a mocked repo, i can't commit into a List. That's a bruh moment right here.";
            Debug.WriteLine(message);
        }

        public virtual TEntity Find(long id)
            => _entities.Find(x => x.Id == id);

        public virtual IQueryable<TEntity> GetAll()
            => _entities.Select(x => x).AsQueryable();

        public virtual async Task Remove(TEntity entity)
        {
            if (entity is not null)
            {
                await Task.Run(() => _entities.Remove(entity));
            }
        }

        public virtual async Task Remove(long id)
        {
            var entity = await Task.Run(() => Find(id));

            if (entity is not null)
            {
                await Task.Run(() => Remove(entity));
            }
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            int index = await Task.Run(() => _entities.FindIndex(x => x.Id == entity.Id));

            if (index == -1)
            {
                return null;
            }

            _entities[index] = entity;
            return entity;
        }
    }
}
