using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.CreateVinylRecord
{
    public class CreateVinylRecordCommand : IRequest<Guid>
    {
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
}