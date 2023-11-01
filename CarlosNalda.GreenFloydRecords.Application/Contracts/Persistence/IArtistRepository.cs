using CarlosNalda.GreenFloydRecords.Domain.Entities;

namespace CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence
{
    public interface IArtistRepository : IAsyncRepository<Artist>
    {
    }
}
