using CarlosNalda.GreenFloydRecords.WebApp.Data;
using CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence;
using CarlosNalda.GreenFloydRecords.WebApp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepository.ListAllAsync();
            return View(genres);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (char.IsDigit(genre.Name.ToCharArray()[0]))
                ModelState.AddModelError("name", "Name can not start with digit.");

            if (!ModelState.IsValid)
                return View(genre);

            await _genreRepository.AddAsync(genre);

            TempData["success"] = "Genre created successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = await _genreRepository.GetByIdAsync(parsedId);

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Genre genre)
        {
            if (char.IsDigit(genre.Name.ToCharArray()[0]))
                ModelState.AddModelError("name", "Name can not start with digit.");

            if (!ModelState.IsValid)
                return View(genre);

            var existingGenre = _genreRepository.GetByIdAsNoTrackingAsync(genre.Id);

            if (existingGenre == null)
                return NotFound();

            await _genreRepository.UpdateAsync(genre);

            TempData["success"] = "Genre updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = await _genreRepository.GetByIdAsync(parsedId);

            if (genre == null)
                return NotFound();

            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWithPost(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = await _genreRepository.GetByIdAsNoTrackingAsync(parsedId);

            if (genre == null)
                return NotFound();

            await _genreRepository.DeleteAsync(genre);

            TempData["success"] = "Genre deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
