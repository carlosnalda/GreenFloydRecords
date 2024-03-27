using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.DeleteGenre
{
    public class DeleteGenreCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
