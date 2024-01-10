using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IObraRepository
    {
        Task<List<Novel>> RetornaListaNovels();
        Task<List<Comic>> RetornaListaComics();

        Task<Novel> RetornaNovelPorId(Guid obraId);
        Task<Comic> RetornaComicPorId(Guid obraId);

        void AdicionaNovel(Novel novel);
        void AdicionaComic(Comic comic);

        Novel AtualizaNovel(ObraDTO obraDTO);
        Comic AtualizaComic(ObraDTO obraDTO);

        void ExcluiNovel(Novel novel);
        void ExcluiComic(Comic comic);

        Task InsereGenerosNovel(Novel obra, List<string> ListaGeneros, bool inclusao);
        Task InsereGenerosComic(Comic comic, List<string> ListaGeneros, bool inclusao);

        Task<Novel> RetornaNovelExistente(string titulo);
        Task<Comic> RetornaComicExistente(string titulo);

        bool AlteracoesSalvas();
    }
}
