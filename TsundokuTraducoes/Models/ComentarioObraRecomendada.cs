using System.ComponentModel.DataAnnotations;

namespace TsundokuTraducoes.Api.Models
{
    public class ComentarioObraRecomendada
    {
        [Key]
        public int Id { get; set; }
        public string AutorComentario { get; set; }
        public string Comentario { get; set; }
        public int ObraRecomendadaId { get; set; }
        public virtual ObraRecomendada ObraRecomendada { get; set; }
    }
}