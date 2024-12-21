using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Domain.Services
{
    public class SprintService : BaseService<Sprint>, ISprintService
    {
        private readonly ISprintRepository SprintRepository;

        public SprintService(ISprintRepository SprintRepository) : base(SprintRepository)
        {
            this.SprintRepository = SprintRepository;
        }
    }
}
