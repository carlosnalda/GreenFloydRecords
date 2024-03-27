using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.DeleteVinylRecord
{
    public class DeleteVinylRecordCommandHandler : IRequestHandler<DeleteVinylRecordCommand>
    {
        private readonly IAsyncRepository<VinylRecord> _repository;

        public DeleteVinylRecordCommandHandler(IAsyncRepository<VinylRecord> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteVinylRecordCommand request, CancellationToken cancellationToken)
        {
            var vinylRecordToDelete = await _repository.GetByIdAsync(request.Id);

            if (vinylRecordToDelete == null)
            {
                throw new NotFoundException(nameof(VinylRecord), request.Id);
            }

            await _repository.DeleteAsync(vinylRecordToDelete);
        }
    }
}
