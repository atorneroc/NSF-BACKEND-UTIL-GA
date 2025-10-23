using MediatR;
using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Application.Commands.AzureBlobStorage.DeleteFile
{
    public class DeleteFileCommand : IRequest<ResponseBlobStorage>
    {
        public string? BlobFileName { get; set; } = string.Empty;
    }
}