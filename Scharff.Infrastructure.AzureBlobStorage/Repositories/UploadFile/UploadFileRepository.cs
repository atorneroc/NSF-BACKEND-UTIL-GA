using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Infrastructure.AzureBlobStorage.Repositories.UploadFile
{
    public class UploadFileRepository : IUploadFileRepository
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public UploadFileRepository(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString") ?? "";
            _storageContainerName = configuration.GetConnectionString("BlobContainerName") ?? "";
        }

        public async Task<ResponseBlobStorage> UploadFile(IFormFile file)
        {
            ResponseBlobStorage response = new();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            await container.CreateIfNotExistsAsync();
            try
            {
                BlobClient client = container.GetBlobClient(file.FileName);

                await using (Stream? data = file.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }

                response.Status = $"File {file.FileName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = client.Name;
            }
            catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                response.Status = $"File with name {file.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            catch (RequestFailedException ex)
            {
                response.Status = $"Unexpected error: {ex.StackTrace}.";
                response.Error = true;
                return response;
            }

            return response;
        }
    }
}
