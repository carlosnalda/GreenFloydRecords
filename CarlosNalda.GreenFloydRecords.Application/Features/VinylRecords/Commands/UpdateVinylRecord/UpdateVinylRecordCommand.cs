using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.UpdateVinylRecord;

public class UpdateVinylRecordCommand : IRequest
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
}
