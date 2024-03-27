using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetGenreList
{
    public class GetGenreListQueryHandler : IRequestHandler<GetGenreListQuery, List<GenreVm>>
    {
        private readonly IAsyncRepository<Genre> _repository;
        private readonly IMapper _mapper;

        public GetGenreListQueryHandler(IMapper mapper, IAsyncRepository<Genre> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<GenreVm>> Handle(GetGenreListQuery request, CancellationToken cancellationToken)
        {
            var genres = (await _repository.ListAllAsync()).OrderBy(x => x.Name);
            return _mapper.Map<List<GenreVm>>(genres);
        }
    }
}
