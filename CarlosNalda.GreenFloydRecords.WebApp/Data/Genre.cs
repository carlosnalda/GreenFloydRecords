using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarlosNalda.GreenFloydRecords.WebApp.Data
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
