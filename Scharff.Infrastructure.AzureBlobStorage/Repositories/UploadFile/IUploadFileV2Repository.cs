using Microsoft.AspNetCore.Http;
using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Infrastructure.AzureBlobStorage.Repositories.UploadFile
{
    public interface IUploadFileV2Repository
    {
        Task<ResponseBlobStorage> UploadFileV2(IFormFile file, string storageContainerName, string folderName);
    }
}
