namespace CarlosNalda.GreenFloydRecords.WebApp.Infrastructure.ImageManager
{
    public interface IImageFileManager
    {
        void DeleteFile(string imageUrl);
        string UpsertFile(IFormFile? file, string existingFileImageUrl);
    }
}