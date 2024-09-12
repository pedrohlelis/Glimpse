using GLIMPSE.Domain.Models;
using GLIMPSE.Infrastructure.Data.Context;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Infrastructure.Data.Repositories
{
    public class CheckboxRepository : BaseRepository<Checkbox>, ICheckboxRepository
    {
        private readonly GlimpseContext sqlContext;

        public CheckboxRepository(GlimpseContext _sqlContext) : base(_sqlContext)
        {
            this.sqlContext = _sqlContext;
        }

    }
}