using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetVinylRecordList
{
    public class GetVinylRecordListQueryHandler : IRequestHandler<GetVinylRecordListQuery, List<VinylRecordVm>>
    {
        private readonly IAsyncRepository<VinylRecord> _repository;
        private readonly IMapper _mapper;

        public GetVinylRecordListQueryHandler(IMapper mapper, IAsyncRepository<VinylRecord> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<VinylRecordVm>> Handle(GetVinylRecordListQuery request, CancellationToken cancellationToken)
        {
            var vinylRecords = (await _repository.ListAllAsync(request.IncludeProperties)).OrderBy(x => x.Name);
            return _mapper.Map<List<VinylRecordVm>>(vinylRecords);
        }
    }
}
