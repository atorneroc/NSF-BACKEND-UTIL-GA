using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.AzureBlobStorage.Queries.GetAllFiles
{
    public interface IGetAllFilesQuery
    {
        Task<List<BlobStorageModel>> GetAllFiles();
    }
}
