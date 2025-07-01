using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IGeneroDeParaRepository
    {
        Task<List<Genero>> CarregaListaGenerosNovel(List<GeneroNovel> generoNovels);
        Task<List<Genero>> CarregaListaGenerosComic(List<GeneroComic> generoComics);

        Task AdicionaGeneroNovel(GeneroNovel generoNovel);
        Task AdicionaGeneroComic(GeneroComic generoComic);

        void ExcluiGeneroNovel(GeneroNovel generoNovel);
        void ExcluiGeneroComic(GeneroComic generoComic);        
    }
}
