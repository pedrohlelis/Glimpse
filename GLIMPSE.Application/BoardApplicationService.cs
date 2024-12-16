using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class BoardApplicationService : BaseApplicationService<Board, BoardDTO>, IBoardApplicationService
    {
        public BoardApplicationService(IMapper mapper, IBoardService boardService): base(mapper, boardService)
        {
        }
    }
}
