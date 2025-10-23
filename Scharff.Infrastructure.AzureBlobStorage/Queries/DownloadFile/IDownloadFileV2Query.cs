using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.AzureBlobStorage.Queries.DownloadFile
{
    public interface IDownloadFileV2Query
    {
        Task<BlobStorageModel> DownloadFileV2(string blobFileName, string storageContainerName, string folderName);
    }
}
