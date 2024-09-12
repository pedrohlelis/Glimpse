using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class CheckboxApplicationService : IBaseApplicationService<Checkbox>, ICheckboxApplicationService
    {
        private readonly ICheckboxService CheckboxService;
        public CheckboxApplicationService(ICheckboxService CheckboxService)
        {
            this.CheckboxService = CheckboxService;
        }

        public async Task<Checkbox> Add(Checkbox Checkbox)
        {
            await this.CheckboxService.Add(Checkbox);

            return Checkbox;
        }

        public Task<IList<Checkbox>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Checkbox>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Checkbox> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Checkbox> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Checkbox> Update(Checkbox obj)
        {
            throw new NotImplementedException();
        }
    }
}
