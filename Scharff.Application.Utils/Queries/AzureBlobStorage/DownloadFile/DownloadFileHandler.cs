using MediatR;
using Scharff.Domain.Entities;
using Scharff.Infrastructure.AzureBlobStorage.Queries.DownloadFile;

namespace Scharff.Application.Queries.AzureBlobStorage.DownloadFile
{
    public class DownloadFileHandler : IRequestHandler<DownloadFileQuery, BlobStorageModel>
    {
        private readonly IDownloadFileQuery _downloadFile;

        public DownloadFileHandler(IDownloadFileQuery downloadFile)
        {
            _downloadFile = downloadFile;
        }
        public async Task<BlobStorageModel> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var result = await _downloadFile.DownloadFile(request.BlobFileName ?? "");

            return result;
        }
    }
}
