using MediatR;
using Scharff.Domain.Entities;
using Scharff.Infrastructure.AzureBlobStorage.Queries.DownloadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Application.Queries.AzureBlobStorage.DownloadFile
{
    public class DownloadFileV2Handler : IRequestHandler<DownloadFileV2Query, BlobStorageModel>
    {
        private readonly IDownloadFileV2Query _downloadFile;

        public DownloadFileV2Handler(IDownloadFileV2Query downloadFile)
        {
            _downloadFile = downloadFile;
        }
        public async Task<BlobStorageModel> Handle(DownloadFileV2Query request, CancellationToken cancellationToken)
        {          

            string folderName = string.IsNullOrEmpty(request.BlobFolderName) ?
                "" :
                $"{request.BlobFolderName}/";
            

            var result = await _downloadFile.DownloadFileV2(request.BlobFileName ?? "", request.BlobContainerName, folderName);

            return result;
        }

    }
}
