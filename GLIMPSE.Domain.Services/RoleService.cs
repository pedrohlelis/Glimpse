using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly IRoleRepository RoleRepository;

        public RoleService(IRoleRepository RoleRepository) : base(RoleRepository)
        {
            this.RoleRepository = RoleRepository;
        }
    }
}