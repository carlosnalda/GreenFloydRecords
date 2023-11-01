namespace CarlosNalda.GreenFloydRecords.Application.Contracts.Infrastructure
{
    public interface IImageFileManager
    {
        public void Seed();

        void DeleteFile(string imageUrl);

        string UpsertFile(MemoryStream file, string existingFileImageUrl);
    }
}