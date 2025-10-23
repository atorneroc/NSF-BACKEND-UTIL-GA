using MediatR;
using Microsoft.AspNetCore.Http;
using Scharff.Domain.Response.BlobStorage;
using Scharff.Infrastructure.AzureBlobStorage.Repositories.UploadFile;

namespace Scharff.Application.Commands.AzureBlobStorage.UploadFile
{
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, List<ResponseBlobStorage>>
    {
        private readonly IUploadFileRepository _uploadFile;

        public UploadFileCommandHandler(IUploadFileRepository uploadFile)
        {
            _uploadFile = uploadFile;
        }

        public async Task<List<ResponseBlobStorage>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            List<ResponseBlobStorage> response = new();
            if (request?.File != null)
            {
                foreach (var detail in request.File)
                {
                    byte[] bytes = Convert.FromBase64String(detail.File ?? "");
                    MemoryStream stream = new MemoryStream(bytes);

                    IFormFile file = new FormFile(stream, 0, bytes.Length, detail.Name ?? "", detail.Name ?? "");

                    var result = await _uploadFile.UploadFile(file);
                    response.Add(result);
                }
            }
            return response;
        }
    }
}
