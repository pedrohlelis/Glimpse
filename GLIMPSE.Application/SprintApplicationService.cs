using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Application
{
    public class SprintApplicationService : BaseApplicationService<Sprint, SprintDTO>, ISprintApplicationService
    {
        public SprintApplicationService(IMapper mapper, ISprintService SprintService) : base(mapper, SprintService)
        {
        }
    }
}
