using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class ProjectApplicationService : IBaseApplicationService<Project>, IProjectApplicationService
    {
        private readonly IProjectService projectService;
        public ProjectApplicationService(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        public Task<Project> Add(Project obj)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Project>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Project>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Project> Update(Project obj)
        {
            throw new NotImplementedException();
        }
    }
}
