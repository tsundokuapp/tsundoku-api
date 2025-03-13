using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface ICapituloComicAppService
    {
        Task<List<RetornoCapituloComic>> ObterCapitulosPorComicPorIdObra(Guid idObra);
    }
}
