using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class UserApplicationService : IBaseApplicationService<User>, IUserApplicationService
    {
        private readonly IUserService UserService;
        public UserApplicationService(IUserService UserService)
        {
            this.UserService = UserService;
        }

        public async Task<User> Add(User User)
        {
            await this.UserService.Add(User);

            return User;
        }

        public Task<IList<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<User>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
