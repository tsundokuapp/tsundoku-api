using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IObraAppService
    {
        Task<Result<List<RetornoObra>>> RetornaListaObras();
        Task<Result<List<RetornoObra>>> RetornaListaNovels();
        Task<Result<List<RetornoObra>>> RetornaListaComics();

        Task<Result<RetornoObra>> RetornaNovelPorId(Guid id);
        Task<Result<RetornoObra>> RetornaComicPorId(Guid id);

        Task<Result<RetornoObra>> AdicionaNovel(ObraDTO obraDTO);
        Task<Result<RetornoObra>> AdicionaComic(ObraDTO obraDTO);

        Task<Result<RetornoObra>> AtualizaNovel(ObraDTO obraDTO);
        Task<Result<RetornoObra>> AtualizaComic(ObraDTO obraDTO);

        Task<Result<bool>> ExcluiNovel(Guid idObra);
        Task<Result<bool>> ExcluiComic(Guid idObra);
    }
}
