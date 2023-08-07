using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models
{
    public class ObraRecomendada
    {
        [Key]
        public int Id { get; set; }
        public int IdObra { get; set; }        
        public string UrlImagemCapaPrincipal { get; set; }
        public string TituloAliasObra { get; set; }
        public string Sinopse { get; set; }
        public string SlugObra { get; set; }
        public virtual List<ComentarioObraRecomendada> ListaComentarioObraRecomendada { get; set; }
        
        public ObraRecomendada()
        {
            ListaComentarioObraRecomendada = new List<ComentarioObraRecomendada>();
        }
    }
}
