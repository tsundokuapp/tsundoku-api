namespace TsundokuTraducoes.Api.DTOs.Admin
{
    public class ResultadoMensagemDTO
    {
        public bool Erro { get; set; }
        public string MensagemErro { get; set; }
        public object DTO { get; set; }
    }
}
