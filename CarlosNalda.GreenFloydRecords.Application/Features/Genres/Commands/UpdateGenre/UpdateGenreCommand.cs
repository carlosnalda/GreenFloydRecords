using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.UpdateGenre
{
    public class UpdateGenreCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
