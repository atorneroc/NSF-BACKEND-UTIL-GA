using MediatR;
using Scharff.Domain.Entities;
using Scharff.Infrastructure.AzureBlobStorage.Queries.GetAllFiles;

namespace Scharff.Application.Queries.AzureBlobStorage.GetAllFiles
{
    public class GetAllFilesV2Handler : IRequestHandler<GetAllFilesV2Query, List<BlobStorageModel>>
    {
        private readonly IGetAllFilesV2Query _getAllFiles;

        public GetAllFilesV2Handler(IGetAllFilesV2Query getAllFiles)
        {
            _getAllFiles = getAllFiles;
        }

        public async Task<List<BlobStorageModel>> Handle(GetAllFilesV2Query request, CancellationToken cancellationToken)
        {
            string folderName = string.IsNullOrEmpty(request.BlobFolderName) ? "" : $"{request.BlobFolderName}/";

            var result = await _getAllFiles.GetAllFilesV2(request.BlobContainerName, folderName);

            return result;
        }
    }
}
