using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.CreateArtist
{
    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, Guid>
    {
        private readonly IArtistRepository _repository;
        private readonly IMapper _mapper;

        public CreateArtistCommandHandler(IMapper mapper, IArtistRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Guid> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
        {
            var artist = _mapper.Map<Artist>(request);
            artist = await _repository.AddAsync(artist);
            return artist.Id;
        }
    }
}