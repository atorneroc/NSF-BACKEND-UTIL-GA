using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.AzureBlobStorage.Queries.DownloadFile
{
    public class DownloadFileV2Query : IDownloadFileV2Query
    {
        private readonly string _storageConnectionString;

        public DownloadFileV2Query(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString") ?? "";
        }

        public async Task<BlobStorageModel> DownloadFileV2(string blobFileName, string storageContainerName, string folderName)
        {
            BlobContainerClient client = new(_storageConnectionString, storageContainerName);

            try
            {
                BlobClient file = client.GetBlobClient(folderName + blobFileName);

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
