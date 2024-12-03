using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetSingleVinylRecordForArtist;

public class GetSingleVinylRecordForArtistQuery : IRequest<VinylRecordVm>
{
    public Guid VinylRecordId { get; set; }

    public Guid ArtistId { get; set; }
}
