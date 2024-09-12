using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class LaneService : BaseService<Lane>, ILaneService
    {
        private readonly ILaneRepository LaneRepository;

        public LaneService(ILaneRepository LaneRepository) : base(LaneRepository)
        {
            this.LaneRepository = LaneRepository;
        }
    }
}