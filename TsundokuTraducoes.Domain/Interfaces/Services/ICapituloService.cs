using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface ICapituloService
    {
        List<CapituloNovel> RetornaListaCapitulosNovel(Guid? volumeId);
        List<CapituloComic> RetornaListaCapitulosComic(Guid? volumeId);

        CapituloNovel RetornaCapituloNovelPorId(Guid capituloId);
        CapituloComic RetornaCapituloComicPorId(Guid capituloId);

        Task<bool> AdicionaCapituloNovel(CapituloNovel capituloNovel);
        Task<bool> AdicionaCapituloComic(CapituloComic capituloComic);

        CapituloNovel AtualizaCapituloNovel(CapituloDTO capituloDTO);
        CapituloComic AtualizaCapituloComic(CapituloDTO capituloDTO);

        Task<bool> ExcluiCapituloNovel(CapituloNovel capituloNovel);
        Task<bool> ExcluiCapituloComic(CapituloComic capituloComic);

        CapituloNovel RetornaCapituloNovelExistente(CapituloDTO capituloDTO);
        CapituloComic RetornaCapituloComicExistente(CapituloDTO capituloDTO);

        Task<bool> AlteracoesSalvas();
    }
}
