using CarlosNalda.GreenFloydRecords.WebApp.ImageFileInitializer;
using Microsoft.Extensions.Hosting;

namespace CarlosNalda.GreenFloydRecords.WebApp.Infrastructure.ImageManager
{
    public class ImageFileManager : IImageFileManager
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageFileManager(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string UpsertFile(IFormFile? file, string existingFileImageUrl)
        {
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, ImageDirectoryPath.Production);
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            if (existingFileImageUrl != null)
            {
                DeleteFile(existingFileImageUrl);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            return $"{ImageDirectoryPath.ProductionUrl}/{fileName}";
        }
        public void DeleteFile(string imageUrl)
        {
            var oldImagePath =
                $"{Path.Combine(_hostEnvironment.WebRootPath, ImageDirectoryPath.Production)}\\{Path.GetFileName(imageUrl)}";
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }
    }
}
