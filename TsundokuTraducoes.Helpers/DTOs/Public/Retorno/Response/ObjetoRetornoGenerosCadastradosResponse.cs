namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response
{
    public class ObjetoRetornoGenerosCadastradosResponse
    {
        public string Proxima { get; set; }
        public string Anterior { get; set; }
        public List<RetornoGeneroCadastrado> Data { get; set; }
        public int Total { get; set; }
    }
}
