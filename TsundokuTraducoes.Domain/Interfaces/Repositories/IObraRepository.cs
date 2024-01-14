using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IObraRepository
    {
        List<Novel> RetornaListaNovels();
        List<Comic> RetornaListaComics();

        Novel RetornaNovelPorId(Guid obraId);
        Comic RetornaComicPorId(Guid obraId);

        Task AdicionaNovel(Novel novel);
        Task AdicionaComic(Comic comic);

        Novel AtualizaNovel(ObraDTO obraDTO);
        Comic AtualizaComic(ObraDTO obraDTO);

        void ExcluiNovel(Novel novel);
        void ExcluiComic(Comic comic);

        Novel RetornaNovelExistente(string titulo);
        Comic RetornaComicExistente(string titulo);

        Task InsereGenerosNovel(Novel obra, List<string> ListaGeneros, bool inclusao);
        Task InsereGenerosComic(Comic comic, List<string> ListaGeneros, bool inclusao);

        Task<bool> AlteracoesSalvas();
    }
}
