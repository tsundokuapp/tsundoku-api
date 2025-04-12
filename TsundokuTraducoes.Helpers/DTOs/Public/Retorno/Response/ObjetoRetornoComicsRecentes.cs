namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response
{
    public class ObjetoRetornoComicsRecentes
    {
        public string Proxima { get; set; }
        public string Anterior { get; set; }
        public List<RetornoComicsRecentes> Data { get; set; }
        public int Total { get; set; }
    }
}
