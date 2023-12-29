using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IGeneroRepositoryOld
    {
        Task<List<Genero>> RetornaListaGeneros();
        
        Task AdicionaGeneroNovel(GeneroNovel generoNovel);
        Task AdicionaGeneroComic(GeneroComic generoComic);
                
        void ExcluiGeneroNovel(GeneroNovel generoNovel);
        void ExcluiGeneroComic(GeneroComic generoComic);
                
        Task<List<RetornoGenero>> CarregaListaGenerosNovel(List<GeneroNovel> generosNovel);
        Task<List<RetornoGenero>> CarregaListaGenerosComic(List<GeneroComic> generosComic);
    }
}
