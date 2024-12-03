using CarlosNalda.GreenFloydRecords.Domain.Entities;

namespace CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence
{
    public interface IVinylRecordRepository : IAsyncRepository<VinylRecord>
    {
        Task<ICollection<VinylRecord>> ListForArtistAsync(Guid artistId);

        Task<VinylRecord?> SingleOrDefaultForArtistAsync(Guid artistId, Guid vinylRecordId);
    }
}
