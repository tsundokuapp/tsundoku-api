using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IObrasService
    {
        Task<List<Novel>> ObterListaNovels(RequestObras requestObras);
        Task<List<Novel>> ObterListaNovelsRecentes();        
        Task<Novel> ObterNovelPorId(Guid id);
        Task<Novel> ObterNovelPorSlug(string slug);
        Task<List<Novel>> ObterListaNovelsRecomendadas();

        Task<List<Comic>> ObterListaComics(RequestObras requestObras);
        Task<List<Comic>> ObterListaComicsRecentes();
        Task<Comic> ObterComicPorId(Guid id);
        Task<Comic> ObterComicPorSlug(string slug);        
        Task<List<Comic>> ObterListaComicsRecomendadas();

        Task<List<RetornoCapitulosHome>> ObterCapitulosHome();        
        List<RetornoVolumes> ObterListaVolumeCapitulos(string idObra);
        Task<CapituloComic> ObterCapituloComicPorId(Guid id);
        Task<CapituloNovel> ObterCapituloNovelPorId(Guid id);
    }
}
