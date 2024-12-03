using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Infrastructure;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetArtistList;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetGenreList;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.CreateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.DeleteVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.UpdateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetSingleVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetVinylRecordList;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class VinylRecordController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IImageFileManager _imageFileManager;

        public VinylRecordController(IMapper mapper,
            IMediator mediator,
            IImageFileManager imageFileManager)
        {
            _mapper = mapper;
            _mediator = mediator;
            _imageFileManager = imageFileManager;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> Upsert(string? id)
        {
            VinylRecordUserInterfaceViewModel VinylRecordVm = await GetVinylRecordVmAsync();

            if (string.IsNullOrEmpty(id))
                return View(VinylRecordVm);

            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var vinylRecord = await _mediator.Send(new GetSingleVinylRecordQuery() { Id = parsedId });
            VinylRecordVm.VinylRecord = _mapper.Map<VinylRecordViewModel>(vinylRecord);

            if (VinylRecordVm.VinylRecord == null)
                return NotFound();

            return View(VinylRecordVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(VinylRecordUserInterfaceViewModel viewModel, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                viewModel.GenreList = (await _mediator.Send(new GetGenreListQuery()))
                    .Select(g => MutateToSelectListItem(g.Id, g.Name));
                viewModel.ArtistList = (await _mediator.Send(new GetArtistListQuery()))
                    .Select(a => MutateToSelectListItem(a.Id, a.Name)).ToList();

                return View(viewModel);
            }

            if (viewModel.VinylRecord.Id == default)
            {
                var createVinylRecordCommand = _mapper.Map<CreateVinylRecordCommand>(viewModel.VinylRecord);
                if (file != null)
                {
                    createVinylRecordCommand.ImageStream = file.OpenReadStream();
                }
                _ = await _mediator.Send(createVinylRecordCommand);
            }
            else
            {

                _ = await _mediator.Send(new GetSingleVinylRecordQuery() { Id = viewModel.VinylRecord.Id });

                var updateVinylRecordCommand = _mapper.Map<UpdateVinylRecordCommand>(viewModel.VinylRecord);
                await _mediator.Send(updateVinylRecordCommand);
            }

            TempData["success"] = "Vinyl record created successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vinylRecordList = await _mediator.Send(new GetVinylRecordListQuery() { IncludeProperties = "Genre,Artist" });
            return Json(new { data = vinylRecordList });
        }

        //POST
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return Json(new { success = false, message = "Error while deleting" });

            // To check if exist, could be better if implementing a GetFirstOrDefaultVinylRecordQuery or CheckIfExistsVinylRecordQuery
            var vinylRecord = await _mediator.Send(new GetSingleVinylRecordQuery() { Id = id.GetValueOrDefault() });

            if (vinylRecord == null)
                return Json(new { success = false, message = "Error while deleting" });

            // Que pasa si aqui le meto una excepcion
            _imageFileManager.DeleteFile(vinylRecord.ImageUrl);
            await _mediator.Send(new DeleteVinylRecordCommand { Id = id.Value });

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

        #region Controller private methods
        private async Task<VinylRecordUserInterfaceViewModel> GetVinylRecordVmAsync()
        {
            return new VinylRecordUserInterfaceViewModel
            {
                VinylRecord = new(),
                GenreList = (await _mediator.Send(new GetGenreListQuery()))
                  .Select(g => MutateToSelectListItem(g.Id, g.Name)),
                ArtistList = (await _mediator.Send(new GetArtistListQuery()))
                  .Select(a => MutateToSelectListItem(a.Id, a.Name))
            };
        }

        private SelectListItem MutateToSelectListItem(Guid id, string text) =>
            new SelectListItem
            {
                Value = id.ToString(),
                Text = text,
            };
        #endregion
    }
}
