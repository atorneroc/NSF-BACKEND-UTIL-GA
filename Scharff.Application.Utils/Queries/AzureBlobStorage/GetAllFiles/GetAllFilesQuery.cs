using MediatR;
using Scharff.Domain.Entities;

namespace Scharff.Application.Queries.AzureBlobStorage.GetAllFiles
{
    public class GetAllFilesQuery : IRequest<List<BlobStorageModel>>
    {
    }
}
