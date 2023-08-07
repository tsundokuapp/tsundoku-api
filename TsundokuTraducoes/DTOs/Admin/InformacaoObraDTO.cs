using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class InformacaoObraDTO
    {
        public List<Genero> ListaGeneros { get; set; }
        public Obra Obra { get; set; }

        public InformacaoObraDTO()
        {
            ListaGeneros = new List<Genero>();
        }
    }
}
