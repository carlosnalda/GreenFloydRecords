using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.CreateGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.ViewModel;

namespace CarlosNalda.GreenFloydRecords.Api.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateGenreCommand, GenreVm>();
        }
    }
}
