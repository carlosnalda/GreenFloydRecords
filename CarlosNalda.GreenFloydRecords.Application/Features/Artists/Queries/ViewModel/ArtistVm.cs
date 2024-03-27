namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel
{
    public class ArtistVm
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? Formed { get; set; }

        public DateTime? Disbanded { get; set; }
    }
}
