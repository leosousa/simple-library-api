using NetDevPack.Domain;
using System.Linq.Expressions;

namespace SimpleLibrary.Domain.Interfaces.Repositories.Base;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Create(TEntity entity);
    void CreateAll(IEnumerable<TEntity> entities);
    TEntity? GetById(Guid id);
    TEntity? Get(Expression<Func<TEntity, bool>> filter);
    IEnumerable<TEntity> List();
    IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filters);
    IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filters, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize);
    IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> filters, int? page, int? pagesize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter);
    int Count(Expression<Func<TEntity, bool>>? filter = null);
    int GetPageSize();
    void Update(TEntity entity);
    void UpdateAll(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void Remove(Guid id);
    void RemoveAll(IEnumerable<TEntity> entities);
}
