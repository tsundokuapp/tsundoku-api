using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IObrasService
    {
        Task<List<RetornoObras>> ObterListaNovels(RequestObras requestObras);
        Task<List<RetornoObras>> ObterListaNovelsRecentes();
        Task<RetornoNovel> ObterNovelPorId(Guid id);
        Task<RetornoNovel> ObterNovelPorSlug(string slug);
        Task<List<Novel>> ObterListaNovelsRecomendadas();

        Task<List<RetornoObras>> ObterListaComics(RequestObras requestObras);
        Task<List<RetornoObras>> ObterListaComicsRecentes();
        Task<RetornoComic> ObterComicPorId(Guid id);
        Task<RetornoComic> ObterComicPorSlug(string slug);        
        Task<List<Comic>> ObterListaComicsRecomendadas();

        Task<List<RetornoCapitulosHome>> ObterCapitulosHome();        
        List<RetornoVolumes> ObterListaVolumeCapitulos(string idObra);
        Task<CapituloComic> ObterCapituloComicPorId(Guid id);
        Task<CapituloNovel> ObterCapituloNovelPorId(Guid id);
    }
}
