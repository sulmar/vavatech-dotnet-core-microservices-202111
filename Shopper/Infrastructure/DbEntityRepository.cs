using Core.Domain;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public abstract class DbEntityRepository<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        protected readonly TContext context;
        protected DbSet<TEntity> entities => context.Set<TEntity>();

        public DbEntityRepository(TContext context)
        {
            this.context = context;
        }

        public virtual async Task Add(TEntity entity)
        {
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async virtual Task<IEnumerable<TEntity>> Get()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public async virtual Task<TEntity> Get(int id)
        {
            return await entities.FindAsync(id);
        }
    }
}
