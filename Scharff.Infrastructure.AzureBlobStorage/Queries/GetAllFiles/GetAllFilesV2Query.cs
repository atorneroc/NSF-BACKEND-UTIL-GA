using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.AzureBlobStorage.Queries.GetAllFiles
{
    public class GetAllFilesV2Query : IGetAllFilesV2Query
    {
        private readonly string _storageConnectionString;

        public GetAllFilesV2Query(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString") ?? "";
        }
        public async Task<List<BlobStorageModel>> GetAllFilesV2(string storageContainerName, string folderName)
        {
            BlobContainerClient container = new(_storageConnectionString, storageContainerName);
            List<BlobStorageModel> files = new();

            await foreach (BlobItem file in container.GetBlobsAsync(prefix: folderName))
            {
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = Path.Combine(uri, name);

                files.Add(new BlobStorageModel
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            return files;
        }
    }
}
