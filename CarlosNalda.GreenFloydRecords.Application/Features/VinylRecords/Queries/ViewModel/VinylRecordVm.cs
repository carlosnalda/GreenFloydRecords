using CarlosNalda.GreenFloydRecords.Domain.Entities;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel
{
    public class VinylRecordVm
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
}
