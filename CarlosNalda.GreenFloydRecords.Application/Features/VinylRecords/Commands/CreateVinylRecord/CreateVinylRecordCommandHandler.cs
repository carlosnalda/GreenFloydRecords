using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.CreateVinylRecord
{
    public class CreateVinylRecordCommandHandler : IRequestHandler<CreateVinylRecordCommand, Guid>
    {
        private readonly IVinylRecordRepository _repository;
        private readonly IMapper _mapper;

        public CreateVinylRecordCommandHandler(IMapper mapper, IVinylRecordRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Guid> Handle(CreateVinylRecordCommand request, CancellationToken cancellationToken)
        {
            var vinylRecord = _mapper.Map<VinylRecord>(request);
            vinylRecord = await _repository.AddAsync(vinylRecord);
            return vinylRecord.Id;
        }
    }
}