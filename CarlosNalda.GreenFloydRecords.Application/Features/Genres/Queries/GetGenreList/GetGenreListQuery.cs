using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetGenreList
{
    public class GetGenreListQuery : IRequest<List<GenreVm>>
    {
    }
}
