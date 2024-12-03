namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel
{
    public class ArtistDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? Formed { get; set; }

        public DateTime? Disbanded { get; set; }
    }
}
