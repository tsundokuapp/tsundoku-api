using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface ICapituloNovelAppService
    {
        Task<Result<List<RetornoCapituloNovel>>> ObterCapitulosNovelPorIdObraEIdCapitulo(Guid idObra, Guid idCapitulo);

        Result ValidaExisteCapituloNaLista(List<RetornoCapituloNovel> capitulos, Guid idCapitulo);
    }
}
