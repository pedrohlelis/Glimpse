using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

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