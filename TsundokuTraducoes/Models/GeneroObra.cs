namespace TsundokuTraducoes.Api.Models
{
    public class GeneroObra
    {       
        public Obra Obra { get; set; }
        public int? ObraId { get; set; }
        public Genero Genero { get; set; }
        public int? GeneroId { get; set; }
    }
}
