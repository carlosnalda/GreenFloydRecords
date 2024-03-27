using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.UpdateVinylRecord;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.UpdateVinylRecord;

public class UpdateVinylRecordCommandHandler : IRequestHandler<UpdateVinylRecordCommand>
{
    private readonly IAsyncRepository<VinylRecord> _repository;
    private readonly IMapper _mapper;

    public UpdateVinylRecordCommandHandler(IMapper mapper, IAsyncRepository<VinylRecord> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task Handle(UpdateVinylRecordCommand request, CancellationToken cancellationToken)
    {
        var vinylRecordToUpdate = await _repository.GetByIdAsync(request.Id);
        if (vinylRecordToUpdate == null)
        {
            throw new NotFoundException(nameof(VinylRecord), request.Id);
        }

        _mapper.Map(request, vinylRecordToUpdate, typeof(UpdateVinylRecordCommand), typeof(Genre));

        await _repository.UpdateAsync(vinylRecordToUpdate);
    }
}