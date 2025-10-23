using Scharff.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scharff.Infrastructure.AzureBlobStorage.Queries.GetAllFiles
{
    public interface IGetAllFilesV2Query
    {
        Task<List<BlobStorageModel>> GetAllFilesV2(string storageContainerName, string folderName);
    }
}
