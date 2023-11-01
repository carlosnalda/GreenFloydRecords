using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<IActionResult> Index()
        {
            var artist = await _artistRepository.ListAllAsync();
            return View(artist);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Artist artist)
        {
            if (!ModelState.IsValid)
                return View(artist);

            await _artistRepository.AddAsync(artist);

            TempData["success"] = "Artist created successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = await _artistRepository.GetByIdAsync(parsedId);

            if (artist == null)
                return NotFound();

            return View(artist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Artist artist)
        {
            if (!ModelState.IsValid)
                return View(artist);

            var existingArtist = await _artistRepository.GetByIdAsNoTrackingAsync(artist.Id);

            if (existingArtist == null)
                return NotFound();

            await _artistRepository.UpdateAsync(artist);

            TempData["success"] = "Artist updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = await _artistRepository.GetByIdAsync(parsedId);

            if (artist == null)
                return NotFound();

            return View(artist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWithPost(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = await _artistRepository.GetByIdAsNoTrackingAsync(parsedId);

            if (artist == null)
                return NotFound();

            await _artistRepository.DeleteAsync(artist);

            TempData["success"] = "Artist deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
