using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Infrastructure.AzureBlobStorage.Repositories.DeleteFile
{
    public interface IDeleteFileRepository
    {
        Task<ResponseBlobStorage> DeleteFile(string blobFileName);
    }
}
