using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class TagApplicationService : IBaseApplicationService<Tag>, ITagApplicationService
    {
        private readonly ITagService tagService;
        public TagApplicationService(ITagService tagService)
        {
            this.tagService = tagService;
        }

        public async Task<Tag> Add(Tag Tag)
        {
            await this.tagService.Add(Tag);

            return Tag;
        }

        public Task<IList<Tag>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IList<Tag>> GetAllIgnoreFilters()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> Remove(int id)
        {
            var tag = await this.tagService.Remove(id);

            return tag;
        }

        public Task<Tag> Update(Tag obj)
        {
            throw new NotImplementedException();
        }
    }
}
