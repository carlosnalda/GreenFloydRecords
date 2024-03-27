using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.DeleteArtist
{
    public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand>
    {
        private readonly IAsyncRepository<Artist> _repository;

        public DeleteArtistCommandHandler(IAsyncRepository<Artist> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
        {
            var artistToDelete = await _repository.GetByIdAsync(request.Id);

            if (artistToDelete == null)
            {
                throw new NotFoundException(nameof(Artist), request.Id);
            }

            await _repository.DeleteAsync(artistToDelete);
        }
    }
}
