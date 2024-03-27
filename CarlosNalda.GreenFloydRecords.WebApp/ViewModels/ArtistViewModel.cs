using System.ComponentModel.DataAnnotations;

namespace CarlosNalda.GreenFloydRecords.WebApp.ViewModels
{
    public class ArtistViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? Formed { get; set; }

        public DateTime? Disbanded { get; set; }
    }
}
