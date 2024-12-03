using Microsoft.AspNetCore.Mvc;
using MediatR;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetVinylRecordList;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using AutoMapper;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public HomeController(IMapper mapper, 
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<VinylRecordVm> list =
                await _mediator.Send(new GetVinylRecordListQuery() { IncludeProperties = "Genre,Artist" });
            return View(_mapper.Map<IEnumerable<VinylRecordViewModel>>(list));
        }
    }
}