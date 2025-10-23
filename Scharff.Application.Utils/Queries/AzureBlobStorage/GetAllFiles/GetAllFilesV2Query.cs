using MediatR;
using Scharff.Domain.Entities;

namespace Scharff.Application.Queries.AzureBlobStorage.GetAllFiles
{
    public class GetAllFilesV2Query : IRequest<List<BlobStorageModel>>
    {
        public string BlobContainerName { get; set; } = string.Empty;
        public string? BlobFolderName { get; set; } = string.Empty;
    }
}
