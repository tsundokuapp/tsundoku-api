using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.Models.Genero
{
    public class GeneroObra
    {
        public Novel Obra { get; set; }
        public int? ObraId { get; set; }
        public Genero Genero { get; set; }
        public int? GeneroId { get; set; }
    }
}
