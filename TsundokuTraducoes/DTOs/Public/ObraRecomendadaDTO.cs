using System.Collections.Generic;

namespace TsundokuTraducoes.Api.DTOs.Public
{
    public class ObraRecomendadaDTO
    {
        public string UrlImagemCapaPrincipal { get; set; }
        public string TituloAliasObra { get; set; }
        public string Sinopse { get; set; }
        public string SlugObra { get; set; }
        public List<ComentarioObraRecomendadaDTO> ComentariosObraRecomendadaDTO { get; set; }

        public ObraRecomendadaDTO()
        {
            ComentariosObraRecomendadaDTO = new List<ComentarioObraRecomendadaDTO>();
        }
    }
}
