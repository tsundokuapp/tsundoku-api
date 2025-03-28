namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response
{
    public class ObjetoRetornoVolumeComicResponse
    {
        public string Proxima { get; set; }
        public string Anterior { get; set; }
        public List<RetornoVolumeComic> Data { get; set; }
        public int Total { get; set; }
    }
}