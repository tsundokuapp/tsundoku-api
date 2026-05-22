using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IGeneroDeParaAppService
    {
        Task<List<RetornoGeneroObra>> CarregaListaGenerosNovel(List<GeneroNovel> generosNovels);
        Task<List<RetornoGeneroObra>> CarregaListaGenerosComic(List<GeneroComic> generosComics);        
    }
}
