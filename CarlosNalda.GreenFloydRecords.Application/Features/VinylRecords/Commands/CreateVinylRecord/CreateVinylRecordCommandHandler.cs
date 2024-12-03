using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Infrastructure;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.CreateVinylRecord
{
    public class CreateVinylRecordCommandHandler : IRequestHandler<CreateVinylRecordCommand, Guid>
    {
        private readonly IAsyncRepository<VinylRecord> _vinylRecordrepository;
        private readonly IAsyncRepository<Artist> _artistRepository;
        private readonly IAsyncRepository<Genre> _genreRepository;
        private readonly IMapper _mapper;
        private readonly IImageFileManager _imageFileManager;

        public CreateVinylRecordCommandHandler(IMapper mapper,
            IAsyncRepository<VinylRecord> vinylRecordrepository,
            IAsyncRepository<Artist> artistRepository,
            IAsyncRepository<Genre> genreRepository,
            IImageFileManager imageFileManager)
        {
            _mapper = mapper;
            _vinylRecordrepository = vinylRecordrepository;
            _artistRepository = artistRepository;
            _genreRepository = genreRepository;
            _imageFileManager = imageFileManager;
        }
        public async Task<Guid> Handle(CreateVinylRecordCommand request, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository
                .GetByIdAsync(request.ArtistId);

            if (artist == null)
            {
                throw new NotFoundException(nameof(artist), request.ArtistId);
            }

            var genre = await _genreRepository
                .GetByIdAsync(request.GenreId);

            if (genre == null)
            {
                throw new NotFoundException(nameof(genre), request.GenreId);
            }

            if (request.ImageStream != null)
            {
                request.ImageUrl = await _imageFileManager.UpsertFileAsync(request.ImageStream, request.ImageUrl);
            }

            var vinylRecord = _mapper.Map<VinylRecord>(request);
            vinylRecord = await _vinylRecordrepository.AddAsync(vinylRecord);
            return vinylRecord.Id;
        }
    }
}