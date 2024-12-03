using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetSingleArtistWithChildEntities;

public class GetSingleArtistWithChildEntitiesQuery : IRequest<ArtistVm>
{
    public Guid Id { get; set; }

    public string? IncludeProperties { get; set; }
}
