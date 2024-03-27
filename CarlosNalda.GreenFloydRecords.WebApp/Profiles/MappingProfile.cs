using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.CreateArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.DeleteArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.UpdateArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.CreateGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.DeleteGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.UpdateGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.CreateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.DeleteVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.UpdateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;

namespace CarlosNalda.GreenFloydRecords.WebApp.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GenreViewModel, GenreVm>().ReverseMap();
            CreateMap<GenreViewModel, CreateGenreCommand>().ReverseMap();
            CreateMap<GenreViewModel, UpdateGenreCommand>().ReverseMap();
            CreateMap<GenreViewModel, DeleteGenreCommand>().ReverseMap();

            CreateMap<ArtistViewModel, ArtistVm>().ReverseMap();
            CreateMap<ArtistViewModel, CreateArtistCommand>().ReverseMap();
            CreateMap<ArtistViewModel, UpdateArtistCommand>().ReverseMap();
            CreateMap<ArtistViewModel, DeleteArtistCommand>().ReverseMap();

            CreateMap<VinylRecordViewModel, VinylRecordVm>().ReverseMap();
            CreateMap<VinylRecordViewModel, CreateVinylRecordCommand>().ReverseMap();
            CreateMap<VinylRecordViewModel, UpdateVinylRecordCommand>().ReverseMap();
            CreateMap<VinylRecordViewModel, DeleteVinylRecordCommand>().ReverseMap();
        }
    }
}
