namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response
{
    public class ObjetoRetornoNovelsRecentes
    {
        public string Proxima { get; set; }
        public string Anterior { get; set; }
        public List<RetornoNovelsRecentes> Data { get; set; }
        public int Total { get; set; }
    }
}
