namespace AllTrails.Services
{
    public interface IAzureBlobService
    {
        public Task<string> UploadFileAsync(IFormFile file, string filename);
    }
}
