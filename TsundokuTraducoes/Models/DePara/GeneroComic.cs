using System;
using TsundokuTraducoes.Api.Models.Generos;
using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.Models.DePara
{
    public class GeneroComic
    {
        public Comic Comic { get; set; }
        public Guid ComicId { get; set; }
        public Genero Genero { get; set; }
        public Guid GeneroId { get; set; }
    }
}
