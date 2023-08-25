using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models.Recomendacao.Comic
{
    public class ComentarioComicRecomendada
    {
        [Key]
        public int Id { get; set; }
        public string AutorComentario { get; set; }
        public string Comentario { get; set; }
        public int ComicRecomendadaId { get; set; }
        public virtual ComicRecomendada ComicRecomendada { get; set; }
    }
}