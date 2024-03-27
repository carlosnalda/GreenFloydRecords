using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CarlosNalda.GreenFloydRecords.Application.Features.Genres.Commands.CreateGenre
{
    public class CreateGenreCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
