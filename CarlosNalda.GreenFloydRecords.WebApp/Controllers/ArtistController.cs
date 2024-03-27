using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.CreateArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.DeleteArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Commands.UpdateArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetArtistList;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetSingleArtist;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using CarlosNalda.GreenFloydRecords.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarlosNalda.GreenFloydRecords.WebApp.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ArtistController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var artists = await _mediator.Send(new GetArtistListQuery());
            return View(_mapper.Map<List<ArtistViewModel>>(artists));
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistViewModel artist)
        {
            if (!ModelState.IsValid)
                return View(artist);

            var request = _mapper.Map<CreateArtistCommand>(artist);
            _ = await _mediator.Send(request);

            TempData["success"] = "Artist created successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = await _mediator.Send(new GetSingleArtistQuery { Id = parsedId });

            return View(_mapper.Map<ArtistViewModel>(artist));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArtistViewModel artist)
        {
            if (!ModelState.IsValid)
                return View(artist);

            _ = await _mediator.Send(new GetSingleArtistQuery { Id = artist.Id });

            var request = _mapper.Map<UpdateArtistCommand>(artist);
            await _mediator.Send(request);

            TempData["success"] = "Artist updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            var artist = await _mediator.Send(new GetSingleArtistQuery { Id = parsedId });

            return View(_mapper.Map<ArtistViewModel>(artist));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWithPost(string? id)
        {
            if (!Guid.TryParse(id, out Guid parsedId))
                return NotFound();

            _ = await _mediator.Send(new GetSingleArtistQuery { Id = parsedId });

            await _mediator.Send(new DeleteArtistCommand { Id = parsedId });


            TempData["success"] = "Artist deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
