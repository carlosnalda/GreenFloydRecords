﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarlosNalda.GreenFloydRecords.WebApp.ViewModels
{
    public class VinylRecordViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Genre")]
        public Guid GenreId { get; set; }

        [ValidateNever]
        public GenreViewModel Genre { get; set; }

        [Display(Name = "Artist")]
        public Guid ArtistId { get; set; }

        [ValidateNever]
        public ArtistViewModel Artist { get; set; }

        [ValidateNever]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
    }
}
