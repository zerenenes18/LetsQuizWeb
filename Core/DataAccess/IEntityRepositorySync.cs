using System.Linq.Expressions;
using Core.Entities;

namespace Core.DataAccess;


public interface IEntityRepositorySync<T> where T : class, IEntity, new()
{
    List<T> GetAll(Expression<Func<T, bool>> filter = null);
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Delete(T entity);
    void Update(T entity);
}