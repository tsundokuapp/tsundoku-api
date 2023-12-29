namespace TsundokuTraducoes.Helpers.DTOs.Admin
{
    public class ComentarioObraRecomendadaDTO
    {
        public Guid Id { get; set; }
        public string AutorComentario { get; set; }
        public string Comentario { get; set; }
        public Guid ObraRecomendadaId { get; set; }
    }
}