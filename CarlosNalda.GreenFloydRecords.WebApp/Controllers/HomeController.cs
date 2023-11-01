using Microsoft.AspNetCore.Mvc;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Domain.Entities;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVinylRecordRepository _vinylRecordRepository;

        public HomeController(IVinylRecordRepository vinylRecordRepository)
        {
            _vinylRecordRepository = vinylRecordRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<VinylRecord> list =
                await _vinylRecordRepository.ListAllAsync(includeProperties: "Artist,Genre");
            return View(list);
        }
    }
}