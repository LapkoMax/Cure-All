using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository.Impl
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext DataContext;
        public RepositoryBase(DataContext dataContext) =>
            DataContext = dataContext;
        public virtual IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Set<T>()
            .AsNoTracking() :
            DataContext.Set<T>();
        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Set<T>()
            .Where(expression)
            .AsNoTracking() :
            DataContext.Set<T>()
            .Where(expression);
        public void Create(T entity) => DataContext.Set<T>().Add(entity);
        public void Update(T entity) => DataContext.Set<T>().Update(entity);
        public void Delete(T entity) => DataContext.Set<T>().Remove(entity);
    }
}
