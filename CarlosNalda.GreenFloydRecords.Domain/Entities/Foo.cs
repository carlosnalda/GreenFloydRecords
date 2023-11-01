using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarlosNalda.GreenFloydRecords.Domain.Entities
{
    public class Foo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Genre Name")]
        public string Name { get; set; }
    }
}
