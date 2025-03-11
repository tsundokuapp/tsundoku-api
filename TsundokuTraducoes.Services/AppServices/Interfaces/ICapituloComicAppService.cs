using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface ICapituloComicAppService
    {
        Task<RetornoCapituloComic> ObterCapituloPorComicPorId(Guid idObra, Guid idCapitulo);
    }
}
