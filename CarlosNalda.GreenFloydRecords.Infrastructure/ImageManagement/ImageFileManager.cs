using CarlosNalda.GreenFloydRecords.Application.Contracts.Infrastructure;
using System.Reflection;

namespace CarlosNalda.GreenFloydRecords.Infrastructure.ImageManagement
{
    public class ImageFileManager : IImageFileManager
    {
        public void Seed()
        {
            var defaultImagesDirectory = GetDefaultImagesDirectory();
            var files = GetImageFilesInfoFromDirectorty(defaultImagesDirectory);

            var imagesDirectory = GetImagesDirectory();
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }

            foreach (FileInfo file in files)
            {
                var newFile = $"{imagesDirectory}\\{file.Name}";
                if (File.Exists(newFile))
                    continue;

                File.Copy(file.FullName, newFile);
            }
        }

        public async Task<string> UpsertFileAsync(Stream file, string existingFileImageUrl)
        {
            var uploads = Path.Combine(GetWwwRootFolder(), ImageDirectoryPath.Production);
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            if (existingFileImageUrl != null)
            {
                DeleteFile(existingFileImageUrl);
            }

            string fileName = $"{Guid.NewGuid()}.png";
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStreams);
            }

            return $"{ImageDirectoryPath.ProductionUrl}{fileName}";
        }

        public void DeleteFile(string imageUrl)
        {
            var oldImagePath =
                $"{Path.Combine(GetWwwRootFolder(), ImageDirectoryPath.Production)}\\{Path.GetFileName(imageUrl)}";
            if (File.Exists(oldImagePath))
            {
                File.Delete(oldImagePath);
            }
        }

        private string GetDefaultImagesDirectory() => Path.Combine(GetWwwRootFolder(), ImageDirectoryPath.Default);

        private string GetImagesDirectory() => Path.Combine(GetWwwRootFolder(), ImageDirectoryPath.Production);

        private string GetWwwRootFolder()
        {
            var directoryNode = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var foundDirectory = new DirectoryInfo(directoryNode);

            // I changed this implementation to take web app wwwroot folder
            // to insert images on web app directory.
            while (foundDirectory.Name != "GreenFloydRecords")
            {
                directoryNode = Directory.GetParent(directoryNode).FullName;
                foundDirectory = new DirectoryInfo(directoryNode);
            }

            var specificDirectory = Directory
                .GetDirectories(directoryNode)
                .FirstOrDefault(directory => directory.Contains("CarlosNalda.GreenFloydRecords.WebApp"));

            var wwwRootDirectory = Path.Combine(specificDirectory, ImageDirectoryPath.wwwRoot);
            if (!Directory.Exists(wwwRootDirectory))
            {
                return string.Empty;
            }

            return wwwRootDirectory;
        }

        private string GetWwwRootFolderForWebApi()
        {
            var directoryNode = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var foundDirectory = new DirectoryInfo(directoryNode);
            while (foundDirectory.Name != "CarlosNalda.GreenFloydRecords.WebApp")
            {
                directoryNode = Directory.GetParent(directoryNode).FullName;
                foundDirectory = new DirectoryInfo(directoryNode);
            }

            var wwwRootDirectory = Path.Combine(foundDirectory.FullName, ImageDirectoryPath.wwwRoot);
            if (!Directory.Exists(wwwRootDirectory))
            {
                return string.Empty;
            }

            return wwwRootDirectory;
        }

        private List<FileInfo> GetImageFilesInfoFromDirectorty(string defaultImagesDirectory) => new DirectoryInfo(defaultImagesDirectory)
         .GetFiles("*.*")
         .Where(f => f.Name.EndsWith(".png"))
         .ToList();
    }
}
