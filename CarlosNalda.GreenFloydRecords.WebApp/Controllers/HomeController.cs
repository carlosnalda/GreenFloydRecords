using CarlosNalda.GreenFloydRecords.WebApp.Data;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Linq;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDbcontext;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext applicationDbcontext)
        {
            _logger = logger;
            _applicationDbcontext = applicationDbcontext;
        }

        public IActionResult Index()
        {
            IEnumerable<VinylRecord> list = _applicationDbcontext
                .VinylRecord
                .Include(v => v.Genre)
                .Include(v => v.Artist);

            return View(list);
        }
    }
}