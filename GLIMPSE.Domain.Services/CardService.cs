using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class CardService : BaseService<Card>, ICardService
    {
        private readonly ICardRepository CardRepository;

        public CardService(ICardRepository CardRepository) : base(CardRepository)
        {
            this.CardRepository = CardRepository;
        }
    }
}