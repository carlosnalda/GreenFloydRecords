using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetSingleArtist;

public class GetSingleArtistQuery : IRequest<ArtistVm>
{
    public Guid Id { get; set; }
}
