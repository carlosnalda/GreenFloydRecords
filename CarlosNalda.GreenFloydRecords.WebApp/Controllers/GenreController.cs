using CarlosNalda.GreenFloydRecords.WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var genres = GetAll<Genre>();
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

            Add(genre);

            TempData["success"] = "Genre created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = GetById<Genre>(parsedId);

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

            var existingGenre = GetByIdAsNoTracking<Genre>(genre.Id);

            if (existingGenre == null)
                return NotFound();

            Update(genre);

            TempData["success"] = "Genre updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = GetById<Genre>(parsedId);

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

            var genre = GetByIdAsNoTracking<Genre>(parsedId);

            if (genre == null)
                return NotFound();

            Delete(genre);

            TempData["success"] = "Genre deleted successfully";
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
