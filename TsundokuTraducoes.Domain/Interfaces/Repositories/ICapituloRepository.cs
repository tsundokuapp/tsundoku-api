using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface ICapituloRepository
    {
        Task<List<CapituloNovel>> RetornaListaCapitulosNovel(Guid? volumeId);
        Task<List<CapituloComic>> RetornaListaCapitulosComic(Guid? volumeId);

        Task<CapituloNovel> RetornaCapituloNovelPorId(Guid capituloId);
        Task<CapituloComic> RetornaCapituloComicPorId(Guid capituloId);

        Task AdicionaCapituloNovel(CapituloNovel volumeNovel);
        Task AdicionaCapituloComic(CapituloComic volumeComic);

        Task<CapituloNovel> AtualizaCapituloNovel(CapituloNovel volumeNovel);
        Task<CapituloComic> AtualizaCapituloComic(CapituloComic volumeComic);

        void ExcluiCapituloNovel(CapituloNovel volumeNovel);
        void ExcluiCapituloComic(CapituloComic volumeComic);

        Task<CapituloNovel> RetornaCapituloNovelExistente(CapituloNovel capituloNovel);
        Task<CapituloComic> RetornaCapituloComicExistente(CapituloComic capituloComic);

        void AtualizaNovelPorCapitulo(Novel novel, CapituloNovel capituloNovel);
        void AtualizaComicPorCapitulo(Comic comic, CapituloComic capituloComic);

        Task<bool> AlteracoesSalvas();
    }
}
