using CarlosNalda.GreenFloydRecords.WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var artist = GetAll<Artist>();
            return View(artist);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Artist artist)
        {
            if (!ModelState.IsValid)
                return View(artist);

            Add(artist);

            TempData["success"] = "Artist created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = GetById<Artist>(parsedId);

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

            var existingArtist = GetByIdAsNoTracking<Artist>(artist.Id);

            if (existingArtist == null)
                return NotFound();

            Update(artist);

            TempData["success"] = "Artist updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = GetById<Artist>(parsedId);

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

            var artist = GetByIdAsNoTracking<Artist>(parsedId);

            if (artist == null)
                return NotFound();

            Delete(artist);

            TempData["success"] = "Artist deleted successfully";
            return RedirectToAction("Index");
        }

        #region Refactored CRUD with clean code
        private IEnumerable<T> GetAll<T>()
            where T : class
        {
            IQueryable<T> query = _applicationDbcontext.Set<T>();
            return query.ToList();
        }

        public T? GetById<T>(Guid id)
            where T : class
        {
            T? t = _applicationDbcontext.Set<T>().Find(id);
            return t;
        }

        public T? GetByIdAsNoTracking<T>(Guid id)
           where T : class
        {
            T? t = _applicationDbcontext.Set<T>().Find(id);
            if (t != null)
            {
                _applicationDbcontext.Entry(t).State = EntityState.Detached;
            }

            return t;
        }

        public void Add<T>(T entity)
            where T : class
        {
            _applicationDbcontext.Add(entity);
            _applicationDbcontext.SaveChanges();
        }

        public void Update<T>(T entity)
            where T : class
        {
            _applicationDbcontext.Update(entity);
            _applicationDbcontext.SaveChanges();
        }

        public void Delete<T>(T entity)
            where T : class
        {
            _applicationDbcontext.Remove(entity);
            _applicationDbcontext.SaveChanges();
        }
        #endregion
    }
}
