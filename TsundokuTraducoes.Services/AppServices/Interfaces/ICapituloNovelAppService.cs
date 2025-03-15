using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface ICapituloNovelAppService
    {
        Task<List<RetornoCapituloNovel>> ObterCapitulosNovelPorIdObra(Guid idObra);
    }
}
