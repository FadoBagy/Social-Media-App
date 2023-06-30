namespace Social_Media_App.Services.File
{
    public interface IFileService
    {
        public Task<string> SaveImage(IFormFile image, string userId);
    }
}
