using CarlosNalda.GreenFloydRecords.WebApp.Data;
using CarlosNalda.GreenFloydRecords.WebApp.ImageFileInitializer;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            VinylRecordVM VinylRecordVm = new()
            {
                VinylRecord = new(),
                GenreList = _applicationDbcontext
                  .Genre
                  .ToList()
                  .Select(genre => new SelectListItem
                  {
                      Value = genre.Id.ToString(),
                      Text = genre.Name,
                  }).ToList(),
                ArtistList = _applicationDbcontext
                  .Artist
                  .ToList()
                  .Select(artist => new SelectListItem
                  {
                      Value = artist.Id.ToString(),
                      Text = artist.Name,
                  }).ToList(),
            };

            if (string.IsNullOrEmpty(id))
            {
                return View(VinylRecordVm);
            }

            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();


            VinylRecordVm.VinylRecord = _applicationDbcontext
                  .VinylRecord
                  .FirstOrDefault(vinylRecord => vinylRecord.Id == parsedId);

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
                viewModel.GenreList = _applicationDbcontext
                    .Genre
                    .ToList()
                    .Select(genre => new SelectListItem
                    {
                        Value = genre.Id.ToString(),
                        Text = genre.Name,
                    }).ToList();

                viewModel.ArtistList = _applicationDbcontext
                    .Artist
                    .ToList()
                    .Select(artist => new SelectListItem
                    {
                        Value = artist.Id.ToString(),
                        Text = artist.Name,
                    }).ToList();

                return View(viewModel);
            }

            if (file != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\vinylRecord");
                var extension = Path.GetExtension(file.FileName);

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                if (viewModel.VinylRecord.ImageUrl != null)
                {
                    var oldImagePath = $"{Path.Combine(_hostEnvironment.WebRootPath, ImageDirectoryPath.Production)}\\{Path.GetFileName(viewModel.VinylRecord.ImageUrl)}";
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                viewModel.VinylRecord.ImageUrl = "/images/vinylRecord/" + fileName + extension;

            }

            if (viewModel.VinylRecord.Id == default)
            {
                _applicationDbcontext.VinylRecord.Add(viewModel.VinylRecord);
            }
            else
            {
                var entity = _applicationDbcontext.VinylRecord.FirstOrDefault(vinylRecord => vinylRecord.Id == viewModel.VinylRecord.Id);
                if (entity != null)
                {
                    entity.Name = viewModel.VinylRecord.Name;
                    entity.Description = viewModel.VinylRecord.Description;
                    entity.ReleaseDate = viewModel.VinylRecord.ReleaseDate;
                    entity.Rate = viewModel.VinylRecord.Rate;
                    entity.Price = viewModel.VinylRecord.Price;
                    entity.GenreId = viewModel.VinylRecord.GenreId;
                    entity.ArtistId = viewModel.VinylRecord.ArtistId;

                    if (entity.ImageUrl != null)
                    {
                        entity.ImageUrl = viewModel.VinylRecord.ImageUrl;
                    }
                }
            }

            _applicationDbcontext.SaveChanges();

            TempData["success"] = "Vinyl record created successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var vinylRecordList = _applicationDbcontext
                .VinylRecord
                .Include(x => x.Genre)
                .Include(x => x.Artist)
                .ToList();
            return Json(new { data = vinylRecordList });
        }

        //POST
        [HttpDelete]
        public IActionResult Delete(Guid? id)
        {
            var vinylRecord = _applicationDbcontext
                .VinylRecord
                .FirstOrDefault(vinylRecord => vinylRecord.Id == id);

            if (vinylRecord == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = $"{Path.Combine(_hostEnvironment.WebRootPath, ImageDirectoryPath.Production)}\\{Path.GetFileName(vinylRecord.ImageUrl)}";
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _applicationDbcontext.VinylRecord.Remove(vinylRecord);
            _applicationDbcontext.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
