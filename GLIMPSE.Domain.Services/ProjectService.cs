using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository) : base(projectRepository)
        {
            this.projectRepository = projectRepository;
        }
    }
}
