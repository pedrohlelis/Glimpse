using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Domain.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> repository;
        public BaseService(IBaseRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            await repository.Add(obj);
            return obj;
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await repository.GetAll();
        }

        public async Task<IList<TEntity>> GetAllIgnoreFilters()
        {
            return await repository.GetAllIgnoreFilters();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task<TEntity> Remove(int id)
        {
            var entity = await repository.Remove(id);
            return entity;
        }

        public async Task<TEntity> Update(TEntity obj)
        {
            await repository.Update(obj);
            return obj;
        }
    }
}
