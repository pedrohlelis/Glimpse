using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class BoardApplicationService : IBaseApplicationService<Board>, IBoardApplicationService
    {
        private readonly IBoardService BoardService;
        public BoardApplicationService(IBoardService BoardService)
        {
            this.BoardService = BoardService;
        }

        public async Task<Board> Add(Board Board)
        {
            await this.BoardService.Add(Board);

            return Board;
        }

        public Task<IList<Board>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Board>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Board> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Board> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Board> Update(Board obj)
        {
            throw new NotImplementedException();
        }
    }
}
