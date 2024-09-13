using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class RepositoryService : BaseService<Repository>, IRepositoryService
    {
        private readonly IRepositoryRepository RepoRepository;

        public RepositoryService(IRepositoryRepository RepoRepository) : base(RepoRepository)
        {
            this.RepoRepository = RepoRepository;
        }
    }
}