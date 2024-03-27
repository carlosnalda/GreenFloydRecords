using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetSingleVinylRecord;

public class GetSingleVinylRecordQuery : IRequest<VinylRecordVm>
{
    public Guid Id { get; set; }
}
