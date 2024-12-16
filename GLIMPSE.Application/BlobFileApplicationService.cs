using AutoMapper;
using GLIMPSE.Application.Dtos;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class BlobFileApplicationService : BaseApplicationService<BlobFile, BlobFileDTO>, IBlobFileApplicationService
    {
        public BlobFileApplicationService(IMapper mapper, IBlobFileService blobFileService): base(mapper, blobFileService)
        {
        }
    }
}
