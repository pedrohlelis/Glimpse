using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class UserApplicationService : BaseApplicationService<User, UserDTO>, IUserApplicationService
    {
        public UserApplicationService(IMapper mapper, IUserService userService): base(mapper, userService)
        {
        }
    }
}
