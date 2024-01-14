using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IObraService
    {
        List<Novel> RetornaListaNovels();
        List<Comic> RetornaListaComics();

        Novel RetornaNovelPorId(Guid id);
        Comic RetornaComicPorId(Guid id);

        Task<bool> AdicionaNovel(Novel novel);
        Task<bool> AdicionaComic(Comic comic);

        Novel AtualizaNovel(ObraDTO obraDTO);
        Comic AtualizaComic(ObraDTO comic);

        Task<bool> ExcluiNovel(Novel novel);
        Task<bool> ExcluiComic(Comic comic);

        Novel RetornaNovelExistente(string titulo);
        Comic RetornaComicExistente(string titulo);

        Task InsereGenerosNovel(Novel novel, List<string> ListaGeneros, bool inclusao);
        Task InsereGenerosComic(Comic comic, List<string> ListaGeneros, bool inclusao);

        Task<bool> AlteracoesSalvas();
    }
}
