using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IObraService
    {
        Task<List<Novel>> RetornaListaNovels();
        Task<List<Comic>> RetornaListaComics();

        Task<Novel> RetornaNovelPorId(Guid id);
        Task<Comic> RetornaComicPorId(Guid id);

        bool AdicionaNovel(Novel novel);
        bool AdicionaComic(Comic comic);

        Novel AtualizaNovel(ObraDTO obraDTO);
        Comic AtualizaComic(ObraDTO comic);

        bool ExcluiNovel(Novel novel);
        bool ExcluiComic(Comic comic);

        Task<Novel> RetornaNovelExistente(string titulo);
        Task<Comic> RetornaComicExistente(string titulo);

        Task InsereGenerosNovel(Novel novel, List<string> ListaGeneros, bool inclusao);
        Task InsereGenerosComic(Comic comic, List<string> ListaGeneros, bool inclusao);

        bool AlteracoesSalvas();
    }
}
