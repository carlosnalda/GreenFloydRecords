using CarlosNalda.GreenFloydRecords.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarlosNalda.GreenFloydRecords.WebApp.ViewModels
{
    public class VinylRecordUserInterfaceViewModel
    {
        public VinylRecordViewModel? VinylRecord { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> GenreList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ArtistList { get; set; }
    }
}
