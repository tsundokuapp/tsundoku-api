using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IObrasService
    {
        Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras);
        Task<List<RetornoObra>> ObterListaNovelsRecentes();
        Task<RetornoObra> ObterNovelPorId(RequestObras requestObras);

        //Task<List<RetornoObra>> ObterListaComics(RequestObras requestObras);
        //Task<List<RetornoObra>> ObterListaComicsRecentes();
        //Task<RetornoObra> ObterComicPorId(RequestObras requestObras);

        //Task<List<RetornoCapitulos>> ObterCapitulosHome();
    }
}
