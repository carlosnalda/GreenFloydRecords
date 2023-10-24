using CarlosNalda.GreenFloydRecords.WebApp.Data;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Linq;
using CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence;

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