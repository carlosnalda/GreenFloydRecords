using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetVinylRecordList;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetVinylRecordListForArtist
{
    public class GetVinylRecordListQueryForArtistHandler : IRequestHandler<GetVinylRecordListForArtistQuery, List<VinylRecordVm>>
    {
        private readonly IVinylRecordRepository _vinylRecordRepository;
        private readonly IAsyncRepository<Artist> _artistRepository;
        private readonly IMapper _mapper;

        public GetVinylRecordListQueryForArtistHandler(IMapper mapper,
            IVinylRecordRepository vinylRecordRepository,
            IAsyncRepository<Artist> artistRepository)
        {
            _mapper = mapper;
            _vinylRecordRepository = vinylRecordRepository;
            _artistRepository = artistRepository;
        }

        public async Task<List<VinylRecordVm>> Handle(GetVinylRecordListForArtistQuery request, CancellationToken cancellationToken)
        {
            // var artist = await _artistRepository
            //    .GetByIdAsNoTrackingAsync(request.ArtistId);
            var artist = await _artistRepository
               .GetByIdAsync(request.ArtistId);

            if (artist == null)
            {
                throw new NotFoundException(nameof(artist), request.ArtistId);
            }

            var vinylRecordForArtist = (await _vinylRecordRepository
                    .ListForArtistAsync(request.ArtistId))
                        .OrderBy(x => x.Name);

            return _mapper.Map<List<VinylRecordVm>>(vinylRecordForArtist);
        }
    }
}
