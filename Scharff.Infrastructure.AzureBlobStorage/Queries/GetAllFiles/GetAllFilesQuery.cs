using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Scharff.Domain.Entities;

namespace Scharff.Infrastructure.AzureBlobStorage.Queries.GetAllFiles
{
    public class GetAllFilesQuery : IGetAllFilesQuery
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;

        public GetAllFilesQuery(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("BlobConnectionString") ?? "";
            _storageContainerName = configuration.GetConnectionString("BlobContainerName") ?? ""  ;
        }
        public async Task<List<BlobStorageModel>> GetAllFiles()
        {
            BlobContainerClient container = new(_storageConnectionString, _storageContainerName);
            List<BlobStorageModel> files = new();

            await foreach (BlobItem file in container.GetBlobsAsync())
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
