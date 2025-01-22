using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IObrasRepository
    {
        Task<List<RetornoObras>> ObterListaNovels(RequestObras requestObras);
        Task<List<RetornoObras>> ObterListaComics(RequestObras requestObras);
        
        Task<List<RetornoObras>> ObterListaNovelsRecentes();
        Task<List<RetornoObras>> ObterListaComicsRecentes();
        
        Task<RetornoNovel> ObterNovelPorId(Guid id);
        Task<RetornoNovel> ObterNovelPorSlug(string slug);

        Task<RetornoObras> ObterComicPorId(RequestObras requestObras);

        Task<List<RetornoCapitulosHome>> ObterCapitulosHome();
        Task<List<RetornoObrasRecomendadas>> ObterObrasRecomendadas();

        List<RetornoVolumes> ObterListaVolumeCapitulos(string idObra);
        Task<CapituloComic> ObterCapituloComicPorId(Guid id);
        Task<CapituloNovel> ObterCapituloNovelPorId(Guid id);
    }
}