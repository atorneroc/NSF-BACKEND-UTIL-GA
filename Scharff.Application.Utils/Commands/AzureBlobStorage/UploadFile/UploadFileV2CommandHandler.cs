using MediatR;
using Microsoft.AspNetCore.Http;
using Scharff.Domain.Response.BlobStorage;
using Scharff.Infrastructure.AzureBlobStorage.Repositories.UploadFile;

namespace Scharff.Application.Commands.AzureBlobStorage.UploadFile
{
    public class UploadFileV2CommandHandler : IRequestHandler<UploadFileV2Command, List<ResponseBlobStorage>>
    {
        private readonly IUploadFileV2Repository _uploadFile;

        public UploadFileV2CommandHandler(IUploadFileV2Repository uploadFile)
        {
            _uploadFile = uploadFile;
        }

        public async Task<List<ResponseBlobStorage>> Handle(UploadFileV2Command request, CancellationToken cancellationToken)
        {
            string folderName = string.IsNullOrEmpty(request.BlobFolderName) ?
                "" :
                $"{request.BlobFolderName}/";

            List<ResponseBlobStorage> response = new();
            if (request?.File != null)
            {
                foreach (var detail in request.File)
                {
                    byte[] bytes = Convert.FromBase64String(detail.File ?? "");
                    MemoryStream stream = new MemoryStream(bytes);

                    IFormFile file = new FormFile(stream, 0, bytes.Length, detail.Name ?? "", detail.Name ?? "");

                    var result = await _uploadFile.UploadFileV2(file, request.BlobContainerName, folderName);
                    response.Add(result);
                }
            }
            return response;
        }
    }
}
