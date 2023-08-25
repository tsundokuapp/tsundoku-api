using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models.Recomendacao.Novel
{
    public class NovelRecomendada
    {
        [Key]
        public int Id { get; set; }
        public int IdObra { get; set; }
        public string UrlImagemCapaPrincipal { get; set; }
        public string TituloAliasObra { get; set; }
        public string Sinopse { get; set; }
        public string SlugObra { get; set; }
        public virtual List<ComentarioNovelRecomendada> ListaComentarioObraRecomendada { get; set; }

        public NovelRecomendada()
        {
            ListaComentarioObraRecomendada = new List<ComentarioNovelRecomendada>();
        }
    }
}
