namespace TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response;

public class ObjetoRetornoCapitulosPorSlugComics
{
    public string Proxima { get; set; }
    public string Anterior { get; set; }
    public List<RetornoCapituloComic> Data { get; set; }
    public int Total { get; set; }
}
