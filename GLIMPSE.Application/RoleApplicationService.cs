using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class RoleApplicationService : BaseApplicationService<Role, RoleDTO>, IRoleApplicationService
    {
        public RoleApplicationService(IMapper mapper, IRoleService roleService): base(mapper, roleService)
        {
        }
    }
}
