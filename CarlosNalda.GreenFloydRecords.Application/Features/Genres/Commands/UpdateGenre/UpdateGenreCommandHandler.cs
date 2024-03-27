using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.UpdateGenre
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
    {
        private readonly IAsyncRepository<Genre> _repository;
        private readonly IMapper _mapper;

        public UpdateGenreCommandHandler(IMapper mapper, IAsyncRepository<Genre> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {

            var genreToUpdate = await _repository.GetByIdAsync(request.Id);
            if (genreToUpdate == null)
            {
                throw new NotFoundException(nameof(Genre), request.Id);
            }

            // var validator = new UpdateEventCommandValidator();
            // var validationResult = await validator.ValidateAsync(request);

            // if (validationResult.Errors.Count > 0) throw new ValidationException(validationResult);

            _mapper.Map(request, genreToUpdate, typeof(UpdateGenreCommand), typeof(Genre));

            await _repository.UpdateAsync(genreToUpdate);
        }
    }
}