using CarlosNalda.GreenFloydRecords.WebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ApplicationDbContext _applicationDbcontext;
        public ArtistController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbcontext = applicationDbContext;
        }

        public IActionResult Index()
        {
            var artist = _applicationDbcontext
                .Set<Artist>()
                .ToList();

            return View(artist);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Artist artist)
        {
            if (!ModelState.IsValid)
                return View(artist);

            _applicationDbcontext
                .Set<Artist>()
                .Add(artist);
            _applicationDbcontext.SaveChanges();

            TempData["success"] = "Artist created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = _applicationDbcontext
                .Artist
                .Find(parsedId);

            if (artist == null)
                return NotFound();

            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Artist artist)
        {
            if (!ModelState.IsValid)
                return View(artist);

            if (!_applicationDbcontext.Artist.Any(x => x.Id == artist.Id))
                return NotFound();


            _applicationDbcontext
                .Set<Artist>()
                .Update(artist);
            _applicationDbcontext.SaveChanges();

            TempData["success"] = "Artist updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = _applicationDbcontext
              .Artist
              .Find(parsedId);

            if (artist == null)
                return NotFound();

            return View(artist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWithPost(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = _applicationDbcontext
              .Artist
              .Find(parsedId);

            if (artist == null)
                return NotFound();

            _applicationDbcontext
                .Set<Artist>()
                .Remove(artist);
            _applicationDbcontext.SaveChanges();

            TempData["success"] = "Artist deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
