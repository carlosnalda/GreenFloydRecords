using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetSingleVinylRecord;

public class GetSingleVinylRecordQueryHandler : IRequestHandler<GetSingleVinylRecordQuery, VinylRecordVm>
{
    private readonly IAsyncRepository<VinylRecord> _repository;
    private readonly IMapper _mapper;

    public GetSingleVinylRecordQueryHandler(IMapper mapper, IAsyncRepository<VinylRecord> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<VinylRecordVm> Handle(GetSingleVinylRecordQuery request, CancellationToken cancellationToken)
    {
        var vinylRecord = await _repository.GetByIdAsync(request.Id);
        var vinylRecordVm = _mapper.Map<VinylRecordVm>(vinylRecord);

        if (vinylRecord == null)
        {
            throw new NotFoundException(nameof(VinylRecord), request.Id);
        }

        return vinylRecordVm;
    }
}
