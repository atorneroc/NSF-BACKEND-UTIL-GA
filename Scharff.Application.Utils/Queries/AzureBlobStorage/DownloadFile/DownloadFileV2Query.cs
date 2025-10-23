using MediatR;
using Scharff.Domain.Entities;

namespace Scharff.Application.Queries.AzureBlobStorage.DownloadFile
{
    public class DownloadFileV2Query : IRequest<BlobStorageModel>
    {
        public string? BlobFileName { get; set; } = string.Empty;
        public string BlobContainerName { get; set; } = string.Empty;
        public string? BlobFolderName { get; set; } = string.Empty;
    }
}
