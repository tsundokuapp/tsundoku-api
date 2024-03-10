using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Services.AppServices
{
    public class ObrasAppService
    {
        private readonly IObrasService _obrasService;
        public async Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras)
        {
            var listaRetornoObra = await _obrasService.ObterListaNovels(requestObras);
            return listaRetornoObra;
        }

        public async Task<List<RetornoObra>> ObterListaNovelsRecentes()
        {
            var listaRetornoObra = await _obrasService.ObterListaNovelsRecentes();
            return listaRetornoObra;
        }

        public async Task<RetornoObra> ObterNovelPorId(RequestObras requestObras)
        {
            var retornoObra = await _obrasService.ObterNovelPorId(requestObras);
            return retornoObra;
        }
    }
}
