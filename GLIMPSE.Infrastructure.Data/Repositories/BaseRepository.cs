using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly GlimpseContext sqlContext;

        public BaseRepository(GlimpseContext _sqlcontext)
        {
            this.sqlContext = _sqlcontext;
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            try
            {
                this.sqlContext.Set<TEntity>().Add(obj);
                await this.sqlContext.SaveChangesAsync();
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await this.sqlContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllIgnoreFilters()
        {
            return await this.sqlContext.Set<TEntity>().IgnoreQueryFilters().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            var keyProperty = this.sqlContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties[0];
            var entity = this.sqlContext.Set<TEntity>().IgnoreQueryFilters().FirstOrDefault(e => EF.Property<int>(e, keyProperty.Name) == id);

            return entity;
        }

        public async Task<IList<TEntity>> GetIgnoreFilters(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.sqlContext.Set<TEntity>().Where(predicate).IgnoreQueryFilters().ToListAsync();
        }

        public async Task<TEntity> Remove(int id)
        {
            var keyProperty = this.sqlContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties[0];
            var entity = this.sqlContext.Set<TEntity>().IgnoreQueryFilters().FirstOrDefault(e => EF.Property<int>(e, keyProperty.Name) == id);
            if (entity == null)
            {
                return null;
            }
            this.sqlContext.Set<TEntity>().Remove(entity);
            await this.sqlContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity obj)
        {
            this.sqlContext.Entry(obj).State = EntityState.Modified;
            await this.sqlContext.SaveChangesAsync();
            return obj;
        }
    }
}
