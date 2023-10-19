using System.Linq.Expressions;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookBook.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext repositoryContext {get; set;}
        public RepositoryBase(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }
        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? 
                    repositoryContext.Set<T>().AsNoTracking() :
                    repositoryContext.Set<T>();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ?
                    repositoryContext.Set<T>().Where(expression).AsNoTracking() :
                    repositoryContext.Set<T>().Where(expression);
        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);    
        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
        public void CreateRange(List<T> entities) => repositoryContext.Set<List<T>>().AddRange(entities);
        public void UpdateRange(List<T> entities) => repositoryContext.Set<List<T>>().UpdateRange(entities);
        public void DeleteRange(List<T> entities) => repositoryContext.Set<List<T>>().RemoveRange(entities);

    }
}