using CarlosNalda.GreenFloydRecords.Domain.Entities;

namespace CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence
{
    public interface IArtistRepository : IAsyncRepository<Artist>
    {
        Task<Artist?> GetArtistWithChildEntitiesAsync(Guid id, string? includeProperties = null);
    }
}
