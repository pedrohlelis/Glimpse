using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class LaneApplicationService : IBaseApplicationService<Lane>, ILaneApplicationService
    {
        private readonly ILaneService LaneService;
        public LaneApplicationService(ILaneService LaneService)
        {
            this.LaneService = LaneService;
        }

        public async Task<Lane> Add(Lane Lane)
        {
            await this.LaneService.Add(Lane);

            return Lane;
        }

        public Task<IList<Lane>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Lane>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Lane> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Lane> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Lane> Update(Lane obj)
        {
            throw new NotImplementedException();
        }
    }
}
