using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarlosNalda.GreenFloydRecords.Domain.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Genre Name")]
        public string Name { get; set; }
    }
}
