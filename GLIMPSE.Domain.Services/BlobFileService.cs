using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;

namespace GLIMPSE.Domain.Services
{
    public class BlobFileService : BaseService<BlobFile>, IBlobFileService
    {
        private readonly IBlobFileRepository BlobFileRepository;

        public BlobFileService(IBlobFileRepository BlobFileRepository) : base(BlobFileRepository)
        {
            this.BlobFileRepository = BlobFileRepository;
        }
    }
}
