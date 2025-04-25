namespace TsundokuTraducoes.Helpers.DTOs.Admin.Retorno.Response
{
    public class ObjetoRetornoGenerosResponse
    {
        public string Proxima { get; set; }
        public string Anterior { get; set; }
        public List<RetornoGenero> Data { get; set; }
        public int Total { get; set; }
    }
}
