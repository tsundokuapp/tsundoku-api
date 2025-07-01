using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface ICapituloNovelAppService
    {
        Task<Result<List<RetornoCapituloNovel>>> ObterCapitulosNovelPorIdObraEIdCapitulo(Guid idObra, Guid idCapitulo);
        Task<Result<List<RetornoCapituloNovel>>> ObterCapitulosNovelPorSlugObraEIdCapitulo(string slugObra, Guid idCapitulo);
        Task<Result<List<RetornoCapituloNovel>>> ObterCapitulosNovelPorSlugObraESlugCapitulo(string slugObra, string slugCapitulo);
        Result ValidaExisteCapituloNaLista(List<RetornoCapituloNovel> capitulos, Guid idCapitulo);
    }
}
