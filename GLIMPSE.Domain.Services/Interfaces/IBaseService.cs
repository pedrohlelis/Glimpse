using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Domain.Services.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<TEntity> Update(TEntity obj);
        Task<TEntity> Remove(int id);
        Task<IList<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> GetByIdNoTracking(int id);
        Task<IList<TEntity>> GetAllIgnoreFilters();
        Task<IList<TEntity>> GetAllIgnoreFilters(Expression<Func<TEntity, bool>> predicate);
    }
}