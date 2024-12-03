using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.PartialUpdateVinylRecord;

public class PartialUpdateVinylRecordCommand : IRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public decimal Rate { get; set; }

    public decimal Price { get; set; }

    public Guid GenreId { get; set; }

    public Genre Genre { get; set; }

    public Guid ArtistId { get; set; }

    public Artist Artist { get; set; }

    public string ImageUrl { get; set; }

    public Stream ImageStream { get; set; }
}
