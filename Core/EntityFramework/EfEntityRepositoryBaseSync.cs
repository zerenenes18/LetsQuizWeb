using System.Linq.Expressions;
using Core.DataAccess;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.EntityFramework;

public class EfEntityRepositoryBaseSync<TEntity, TContext> : IEntityRepositorySync<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    private readonly DbContextOptions<TContext> _options;

    public EfEntityRepositoryBaseSync(DbContextOptions<TContext> options)
    {
        _options = options;
    }

    public void Add(TEntity entity)
    {
        using (var context = (TContext)Activator.CreateInstance(typeof(TContext), _options))
        {
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
        }
    }

    public void Delete(TEntity entity)
    {
        using (var context = (TContext)Activator.CreateInstance(typeof(TContext), _options))
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
        using (var context = (TContext)Activator.CreateInstance(typeof(TContext), _options))
        {
            return context.Set<TEntity>().SingleOrDefault(filter);
        }
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
        using (var context = (TContext)Activator.CreateInstance(typeof(TContext), _options))
        {
            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }
    }

    public void Update(TEntity entity)
    {
        using (var context = (TContext)Activator.CreateInstance(typeof(TContext), _options))
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}