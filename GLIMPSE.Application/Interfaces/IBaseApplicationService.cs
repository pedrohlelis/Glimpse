namespace GLIMPSE.Application.Interfaces
{
    public interface IBaseApplicationService<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<TEntity> Update(TEntity obj);
        Task<IList<TEntity>> GetAll();
        Task<IList<TEntity>> GetAllIgnoreFilters();
        Task<TEntity> GetById(int id);
        Task<TEntity> Remove(int id);
    }
}
