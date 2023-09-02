using CarlosNalda.GreenFloydRecords.WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _applicationDbcontext;
        public GenreController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbcontext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var genres = _applicationDbcontext
                .Set<Genre>()
                .ToList();

            return View(genres);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre genre)
        {
            if (char.IsDigit(genre.Name.ToCharArray()[0]))
                ModelState.AddModelError("name", "Name can not start with digit.");

            if (!ModelState.IsValid)
                return View(genre);

            _applicationDbcontext
                .Set<Genre>()
                .Add(genre);
            _applicationDbcontext.SaveChanges();

            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = _applicationDbcontext
                .Genre
                .Find(parsedId);

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre genre)
        {
            if (char.IsDigit(genre.Name.ToCharArray()[0]))
                ModelState.AddModelError("name", "Name can not start with digit.");

            if (!ModelState.IsValid)
                return View(genre);

            if (!_applicationDbcontext.Genre.Any(x => x.Id == genre.Id))
                return NotFound();


            _applicationDbcontext
                .Set<Genre>()
                .Update(genre);
            _applicationDbcontext.SaveChanges();

            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = _applicationDbcontext
              .Genre
              .Find(parsedId);

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWithPost(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = _applicationDbcontext
              .Genre
              .Find(parsedId);

            if (genre == null)
                return NotFound();

            _applicationDbcontext
                .Set<Genre>()
                .Remove(genre);
            _applicationDbcontext.SaveChanges();

            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
