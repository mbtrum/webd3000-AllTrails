using Azure.Storage.Blobs;

namespace AllTrails.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly IConfiguration _configuration;

        private readonly BlobServiceClient _blobServiceClient;
        private string containerName;

        // Constructor
        public AzureBlobService(IConfiguration configuration)
        {
            _configuration = configuration;

            // Get the container name from User Secrets
            containerName = _configuration["AZ_STORAGE_CONTAINER_NAME"];

            string connectionString = _configuration.GetConnectionString("AZ_STORAGE_CONNECTION_STRING");

            // Initialize BlobService Client
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string filename)
        {
            // 1. Create a container client for the container in Azure Storage (i.e., "uploads" or "photos")
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            // 2. Get the blob client
            var blobClient = containerClient.GetBlobClient(filename);

            // Upload the file
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.ToString(); // return the blob's URL from Azure Blob Storage
        }
    }
}
