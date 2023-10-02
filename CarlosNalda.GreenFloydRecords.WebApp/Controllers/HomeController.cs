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
            IEnumerable<VinylRecord> list = GetAll<VinylRecord>(includeProperties: "Artist,Genre");

            return View(list);
        }

        private IEnumerable<T> GetAll<T>(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
              where T : class
        {
            IQueryable<T> query = _applicationDbcontext.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }
    }
}