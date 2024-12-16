using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class RepositoryApplicationService : BaseApplicationService<Repository, RepositoryDTO>, IRepositoryApplicationService
    {
        public RepositoryApplicationService(IMapper mapper, IRepositoryService repositoryService): base(mapper, repositoryService)
        {
        }
    }
}
