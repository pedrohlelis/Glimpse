using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Infrastructure.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<TEntity> Update(TEntity obj);
        Task<TEntity> Remove(int id);
        Task<IList<TEntity>> GetAll();
        Task<IList<TEntity>> GetAllIgnoreFilters();
        Task<TEntity> GetById(int id);
    }
}
