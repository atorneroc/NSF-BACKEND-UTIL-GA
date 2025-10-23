using MediatR;
using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Application.Commands.AzureBlobStorage.UploadFile
{
    public class UploadFileCommand : IRequest<List<ResponseBlobStorage>>
    {
        public List<AttachedFileModel>? File { get; set; }
    }

    public class AttachedFileModel
    {
        public int Size { get; set; }
        public string? File { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
