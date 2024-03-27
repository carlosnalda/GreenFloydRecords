using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.CreateArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.UpdateArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.CreateGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.UpdateGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.CreateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.UpdateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;

namespace CarlosNalda.GreenFloydRecords.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreVm>().ReverseMap();
            CreateMap<Genre, CreateGenreCommand>().ReverseMap();
            CreateMap<Genre, UpdateGenreCommand>().ReverseMap();

            CreateMap<Artist, ArtistVm>().ReverseMap();
            CreateMap<Artist, CreateArtistCommand>().ReverseMap();
            CreateMap<Artist, UpdateArtistCommand>().ReverseMap();

            CreateMap<VinylRecord, VinylRecordVm>().ReverseMap();
            CreateMap<VinylRecord, CreateVinylRecordCommand>().ReverseMap();
            CreateMap<VinylRecord, UpdateVinylRecordCommand>().ReverseMap();
        }
    }
}
