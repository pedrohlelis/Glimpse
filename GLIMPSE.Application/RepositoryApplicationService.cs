using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class RepositoryApplicationService : IBaseApplicationService<Repository>, IRepositoryApplicationService
    {
        private readonly IRepositoryService RepositoryService;
        public RepositoryApplicationService(IRepositoryService RepositoryService)
        {
            this.RepositoryService = RepositoryService;
        }

        public async Task<Repository> Add(Repository Repository)
        {
            await this.RepositoryService.Add(Repository);

            return Repository;
        }

        public Task<IList<Repository>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Repository>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Repository> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Repository> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Repository> Update(Repository obj)
        {
            throw new NotImplementedException();
        }
    }
}
