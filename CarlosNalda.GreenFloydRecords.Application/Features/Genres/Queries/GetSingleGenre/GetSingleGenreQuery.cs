using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetSingleGenre;

public class GetSingleGenreQuery : IRequest<GenreVm>
{
    public Guid Id { get; set; }
}
