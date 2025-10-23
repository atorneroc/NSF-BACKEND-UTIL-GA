using MediatR;
using Scharff.Domain.Entities;

namespace Scharff.Application.Queries.AzureBlobStorage.DownloadFile
{
    public class DownloadFileQuery : IRequest<BlobStorageModel>
    {
        public string? BlobFileName { get; set; } = string.Empty;
    }
}
