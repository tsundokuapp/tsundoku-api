using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface ICapituloComicAppService
    {
        Task<Result<List<RetornoCapituloComic>>> ObterCapitulosComicPorIdObraEIdCapitulo(Guid idObra, Guid idCapitulo);
        Result ValidaExisteCapituloNaLista(List<RetornoCapituloComic> capitulos, Guid idCapitulo);
    }
}
