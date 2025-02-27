using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IObrasRepository
    {
        Task<List<Novel>> ObterListaNovels(RequestObras requestObras);
        Task<List<RetornoObras>> ObterListaComics(RequestObras requestObras);

        Task<List<Novel>> ObterListaNovelsRecomendadas();
        Task<List<Comic>> ObterListaComicsRecomendadas();

        Task<List<RetornoObras>> ObterListaNovelsRecentes();
        Task<List<RetornoObras>> ObterListaComicsRecentes();
        
        Task<Novel> ObterNovelPorId(Guid id);
        Task<Novel> ObterNovelPorSlug(string slug);

        Task<RetornoComic> ObterComicPorId(Guid id);
        Task<RetornoComic> ObterComicPorSlug(string slug);

        Task<List<RetornoCapitulosHome>> ObterCapitulosHome();

        List<RetornoVolumes> ObterListaVolumeCapitulos(string idObra);
        Task<CapituloComic> ObterCapituloComicPorId(Guid id);
        Task<CapituloNovel> ObterCapituloNovelPorId(Guid id);
    }
}