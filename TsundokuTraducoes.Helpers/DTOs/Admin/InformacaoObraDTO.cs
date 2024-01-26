using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;

namespace TsundokuTraducoes.Helpers.DTOs.Admin
{
    public class InformacaoObraDTO
    {
        public List<Genero> ListaGeneros { get; set; }
        public Novel Novel { get; set; }

        public InformacaoObraDTO()
        {
            ListaGeneros = new List<Genero>();
        }
    }
}
