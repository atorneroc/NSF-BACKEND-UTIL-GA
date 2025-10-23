using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.AzureBlobStorage.Queries.DownloadFile
{
    public interface IDownloadFileQuery
    {
        Task<BlobStorageModel> DownloadFile(string blobFileName);
    }
}
