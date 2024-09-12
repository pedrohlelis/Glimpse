using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Checkboxs.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Checkboxs
{
    public class ProjectCheckbox : BaseCheckbox<Project>, IProjectCheckbox
    {
        private readonly IProjectRepository projectRepository;

        public ProjectCheckbox(IProjectRepository projectRepository) : base(projectRepository)
        {
            this.projectRepository = projectRepository;
        }
    }
}