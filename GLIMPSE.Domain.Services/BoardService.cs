using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class BoardService : BaseService<Board>, IBoardService
    {
        private readonly IBoardRepository BoardRepository;

        public BoardService(IBoardRepository BoardRepository) : base(BoardRepository)
        {
            this.BoardRepository = BoardRepository;
        }
    }
}
