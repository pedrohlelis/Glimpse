using GLIMPSE.Domain.Models;
using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Infrastructure.Data.Repositories
{
    public class RepositoryRepository : BaseRepository<Repository>, IRepositoryRepository
    {
        private readonly GlimpseContext sqlContext;

        public RepositoryRepository(GlimpseContext _sqlContext) : base(_sqlContext)
        {
            this.sqlContext = _sqlContext;
        }

    }
}