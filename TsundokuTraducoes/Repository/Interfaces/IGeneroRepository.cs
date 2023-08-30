using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.DePara;
using TsundokuTraducoes.Api.Models.Generos;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IGeneroRepository
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
