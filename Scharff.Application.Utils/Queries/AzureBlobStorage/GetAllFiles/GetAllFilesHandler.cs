using MediatR;
using Scharff.Domain.Entities;
using Scharff.Infrastructure.AzureBlobStorage.Queries.GetAllFiles;

namespace Scharff.Application.Queries.AzureBlobStorage.GetAllFiles
{
    public class GetAllFilesHandler : IRequestHandler<GetAllFilesQuery, List<BlobStorageModel>>
    {
        private readonly IGetAllFilesQuery _getAllFiles;

        public GetAllFilesHandler(IGetAllFilesQuery getAllFiles)
        {
            _getAllFiles = getAllFiles;
        }

        public async Task<List<BlobStorageModel>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {
            var result = await _getAllFiles.GetAllFiles();

            return result;
        }
    }
}
