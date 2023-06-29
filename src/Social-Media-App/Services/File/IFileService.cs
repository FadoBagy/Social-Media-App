namespace Social_Media_App.Services.File
{
    public interface IFileService
    {
        public string SaveImage(IFormFile image, string userId);
    }
}
