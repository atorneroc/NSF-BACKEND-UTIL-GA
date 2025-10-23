using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.AzureBlobStorage.Queries.DownloadFile
{
    public class DownloadFileQuery : IDownloadFileQuery
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public DownloadFileQuery(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString") ?? "";
            _storageContainerName = configuration.GetConnectionString("BlobContainerName") ?? "";
        }

        public async Task<BlobStorageModel> DownloadFile(string blobFileName)
        {
            BlobContainerClient client = new(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient file = client.GetBlobClient(blobFileName);

                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;

                    var content = await file.DownloadContentAsync();
                    string name = blobFileName;
                    string contentType = content.Value.Details.ContentType;

                    return new BlobStorageModel
                    {
                        Content = blobContent,
                        Name = name,
                        ContentType = contentType
                    };
                }
                else
                {
                    throw new FileNotFoundException($"El archivo '{blobFileName}' no fue encontrado.");
                }
            }


            catch (RequestFailedException ex)
            when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                throw new FileNotFoundException($"El archivo '{blobFileName}' no fue encontrado.");
            }


        }
    }
}
