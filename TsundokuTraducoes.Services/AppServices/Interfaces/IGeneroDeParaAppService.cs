using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IGeneroDeParaAppService
    {
        Task<List<RetornoGenero>> CarregaListaGenerosNovel(List<GeneroNovel> generosNovels);
        Task<List<RetornoGenero>> CarregaListaGenerosComic(List<GeneroComic> generosComics);


        Task AdicionaGeneroNovel(GeneroNovel generoNovel);
        Task AdicionaGeneroComic(GeneroComic generoComic);


        void ExcluiGeneroNovel(GeneroNovel generoNovel);
        void ExcluiGeneroComic(GeneroComic generoComic);

        
    }
}
