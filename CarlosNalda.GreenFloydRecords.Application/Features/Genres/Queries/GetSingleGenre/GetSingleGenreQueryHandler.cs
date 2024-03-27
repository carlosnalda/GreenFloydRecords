using AutoMapper;
using CarlosNalda.GreenFloydRecords.Application.Contracts.Persistence;
using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using CarlosNalda.GreenFloydRecords.Application.Features.Artists.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.ViewModel;
using CarlosNalda.GreenFloydRecords.Domain.Entities;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Queries.GetSingleGenre;

public class GetSingleGenreQueryHandler : IRequestHandler<GetSingleGenreQuery, GenreVm>
{
    private readonly IAsyncRepository<Genre> _genreRepository;
    private readonly IMapper _mapper;

    public GetSingleGenreQueryHandler(IMapper mapper, IAsyncRepository<Genre> genreRepository)
    {
        _mapper = mapper;
        _genreRepository = genreRepository;
    }

    public async Task<GenreVm> Handle(GetSingleGenreQuery request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetByIdAsync(request.Id);
        var genreVm = _mapper.Map<GenreVm>(genre);

        if (genre == null)
        {
            throw new NotFoundException(nameof(Genre), request.Id);
        }

        return genreVm;
    }
}
