using Microsoft.AspNetCore.Http;
using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Infrastructure.AzureBlobStorage.Repositories.UploadFile
{
    public interface IUploadFileRepository
    {
        Task<ResponseBlobStorage> UploadFile(IFormFile file);
    }
}
