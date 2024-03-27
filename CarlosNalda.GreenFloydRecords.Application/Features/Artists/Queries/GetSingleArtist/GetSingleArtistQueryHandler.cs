using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetSingleArtist;

public class GetSingleArtistQueryHandler : IRequestHandler<GetSingleArtistQuery, ArtistVm>
{
    private readonly IAsyncRepository<Artist> _repository;
    private readonly IMapper _mapper;

    public GetSingleArtistQueryHandler(IMapper mapper, IAsyncRepository<Artist> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ArtistVm> Handle(GetSingleArtistQuery request, CancellationToken cancellationToken)
    {
        var artist = await _repository.GetByIdAsync(request.Id);
        var artistVm = _mapper.Map<ArtistVm>(artist);

        if (artist == null)
        {
            throw new NotFoundException(nameof(Artist), request.Id);
        }

        return artistVm;
    }
}
