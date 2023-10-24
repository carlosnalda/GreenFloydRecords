using CarlosNalda.GreenFloydRecords.WebApp.Data.Persistence;
using CarlosNalda.GreenFloydRecords.WebApp.Infrastructure.ImageManager;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class VinylRecordController : Controller
    {
        private readonly IVinylRecordRepository _vinylRecordRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IImageFileManager _imageFileManager;

        public VinylRecordController(IVinylRecordRepository vinylRecordRepository,
            IGenreRepository genreRepository,
            IArtistRepository artistRepository,
            IImageFileManager imageFileManager)
        {
            _vinylRecordRepository = vinylRecordRepository ?? throw new ArgumentNullException(nameof(vinylRecordRepository));
            _genreRepository = genreRepository;
            _artistRepository = artistRepository;
            _imageFileManager = imageFileManager;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> Upsert(string? id)
        {
            VinylRecordVM VinylRecordVm = await GetVinylRecordVmAsync();

            if (string.IsNullOrEmpty(id))
            {
                return View(VinylRecordVm);
            }

            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            VinylRecordVm.VinylRecord = await _vinylRecordRepository.GetByIdAsNoTrackingAsync(parsedId);

            if (VinylRecordVm.VinylRecord == null)
            {
                return NotFound();
            }

            return View(VinylRecordVm);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(VinylRecordVM viewModel, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                viewModel.GenreList = (await _genreRepository.ListAllAsync())
                    .Select(g => MutateToSelectListItem(g.Id, g.Name));
                viewModel.ArtistList = (await _artistRepository.ListAllAsync())
                    .Select(a => MutateToSelectListItem(a.Id, a.Name)).ToList();

                return View(viewModel);
            }

            if (file != null)
            {
                viewModel.VinylRecord.ImageUrl = _imageFileManager.UpsertFile(file, viewModel.VinylRecord.ImageUrl);
            }

            if (viewModel.VinylRecord.Id == default)
            {
                await _vinylRecordRepository.AddAsync(viewModel.VinylRecord);
            }
            else
            {
                var vinylRecord = await _vinylRecordRepository.GetByIdAsNoTrackingAsync(viewModel.VinylRecord.Id);
                if (vinylRecord == null)
                {
                    return NotFound();
                }

                await _vinylRecordRepository.UpdateAsync(viewModel.VinylRecord);
            }

            TempData["success"] = "Vinyl record created successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vinylRecordList = await _vinylRecordRepository.ListAllAsync("Genre,Artist");
            return Json(new { data = vinylRecordList });
        }

        //POST
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var vinylRecord = await _vinylRecordRepository.GetByIdAsync(id.GetValueOrDefault());

            if (vinylRecord == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _imageFileManager.DeleteFile(vinylRecord.ImageUrl);
            await _vinylRecordRepository.DeleteAsync(vinylRecord);

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

        #region Controller private methods
        private async Task<VinylRecordVM> GetVinylRecordVmAsync()
        {
            return new VinylRecordVM
            {
                VinylRecord = new(),
                GenreList = (await _genreRepository.ListAllAsync())
                  .Select(g => MutateToSelectListItem(g.Id, g.Name)),
                ArtistList = (await _artistRepository.ListAllAsync())
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
