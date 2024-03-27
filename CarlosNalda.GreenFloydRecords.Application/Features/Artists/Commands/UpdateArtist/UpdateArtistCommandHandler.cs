using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.UpdateArtist;

public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand>
{
    private readonly IAsyncRepository<Artist> _repository;
    private readonly IMapper _mapper;

    public UpdateArtistCommandHandler(IMapper mapper, IAsyncRepository<Artist> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {

        var artistToUpdate = await _repository.GetByIdAsync(request.Id);
        if (artistToUpdate == null)
        {
            throw new NotFoundException(nameof(Artist), request.Id);
        }

        _mapper.Map(request, artistToUpdate, typeof(UpdateArtistCommand), typeof(Genre));

        await _repository.UpdateAsync(artistToUpdate);
    }
}