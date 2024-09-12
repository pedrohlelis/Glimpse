using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class RoleApplicationService : IBaseApplicationService<Role>, IRoleApplicationService
    {
        private readonly IRoleService RoleService;
        public RoleApplicationService(IRoleService RoleService)
        {
            this.RoleService = RoleService;
        }

        public async Task<Role> Add(Role Role)
        {
            await this.RoleService.Add(Role);

            return Role;
        }

        public Task<IList<Role>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Role>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Role> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Role> Update(Role obj)
        {
            throw new NotImplementedException();
        }
    }
}
