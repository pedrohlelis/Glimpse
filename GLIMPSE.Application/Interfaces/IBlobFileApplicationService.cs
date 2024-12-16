using GLIMPSE.Domain.Models;
using Microsoft.AspNetCore.Http;
using GLIMPSE.Application.Dtos;

namespace GLIMPSE.Application.Interfaces
{
    public interface IBlobFileApplicationService : IBaseApplicationService<BlobFileDTO>
    {
    }
}
