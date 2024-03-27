using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.UpdateArtist;

public class UpdateArtistCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public DateTime? Formed { get; set; }

    public DateTime? Disbanded { get; set; }
}
