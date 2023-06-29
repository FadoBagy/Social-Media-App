namespace Social_Media_App.Services.File
{
    using Microsoft.AspNetCore.Http;

    public class FileService : IFileService
    {
        public string SaveImage(IFormFile image, string userId)
        {
            // Create a dedicated folder for the user within the "uploads" folder if it doesn't exist
            var userFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", userId);
            if (!Directory.Exists(userFolderPath))
            {
                Directory.CreateDirectory(userFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            var filePath = Path.Combine(userFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
