using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.DeleteGenre
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
    {
        private readonly IAsyncRepository<Genre> _repository;

        public DeleteGenreCommandHandler(IAsyncRepository<Genre> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genreToDelete = await _repository.GetByIdAsync(request.Id);

            if (genreToDelete == null)
            {
                throw new NotFoundException(nameof(Genre), request.Id);
            }

            await _repository.DeleteAsync(genreToDelete);
        }
    }
}
