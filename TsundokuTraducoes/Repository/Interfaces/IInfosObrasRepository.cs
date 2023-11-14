using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin.Request;
using TsundokuTraducoes.Api.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IInfosObrasRepository
    {
        Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras);
        Task<List<RetornoObra>> ObterListaNovelsRecentes();
        Task<RetornoObra> ObterNovelsPorId(RequestObras requestObras);

        Task<List<RetornoObra>> ObterListaComics(RequestObras requestObras);
        Task<List<RetornoObra>> ObterListaComicsRecentes();
        Task<RetornoObra> ObterComicPorId(RequestObras requestObras);

        Task<List<RetornoCapitulos>> ObterCapitulosHome();
    }
}
