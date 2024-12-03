using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetVinylRecordList
{
    public class GetVinylRecordListForArtistQuery : IRequest<List<VinylRecordVm>>
    {
        public Guid ArtistId { get; set; }
    }
}
