using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Infrastructure.AzureBlobStorage.Repositories.DeleteFile
{
    public class DeleteFileRepository : IDeleteFileRepository
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public DeleteFileRepository(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString") ?? "";
            _storageContainerName = configuration.GetConnectionString("BlobContainerName") ?? "";
        }

        public async Task<ResponseBlobStorage> DeleteFile(string blobFileName)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            BlobClient file = client.GetBlobClient(blobFileName);

            try
            {
                await file.DeleteAsync();
            }

            catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                return new ResponseBlobStorage { Error = true, Status = $"File with name {blobFileName} not found." };
            }

            return new ResponseBlobStorage { Error = false, Status = $"File: {blobFileName} has been successfully deleted." };
        }
    }
}
