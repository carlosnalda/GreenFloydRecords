using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.DeleteArtist
{
    public class DeleteArtistCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
