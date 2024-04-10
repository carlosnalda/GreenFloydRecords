using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.CreateGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetGenreList;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetSingleGenre;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarlosNalda.GreenFloydRecords.Api.Controllers
{
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GenreController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("api/genre")]
        public async Task<ActionResult<IEnumerable<GenreVm>>> GetGenres()
        {
            var dtos = await _mediator.Send(new GetGenreListQuery());
            return Ok(dtos);
        }

        [HttpGet("api/getgenre/{genreId}")]
        public async Task<ActionResult<GenreVm>> GetGenre(Guid genreId)
        {
            var dto = await _mediator.Send(new GetSingleGenreQuery { Id = genreId });
            return Ok(dto);
        }


        [HttpPost("api/genres")]
        public async Task<ActionResult> CreateGenre([FromBody] CreateGenreCommand createGenreCommand)
        {
            var createdId = await _mediator.Send(createGenreCommand);
            var genreToReturn = _mapper.Map<GenreVm>(createGenreCommand);
            genreToReturn.Id = createdId;
            return Ok();
        }
    }
}
