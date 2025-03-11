using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{   
    public class CapituloComicAppService(ICapituloComicService service) : ICapituloComicAppService, IBaseService<ICapituloComicAppService>
    {
        public async Task<RetornoCapituloComic> ObterCapituloPorComicPorId(Guid idObra, Guid idCapitulo)
        {
            var capituloComic = await service.ObterCapituloPorComicPorId(idObra, idCapitulo);

            return new RetornoCapituloComic();
        }
    }
}