namespace CarlosNalda.GreenFloydRecords.Application.Contracts.Infrastructure
{
    public interface IImageFileManager
    {
        public void Seed();

        void DeleteFile(string imageUrl);

        Task<string> UpsertFileAsync(Stream file, string existingFileImageUrl);
    }
}