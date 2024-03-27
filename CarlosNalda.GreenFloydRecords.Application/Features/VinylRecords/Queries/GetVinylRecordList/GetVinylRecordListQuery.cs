using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetVinylRecordList
{
    public class GetVinylRecordListQuery : IRequest<List<VinylRecordVm>>
    {
        public string? includeProperties { get; set; }
    }
}
