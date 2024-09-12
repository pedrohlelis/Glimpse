using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class RepoService : BaseService<Repository>, IRepoService
    {
        private readonly IRepoRepository RepoRepository;

        public RepoService(IRepoRepository RepoRepo) : base(RepoRepository)
        {
            this.RepoRepository = RepoRepository;
        }
    }
}