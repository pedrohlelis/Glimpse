using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace GLIMPSE.Domain.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository UserRepository;

        public UserService(IUserRepository UserRepository) : base(UserRepository)
        {
            this.UserRepository = UserRepository;
        }
    }
}