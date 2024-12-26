using GLIMPSE.Domain.Models;
using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Infrastructure.Data.Repositories
{
    public class BlobFileRepository : BaseRepository<BlobFile>, IBlobFileRepository
    {
        private readonly GlimpseContext sqlContext;

        public BlobFileRepository(GlimpseContext _sqlContext) : base(_sqlContext)
        {
            this.sqlContext = _sqlContext;
        }

    }
}