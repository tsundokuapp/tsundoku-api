using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IGeneroDeParaService
    {
        Task<List<Genero>> CarregaListaGenerosNovel(List<GeneroNovel> generoNovels);
        Task<List<Genero>> CarregaListaGenerosComic(List<GeneroComic> generoComics);
    }
}
