namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response
{
    public class ObjetoRetornoVolumeNovelResponse
    {
        public string Proxima { get; set; }
        public string Anterior { get; set; }
        public int Total { get; set; }
        public List<RetornoVolumeNovel> Data { get; set; }
    }
}
