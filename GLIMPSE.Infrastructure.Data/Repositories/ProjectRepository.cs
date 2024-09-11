using GLIMPSE.Domain.Models;
using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Infrastructure.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly GlimpseContext sqlContext;

        public ProjectRepository(GlimpseContext _sqlContext) : base(_sqlContext)
        {
            this.sqlContext = _sqlContext;
        }
        
    }
}
