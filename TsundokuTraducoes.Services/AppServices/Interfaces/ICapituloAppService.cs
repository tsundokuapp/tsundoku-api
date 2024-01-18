using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface ICapituloAppService
    {
        Result<List<RetornoCapitulo>> RetornaListaCapitulos(Guid? idObra);
        Result<List<RetornoCapitulo>> RetornaListaCapitulosNovel(Guid? idObra);
        Result<List<RetornoCapitulo>> RetornaListaCapitulosComic(Guid? idObra);

        Result<RetornoCapitulo> RetornaCapituloNovelPorId(Guid id);
        Result<RetornoCapitulo> RetornaCapituloComicPorId(Guid id);

        Task<Result<RetornoCapitulo>> AdicionaCapituloNovel(CapituloDTO capituloDTO);
        Task<Result<RetornoCapitulo>> AdicionaCapituloComic(CapituloDTO capituloDTO);

        Task<Result<RetornoCapitulo>> AtualizaCapituloNovel(CapituloDTO capituloDTO);
        Task<Result<RetornoCapitulo>> AtualizaCapituloComic(CapituloDTO capituloDTO);

        Task<Result<bool>> ExcluiCapituloNovel(Guid novelId);
        Task<Result<bool>> ExcluiCapituloComic(Guid comicId);
    }
}
