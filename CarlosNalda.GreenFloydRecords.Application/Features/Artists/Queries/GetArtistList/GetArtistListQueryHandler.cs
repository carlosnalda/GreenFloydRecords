using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetArtistList
{
    public class GetArtistListQueryHandler : IRequestHandler<GetArtistListQuery, List<ArtistVm>>
    {
        private readonly IAsyncRepository<Artist> _repository;
        private readonly IMapper _mapper;

        public GetArtistListQueryHandler(IMapper mapper, IAsyncRepository<Artist> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<ArtistVm>> Handle(GetArtistListQuery request, CancellationToken cancellationToken)
        {
            var artists = (await _repository.ListAllAsync()).OrderBy(x => x.Name);
            return _mapper.Map<List<ArtistVm>>(artists);
        }
    }
}
