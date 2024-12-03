using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.CreateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.DeleteVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.PartialUpdateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Commands.UpdateVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetSingleVinylRecord;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetSingleVinylRecordForArtist;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.GetVinylRecordList;
using CarlosNalda.GreenFloydRecords.Application.Features.VinylRecords.Queries.ViewModel;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarlosNalda.GreenFloydRecords.Api.Controllers
{
    [ApiController]
    [Route("api/vinylRecord")]
    public class VinylRecordController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public VinylRecordController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ArtistVm>>> GetVinylRecords(
          Guid artistId)
        {
            var dto = await _mediator.Send(new GetVinylRecordListForArtistQuery
            {
                ArtistId = artistId
            });

            return Ok(dto);
        }

        [HttpGet()]
        [Route("~/api/artist/{artistId}/vinylRecord")]
        public async Task<ActionResult<IEnumerable<ArtistVm>>> GetVinylRecordsForArtist(
          Guid artistId)
        {
            var dto = await _mediator.Send(new GetVinylRecordListForArtistQuery
            {
                ArtistId = artistId
            });

            return Ok(dto);
        }

        [HttpGet("{vinylRecordId}")]
        public async Task<ActionResult<VinylRecordVm>> GetVinylRecordForArtist(
            Guid artistId, Guid vinylRecordId)
        {
            var dto = await _mediator.Send(new GetSingleVinylRecordForArtistQuery
            {
                ArtistId = artistId,
                VinylRecordId = vinylRecordId
            });

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<VinylRecordVm>> CreateVinylRecord([FromForm] CreateVinylRecordCommand vinylRecordViewModel,
            IFormFile file)
        {
            var createVinylRecordCommand = _mapper.Map<CreateVinylRecordCommand>(vinylRecordViewModel);
            createVinylRecordCommand.ImageStream = file.OpenReadStream();
            var createdId = await _mediator.Send(createVinylRecordCommand);
            var vinylRecordToReturn = _mapper.Map<VinylRecordVm>(createVinylRecordCommand);
            vinylRecordToReturn.Id = createdId;
            return Ok(vinylRecordToReturn);
        }


        [HttpPut("{vinylRecordId}")]
        public async Task<ActionResult<VinylRecordVm>> UpdateRecordForArtist(
            Guid vinylRecordId,
            [FromForm] UpdateVinylRecordCommand updateVinylRecordCommand,
            IFormFile file)
        {
            updateVinylRecordCommand.Id = vinylRecordId;
            updateVinylRecordCommand.ImageStream = file.OpenReadStream();
            await _mediator.Send(updateVinylRecordCommand);
            return NoContent();
        }

        [HttpPatch("{vinylRecordId}")]
        public async Task<ActionResult> PartiallyUpdateRecordForArtist(
             Guid vinylRecordId,
            [FromForm] string jsonPatch,
            IFormFile file)
        {
            var patchDocument = JsonConvert.DeserializeObject<JsonPatchDocument<VinylRecordVm>>(jsonPatch);
            VinylRecordVm dto = await _mediator.Send(new GetSingleVinylRecordQuery
            {
                Id = vinylRecordId
            });
            patchDocument.ApplyTo(dto);
            var partialUpdateVinylRecordCommand = _mapper.Map<PartialUpdateVinylRecordCommand>(dto);
            partialUpdateVinylRecordCommand.ImageStream = file.OpenReadStream();
            await _mediator.Send(partialUpdateVinylRecordCommand);
            return NoContent();
        }

        [HttpDelete("{vinylRecordId}")]
        public async Task<ActionResult<VinylRecordVm>> DeleteCourseForAuthor(Guid vinylRecordId)
        {
            await _mediator.Send(new DeleteVinylRecordCommand { Id = vinylRecordId });
            return NoContent();
        }
    }

    //[HttpPut("{courseId}")]
    //public async Task<IActionResult> UpdateCourseForAuthor(Guid authorId,
    //  Guid courseId,
    //  CourseDto course)
    //{
    //    if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
    //    {
    //        return NotFound();
    //    }

    //    var courseForAuthorFromRepo = await _courseLibraryRepository
    //        .GetCourseAsync(authorId, courseId);

    //    if (courseForAuthorFromRepo == null)
    //    {
    //        return NotFound();
    //    }

    //    _mapper.Map(course, courseForAuthorFromRepo);

    //    _courseLibraryRepository.UpdateCourse(courseForAuthorFromRepo);

    //    await _courseLibraryRepository.SaveAsync();
    //    return NoContent();
    //}

    //[HttpDelete("{courseId}")]
    //public async Task<ActionResult> DeleteCourseForAuthor(
    //    Guid authorId, Guid courseId)
    //{
    //    if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
    //    {
    //        return NotFound();
    //    }

    //    var courseForAuthorFromRepo = await _courseLibraryRepository
    //        .GetCourseAsync(authorId, courseId);

    //    if (courseForAuthorFromRepo == null)
    //    {
    //        return NotFound();
    //    }

    //    _courseLibraryRepository.DeleteCourse(courseForAuthorFromRepo);
    //    await _courseLibraryRepository.SaveAsync();

    //    return NoContent();
    //}

    //[HttpPatch("{courseId}")]
    //public async Task<IActionResult> PartiallyUpdateCourseForAuthor(
    //   Guid authorId,
    //   Guid courseId,
    //   JsonPatchDocument<CourseForUpdateDto> patchDocument)
    //{
    //    if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
    //    {
    //        return NotFound();
    //    }

    //    var courseForAuthorFromRepo = await _courseLibraryRepository
    //        .GetCourseAsync(authorId, courseId);

    //    if (courseForAuthorFromRepo == null)
    //    {
    //        var courseDto = new CourseForUpdateDto();
    //        patchDocument.ApplyTo(courseDto, ModelState);

    //        if (!TryValidateModel(courseDto))
    //        {
    //            return ValidationProblem(ModelState);
    //        }

    //        var courseToAdd = _mapper.Map<Entities.Course>(courseDto);
    //        courseToAdd.Id = courseId;

    //        _courseLibraryRepository.AddCourse(authorId, courseToAdd);
    //        await _courseLibraryRepository.SaveAsync();

    //        var courseToReturn = _mapper.Map<CourseDto>(courseToAdd);
    //        return CreatedAtRoute("GetCourseForAuthor",
    //            new { authorId, courseId = courseToReturn.Id },
    //            courseToReturn);
    //    }

    //    var courseToPatch = _mapper.Map<CourseForUpdateDto>(
    //        courseForAuthorFromRepo);
    //    patchDocument.ApplyTo(courseToPatch, ModelState);

    //    if (!TryValidateModel(courseToPatch))
    //    {
    //        return ValidationProblem(ModelState);
    //    }

    //    _mapper.Map(courseToPatch, courseForAuthorFromRepo);

    //    _courseLibraryRepository.UpdateCourse(courseForAuthorFromRepo);

    //    await _courseLibraryRepository.SaveAsync();

    //    return NoContent();
    //}

}
