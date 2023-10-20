using CarlosNalda.GreenFloydRecords.WebApp.Data;
using CarlosNalda.GreenFloydRecords.WebApp.ImageFileInitializer;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class VinylRecordController : Controller
    {
        private readonly ApplicationDbContext _applicationDbcontext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public VinylRecordController(ApplicationDbContext appTemplateContext,
            IWebHostEnvironment hostEnvironment)
        {
            _applicationDbcontext = appTemplateContext ?? throw new ArgumentNullException(nameof(appTemplateContext));
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(string? id)
        {
            VinylRecordVM VinylRecordVm = GetVinylRecordVm();

            if (string.IsNullOrEmpty(id))
            {
                return View(VinylRecordVm);
            }

            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            VinylRecordVm.VinylRecord = GetByIdAsNoTracking<VinylRecord>(parsedId);

            if (VinylRecordVm.VinylRecord == null)
            {
                return NotFound();
            }

            return View(VinylRecordVm);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(VinylRecordVM viewModel, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                viewModel.GenreList = GetAll<Genre>()
                  .Select(g => MutateToSelectListItem(g.Id, g.Name));
                viewModel.ArtistList = GetAll<Artist>()
                    .Select(a => MutateToSelectListItem(a.Id, a.Name)).ToList();

                return View(viewModel);
            }

            if (file != null)
            {
                viewModel.VinylRecord.ImageUrl = UpsertFile(file, viewModel.VinylRecord.ImageUrl);
            }

            if (viewModel.VinylRecord.Id == default)
            {
                Add(viewModel.VinylRecord);
            }
            else
            {
                var vinylRecord = GetByIdAsNoTracking<VinylRecord>(viewModel.VinylRecord.Id);
                if (vinylRecord == null)
                {
                    return NotFound();
                }

                Update(viewModel.VinylRecord);
            }

            TempData["success"] = "Vinyl record created successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var vinylRecordList = GetAll<VinylRecord>("Genre,Artist");
            return Json(new { data = vinylRecordList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var vinylRecord = GetById<VinylRecord>(id.GetValueOrDefault());

            if (vinylRecord == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            DeleteFile(vinylRecord.ImageUrl);
            Delete(vinylRecord);

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

        #region Refactored file manager
        private string UpsertFile(IFormFile? file, string imageUrl)
        {
            var uploads = Path.Combine(_hostEnvironment.WebRootPath, ImageDirectoryPath.Production);
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            if (imageUrl != null)
            {
                DeleteFile(imageUrl);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using (var fileStreams = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                file.CopyTo(fileStreams);
            }

            return $"{ImageDirectoryPath.ProductionUrl}/{fileName}";
        }

        private void DeleteFile(string imageUrl)
        {
            var oldImagePath =
                $"{Path.Combine(_hostEnvironment.WebRootPath, ImageDirectoryPath.Production)}\\{Path.GetFileName(imageUrl)}";
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }
        #endregion Refactored file manager

        #region Refactored CRUD with clean code
        private IEnumerable<T> GetAll<T>()
            where T : class
        {
            IQueryable<T> query = _applicationDbcontext.Set<T>();
            return query.ToList();
        }

        private IEnumerable<T> GetAll<T>(string? includeProperties = null)
           where T : class
        {
            IQueryable<T> query = _applicationDbcontext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
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

        #region Controller private methods
        private VinylRecordVM GetVinylRecordVm()
        {
            return new VinylRecordVM
            {
                VinylRecord = new(),
                GenreList = GetAll<Genre>()
                  .Select(g => MutateToSelectListItem(g.Id, g.Name)),
                ArtistList = GetAll<Artist>()
                  .Select(a => MutateToSelectListItem(a.Id, a.Name))
            };
        }

        private SelectListItem MutateToSelectListItem(Guid id, string text)
        {
            return new SelectListItem
            {
                Value = id.ToString(),
                Text = text,
            };
        }
        #endregion
    }
}
