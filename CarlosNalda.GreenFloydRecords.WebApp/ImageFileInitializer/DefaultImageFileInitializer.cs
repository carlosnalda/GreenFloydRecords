using Microsoft.Extensions.Hosting;

namespace CarlosNalda.GreenFloydRecords.WebApp.ImageFileInitializer
{
    public class DefaultImageFileInitializer : IDefaultImageFileInitializer
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public DefaultImageFileInitializer(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public void Initialize()
        {
            var defaultImagesDirectory = GetDefaultImagesDirectory();
            var files = GetImageFilesInfoFromDirectorty(defaultImagesDirectory);

            var imagesDirectory = GetImagesDirectory();
            foreach (FileInfo file in files)
            {
                var newFile = $"{imagesDirectory}\\{file.Name}";
                if (File.Exists(newFile))
                    continue;

                File.Copy(file.FullName, newFile);
            }
        }

        private string GetDefaultImagesDirectory() => Path.Combine(_hostEnvironment.WebRootPath, ImageDirectoryPath.Default);

        private string GetImagesDirectory() => Path.Combine(_hostEnvironment.WebRootPath, ImageDirectoryPath.Production);

        private List<FileInfo> GetImageFilesInfoFromDirectorty(string defaultImagesDirectory) =>
            new DirectoryInfo(defaultImagesDirectory)
                .GetFiles("*.*")
                .Where(f => f.Name.EndsWith(".png"))
                .ToList();
    }
}
