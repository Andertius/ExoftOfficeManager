using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Infrastructure.Repositories.EfCore
{
    public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : EntityBase
        where TContext : AppDbContext
    {
        protected readonly TContext _context;

        public EfCoreRepository(TContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> Find(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity is not null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }

        public virtual void Remove(long id)
        {
            var entity = _context.Set<TEntity>().Find(id);

            if (entity is not null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }
    }
}
