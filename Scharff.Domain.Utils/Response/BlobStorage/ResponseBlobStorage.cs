using Scharff.Domain.Entities;

namespace Scharff.Domain.Response.BlobStorage
{
    public class ResponseBlobStorage
    {
        public string? Status { get; set; }
        public bool? Error { get; set; }
        public BlobStorageModel Blob { get; set; }
        public ResponseBlobStorage()
        {
            Blob = new BlobStorageModel();
        }
    }

}
