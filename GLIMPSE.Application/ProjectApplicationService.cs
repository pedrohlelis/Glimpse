using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class ProjectApplicationService : BaseApplicationService<Project, ProjectDTO>, IProjectApplicationService
    {
        public ProjectApplicationService(IMapper mapper, IProjectService projectService): base(mapper, projectService)
        {
        }
    }
}
