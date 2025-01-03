﻿using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.GetSingleArtistWithChildEntities;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers;

//[ApiController]
//[Route("api/artists")]
//public class ArtistController : ControllerBase
//{
//    private readonly IMediator _mediator;

//    public ArtistController(IMediator mediator)
//    {
//        _mediator = mediator ??
//            throw new ArgumentNullException(nameof(mediator));
//    }

//    [HttpGet("{artistId}/vinylRecord")]
//    public async Task<ActionResult<IEnumerable<ArtistVm>>> GetVinylRecordsForArtist(
//        Guid artistId)
//    {
//        var dto = await _mediator.Send(new GetSingleArtistWithChildEntitiesQuery()
//        {
//            Id = artistId,
//            IncludeProperties = "VinylRecords"
//        });
//        return Ok(dto);
//    }

//    //[HttpGet("{courseId}")]
//    //public async Task<ActionResult<CourseDto>> GetCourseForAuthor(
//    //    Guid authorId, Guid courseId)
//    //{
//    //    if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
//    //    {
//    //        return NotFound();
//    //    }

//    //    var courseForAuthorFromRepo = await _courseLibraryRepository
//    //        .GetCourseAsync(authorId, courseId);

//    //    if (courseForAuthorFromRepo == null)
//    //    {
//    //        return NotFound();
//    //    }
//    //    return Ok(_mapper.Map<CourseDto>(courseForAuthorFromRepo));
//    //}


//    //[HttpPost]
//    //public async Task<ActionResult<CourseDto>> CreateCourseForAuthor(
//    //        Guid authorId, CourseForCreationDto course)
//    //{
//    //    if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
//    //    {
//    //        return NotFound();
//    //    }

//    //    var courseEntity = _mapper.Map<Entities.Course>(course);
//    //    _courseLibraryRepository.AddCourse(authorId, courseEntity);
//    //    await _courseLibraryRepository.SaveAsync();

//    //    var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
//    //    return Ok(courseToReturn);
//    //}


//    //[HttpPut("{courseId}")]
//    //public async Task<IActionResult> UpdateCourseForAuthor(Guid authorId,
//    //  Guid courseId,
//    //  CourseDto course)
//    //{
//    //    if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
//    //    {
//    //        return NotFound();
//    //    }

//    //    var courseForAuthorFromRepo = await _courseLibraryRepository
//    //        .GetCourseAsync(authorId, courseId);

//    //    if (courseForAuthorFromRepo == null)
//    //    {
//    //        return NotFound();
//    //    }

//    //    _mapper.Map(course, courseForAuthorFromRepo);

//    //    _courseLibraryRepository.UpdateCourse(courseForAuthorFromRepo);

//    //    await _courseLibraryRepository.SaveAsync();
//    //    return NoContent();
//    //}

//    //[HttpDelete("{courseId}")]
//    //public async Task<ActionResult> DeleteCourseForAuthor(
//    //    Guid authorId, Guid courseId)
//    //{
//    //    if (!await _courseLibraryRepository.AuthorExistsAsync(authorId))
//    //    {
//    //        return NotFound();
//    //    }

//    //    var courseForAuthorFromRepo = await _courseLibraryRepository
//    //        .GetCourseAsync(authorId, courseId);

//    //    if (courseForAuthorFromRepo == null)
//    //    {
//    //        return NotFound();
//    //    }

//    //    _courseLibraryRepository.DeleteCourse(courseForAuthorFromRepo);
//    //    await _courseLibraryRepository.SaveAsync();

//    //    return NoContent();
//    //}

//}