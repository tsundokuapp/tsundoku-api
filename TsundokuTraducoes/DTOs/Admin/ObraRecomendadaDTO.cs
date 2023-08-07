using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class ObraRecomendadaDTO
    {
        public int Id { get; set; }
        public int IdObra { get; set; }        
        public string ImagemCapaPrincipal { get; set; }
        public string TituloAliasObra { get; set; }
        public string Sinopse { get; set; }
        public string SlugObra { get; set; }
        public List<ComentarioObraRecomendadaDTO> ListaComentarioObraRecomendadaDTO { get; set; }
        public string ListaComentarioObraRecomendadaDTOJson { get; set; }
    }
}
