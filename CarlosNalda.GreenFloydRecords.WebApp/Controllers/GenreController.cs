using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.CreateGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.DeleteGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.UpdateGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetGenreList;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetSingleGenre;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class GenreController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GenreController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _mediator.Send(new GetGenreListQuery());
            return View(_mapper.Map<List<GenreViewModel>>(genres));
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreViewModel genre)
        {
            if (char.IsDigit(genre.Name.ToCharArray()[0]))
                ModelState.AddModelError("name", "Name can not start with digit.");

            if (!ModelState.IsValid)
                return View(genre);

            var request = _mapper.Map<CreateGenreCommand>(genre);
            _ = await _mediator.Send(request);

            TempData["success"] = "Genre created successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = await _mediator.Send(new GetSingleGenreQuery { Id = parsedId });

            return View(_mapper.Map<GenreViewModel>(genre));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GenreViewModel genre)
        {
            if (char.IsDigit(genre.Name.ToCharArray()[0]))
                ModelState.AddModelError("name", "Name can not start with digit.");

            if (!ModelState.IsValid)
                return View(genre);

            var request = _mapper.Map<UpdateGenreCommand>(genre);
            await _mediator.Send(request);

            TempData["success"] = "Genre updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var genre = await _mediator.Send(new GetSingleGenreQuery { Id = parsedId });

            return View(_mapper.Map<GenreViewModel>(genre));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWithPost(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            await _mediator.Send(new DeleteGenreCommand { Id = parsedId });

            TempData["success"] = "Genre deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
