using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class CardApplicationService : BaseApplicationService<Card, CardDTO>, ICardApplicationService
    {
        public CardApplicationService(IMapper mapper, ICardService cardService): base(mapper, cardService)
        {
        }
    }
}
