using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IObrasAppService
    {
        Task<List<RetornoNovel>> ObterListaNovels(RequestObras requestObras);
        Task<List<RetornoNovelsRecentes>> ObterListaNovelsRecentes();
        Task<Result<RetornoNovel>> ObterNovelPorId(Guid id);
        Task<Result<RetornoNovel>> ObterNovelPorSlug(string slug);

        Task<List<RetornoComic>> ObterListaComics(RequestObras requestObras);
        Task<List<RetornoComicsRecentes>> ObterListaComicsRecentes();
        Task<Result<RetornoComic>> ObterComicPorId(Guid id);
        Task<Result<RetornoComic>> ObterComicPorSlug(string slug);

        Task<List<RetornoCapitulosHome>> ObterCapitulosHome();
        Task<List<RetornoObrasRecomendadas>> ObterObrasRecomendadas();
        
        List<RetornoVolumes> ObterListaVolumeCapitulos(RequestObras requestObras);
        Task<RetornoCapituloComic> ObterCapituloComicPorId(Guid id);
        Task<RetornoCapituloNovel> ObterCapituloNovelPorId(Guid id);

        Task<List<RetornoObrasPesquisa>> ObterObrasPesquisa(string obra);
    }
}