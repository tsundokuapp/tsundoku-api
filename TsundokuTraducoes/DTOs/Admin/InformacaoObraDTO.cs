using System.Collections.Generic;
using TsundokuTraducoes.Api.Models.Genero;
using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class InformacaoObraDTO
    {
        public List<Genero> ListaGeneros { get; set; }
        public Novel Obra { get; set; }

        public InformacaoObraDTO()
        {
            ListaGeneros = new List<Genero>();
        }
    }
}
