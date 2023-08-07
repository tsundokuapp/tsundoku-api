namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class ComentarioObraRecomendadaDTO
    {
        public int Id { get; set; }
        public string AutorComentario { get; set; }
        public string Comentario { get; set; }
        public int ObraRecomendadaId { get; set; }
    }
}