using System;
using TsundokuTraducoes.Api.Models.Generos;
using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.Models.DePara
{
    public class GeneroNovel
    {
        public Novel Novel { get; set; }
        public Guid NovelId { get; set; }
        public Genero Genero { get; set; }
        public Guid GeneroId { get; set; }
    }
}
