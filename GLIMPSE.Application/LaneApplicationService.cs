using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class LaneApplicationService : BaseApplicationService<Lane, LaneDTO>, ILaneApplicationService
    {
        public LaneApplicationService(IMapper mapper, ILaneService laneService): base(mapper, laneService)
        {
        }
    }
}
