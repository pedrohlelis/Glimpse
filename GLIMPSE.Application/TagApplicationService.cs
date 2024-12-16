using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class TagApplicationService : BaseApplicationService<Tag, TagDTO>, ITagApplicationService
    {
       public TagApplicationService(IMapper mapper, ITagService tagService): base(mapper, tagService)
        {
        }
    }
}
