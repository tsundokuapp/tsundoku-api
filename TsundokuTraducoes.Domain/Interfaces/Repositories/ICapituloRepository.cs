using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface ICapituloRepository
    {
        List<CapituloNovel> RetornaListaCapitulosNovel(Guid? volumeId);
        List<CapituloComic> RetornaListaCapitulosComic(Guid? volumeId);

        CapituloNovel RetornaCapituloNovelPorId(Guid capituloId);
        CapituloComic RetornaCapituloComicPorId(Guid capituloId);

        void AdicionaCapituloNovel(CapituloNovel capituloNovel);
        void AdicionaCapituloComic(CapituloComic capituloComic);

        CapituloNovel AtualizaCapituloNovel(CapituloDTO capituloDTO);
        CapituloComic AtualizaCapituloComic(CapituloDTO capituloDTO);

        void ExcluiCapituloNovel(CapituloNovel capituloNovel);
        void ExcluiCapituloComic(CapituloComic capituloComic);

        CapituloNovel RetornaCapituloNovelExistente(CapituloDTO capituloDTO);
        CapituloComic RetornaCapituloComicExistente(CapituloDTO capituloDTO);

        Task<bool> AlteracoesSalvas();
    }
}
