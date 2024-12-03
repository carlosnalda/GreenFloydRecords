using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetSingleArtistWithChildEntities;

public class GetSingleArtistWithChildEntitiesQueryHandler : IRequestHandler<GetSingleArtistWithChildEntitiesQuery, ArtistVm>
{
    private readonly IArtistRepository _repository;
    private readonly IMapper _mapper;

    public GetSingleArtistWithChildEntitiesQueryHandler(IMapper mapper, IArtistRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ArtistVm> Handle(GetSingleArtistWithChildEntitiesQuery request, CancellationToken cancellationToken)
    {
        var artist = await _repository.GetArtistWithChildEntitiesAsync(request.Id, request.IncludeProperties);

        if (artist == null)
        {
            throw new NotFoundException(nameof(Artist), request.Id);
        }

        var artistVm = _mapper.Map<ArtistVm>(artist);
        return artistVm;
    }
}
