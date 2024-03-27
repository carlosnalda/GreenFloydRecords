using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.DeleteVinylRecord
{
    public class DeleteVinylRecordCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
