using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IObrasAppService
    {
        Task<List<RetornoObras>> ObterListaNovels(RequestObras requestObras);
        Task<List<RetornoObras>> ObterListaNovelsRecentes();
        Task<RetornoNovel> ObterNovelPorId(Guid id);
        Task<RetornoNovel> ObterNovelPorSlug(string slug);
        Task<List<RetornoObras>> ObterListaComics(RequestObras requestObras);
        Task<List<RetornoObras>> ObterListaComicsRecentes();
        Task<RetornoComic> ObterComicPorId(Guid id);
        Task<RetornoComic> ObterComicPorSlug(string slug);

        Task<List<RetornoCapitulosHome>> ObterCapitulosHome();
        Task<List<RetornoObrasRecomendadas>> ObterObrasRecomendadas();
        
        List<RetornoVolumes> ObterListaVolumeCapitulos(RequestObras requestObras);
        Task<RetornoCapituloComic> ObterCapituloComicPorId(Guid id);
        Task<RetornoCapituloNovel> ObterCapituloNovelPorId(Guid id);
    }
}