using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetSingleVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetSingleVinylRecordForArtist;

public class GetSingleVinylRecordForArtistQueryHandler : IRequestHandler<GetSingleVinylRecordForArtistQuery, VinylRecordVm>
{
    private readonly IVinylRecordRepository _vinylRecordRepository;
    private readonly IAsyncRepository<Artist> _artistRepository;
    private readonly IMapper _mapper;

    public GetSingleVinylRecordForArtistQueryHandler(IMapper mapper,
        IVinylRecordRepository vinylRecordRepository,
        IAsyncRepository<Artist> artistRepository)
    {
        _mapper = mapper;
        _vinylRecordRepository = vinylRecordRepository;
        _artistRepository = artistRepository;
    }

    public async Task<VinylRecordVm> Handle(GetSingleVinylRecordForArtistQuery request, CancellationToken cancellationToken)
    {

        var artist = await _artistRepository
                .GetByIdAsync(request.ArtistId);

        if (artist == null)
        {
            throw new NotFoundException(nameof(artist), request.ArtistId);
        }

        var vinylRecordForArtist = await _vinylRecordRepository
                   .SingleOrDefaultForArtistAsync(request.ArtistId, request.VinylRecordId);

        var vinylRecordVm = _mapper.Map<VinylRecordVm>(vinylRecordForArtist);
        return vinylRecordVm;
    }
}
