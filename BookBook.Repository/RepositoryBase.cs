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
        public IQueryable<T> FindAll() => repositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
            repositoryContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);    
        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);

    }
}