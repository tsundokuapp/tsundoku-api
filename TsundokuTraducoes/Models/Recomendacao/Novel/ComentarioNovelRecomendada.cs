using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models.Recomendacao.Novel
{
    public class ComentarioNovelRecomendada
    {
        [Key]
        public int Id { get; set; }
        public string AutorComentario { get; set; }
        public string Comentario { get; set; }
        public int NovelRecomendadaId { get; set; }
        public virtual NovelRecomendada NovelRecomendada { get; set; }
    }
}