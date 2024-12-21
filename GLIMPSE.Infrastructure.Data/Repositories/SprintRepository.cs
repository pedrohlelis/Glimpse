using GLIMPSE.Domain.Models;
using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Infrastructure.Data.Repositories
{
    public class SprintRepository : BaseRepository<Sprint>, ISprintRepository
    {
        private readonly GlimpseContext sqlContext;

        public SprintRepository(GlimpseContext _sqlContext) : base(_sqlContext)
        {
            this.sqlContext = _sqlContext;
        }

    }
}
