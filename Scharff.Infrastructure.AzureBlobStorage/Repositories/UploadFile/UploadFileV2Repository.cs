using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Scharff.Domain.Response.BlobStorage;

namespace Scharff.Infrastructure.AzureBlobStorage.Repositories.UploadFile
{
    public class UploadFileV2Repository : IUploadFileV2Repository
    {
        private readonly string _storageConnectionString;

        public UploadFileV2Repository(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString") ?? "";
        }

        public async Task<ResponseBlobStorage> UploadFileV2(IFormFile file, string storageContainerName, string folderName)
        {
            ResponseBlobStorage response = new();
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, storageContainerName);

            await container.CreateIfNotExistsAsync();
            try
            {
                BlobClient client = container.GetBlobClient(folderName + file.FileName);

                await using (Stream? data = file.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }

                response.Status = $"File {file.FileName} Uploaded Successfully";
                response.Error = false;
                response.Blob.Uri = client.Uri.AbsoluteUri;
                response.Blob.Name = file.FileName;
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
