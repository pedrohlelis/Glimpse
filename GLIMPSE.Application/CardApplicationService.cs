using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class CardApplicationService : IBaseApplicationService<Card>, ICardApplicationService
    {
        private readonly ICardService CardService;
        public CardApplicationService(ICardService CardService)
        {
            this.CardService = CardService;
        }

        public async Task<Card> Add(Card Card)
        {
            await this.CardService.Add(Card);

            return Card;
        }

        public Task<IList<Card>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Card>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Card> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Card> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Card> Update(Card obj)
        {
            throw new NotImplementedException();
        }
    }
}
