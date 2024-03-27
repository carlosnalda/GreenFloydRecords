using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.CreateArtist
{
    public class CreateArtistCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public DateTime? Formed { get; set; }

        public DateTime? Disbanded { get; set; }
    }
}