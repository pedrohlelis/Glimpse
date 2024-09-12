using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {
        private readonly ITagRepository TagRepository;

        public TagService(ITagRepository TagRepository) : base(TagRepository)
        {
            this.TagRepository = TagRepository;
        }
    }
}