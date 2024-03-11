using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class ObrasAppService : IObrasAppService
    {
        private readonly IObrasService _obrasService;

        public ObrasAppService(IObrasService obrasService)
        {
            _obrasService = obrasService;
        }

        public async Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras)
        {
            var listaRetornoObra = await _obrasService.ObterListaNovels(requestObras);
            return listaRetornoObra;
        }

        public Task<List<RetornoObra>> ObterListaComics(RequestObras requestObras)
        {
            throw new NotImplementedException();
        }


        public async Task<List<RetornoObra>> ObterListaNovelsRecentes()
        {
            var listaRetornoObra = await _obrasService.ObterListaNovelsRecentes();
            return listaRetornoObra;
        }

        public Task<List<RetornoObra>> ObterListaComicsRecentes()
        {
            throw new NotImplementedException();
        }


        public async Task<RetornoObra> ObterNovelPorId(RequestObras requestObras)
        {
            var retornoObra = await _obrasService.ObterNovelPorId(requestObras);
            return retornoObra;
        }

        public Task<RetornoObra> ObterComicPorId(RequestObras requestObras)
        {
            throw new NotImplementedException();
        }


        public Task<List<RetornoCapitulos>> ObterCapitulosHome()
        {
            throw new NotImplementedException();
        }
    }
}
