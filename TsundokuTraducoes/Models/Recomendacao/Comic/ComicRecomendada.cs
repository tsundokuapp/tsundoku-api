using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models.Recomendacao.Comic
{
    public class ComicRecomendada
    {
        [Key]
        public int Id { get; set; }
        public int IdObra { get; set; }
        public string UrlImagemCapaPrincipal { get; set; }
        public string TituloAliasObra { get; set; }
        public string Sinopse { get; set; }
        public string SlugObra { get; set; }
        public virtual List<ComentarioComicRecomendada> ListaComentarioComicRecomendada { get; set; }

        public ComicRecomendada()
        {
            ListaComentarioComicRecomendada = new List<ComentarioComicRecomendada>();
        }
    }
}
