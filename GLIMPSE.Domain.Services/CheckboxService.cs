using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class CheckboxService : BaseService<Checkbox>, ICheckboxService
    {
        private readonly ICheckboxRepository CheckboxRepository;

        public CheckboxService(ICheckboxRepository CheckboxRepository) : base(CheckboxRepository)
        {
            this.CheckboxRepository = CheckboxRepository;
        }
    }
}
