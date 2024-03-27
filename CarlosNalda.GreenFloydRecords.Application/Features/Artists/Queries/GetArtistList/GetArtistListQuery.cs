using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetArtistList
{
    public class GetArtistListQuery : IRequest<List<ArtistVm>>
    {
    }
}
