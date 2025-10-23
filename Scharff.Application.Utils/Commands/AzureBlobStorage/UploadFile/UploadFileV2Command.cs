using MediatR;
using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Application.Commands.AzureBlobStorage.UploadFile
{
    public class UploadFileV2Command : IRequest<List<ResponseBlobStorage>>
    {
        public List<AttachedFileModel>? File { get; set; }
        public string BlobContainerName { get; set; } = string.Empty;
        public string? BlobFolderName { get; set; } = string.Empty;
    }

    
}
