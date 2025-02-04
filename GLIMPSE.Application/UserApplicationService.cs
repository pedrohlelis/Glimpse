using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class UserApplicationService(IMapper mapper, IUserService userService) : BaseApplicationService<User, UserDTO>(mapper, userService), IUserApplicationService
    {
    }
}

