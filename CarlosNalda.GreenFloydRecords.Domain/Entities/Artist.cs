using System.ComponentModel.DataAnnotations;

namespace CarlosNalda.GreenFloydRecords.Domain.Entities
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? Formed { get; set; }

        public DateTime? Disbanded { get; set; }

        public ICollection<VinylRecord> VinylRecords { get; set; } =
            new List<VinylRecord>();
    }
}
