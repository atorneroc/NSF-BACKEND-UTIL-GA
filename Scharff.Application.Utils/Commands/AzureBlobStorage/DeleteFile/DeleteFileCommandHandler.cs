using MediatR;
using Scharff.Domain.Response.BlobStorage;
using Scharff.Infrastructure.AzureBlobStorage.Repositories.DeleteFile;

namespace Scharff.Application.Commands.AzureBlobStorage.DeleteFile
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, ResponseBlobStorage>
    {
        private readonly IDeleteFileRepository _deleteFile;

        public DeleteFileCommandHandler(IDeleteFileRepository deleteFile)
        {
            _deleteFile = deleteFile;
        }

        public async Task<ResponseBlobStorage> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var result = await _deleteFile.DeleteFile(request.BlobFileName ?? "");

            return result;
        }
    }
}
