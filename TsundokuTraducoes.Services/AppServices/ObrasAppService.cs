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

        public async Task<List<RetornoObra>> ObterListaComics(RequestObras requestObras)
        {
            var listaRetornoObra = await _obrasService.ObterListaComics(requestObras);
            return listaRetornoObra;
        }


        public async Task<List<RetornoObra>> ObterListaNovelsRecentes()
        {
            var listaRetornoObra = await _obrasService.ObterListaNovelsRecentes();
            return listaRetornoObra;
        }

        public async Task<List<RetornoObra>> ObterListaComicsRecentes()
        {
            var listaRetornoObra = await _obrasService.ObterListaComicsRecentes();
            return listaRetornoObra;
        }


        public async Task<RetornoObra> ObterNovelPorId(RequestObras requestObras)
        {
            var retornoObra = await _obrasService.ObterNovelPorId(requestObras);
            return retornoObra;
        }

        public async Task<RetornoObra> ObterComicPorId(RequestObras requestObras)
        {
            var retornoObra = await _obrasService.ObterComicPorId(requestObras);
            return retornoObra;
        }


        public async Task<List<RetornoCapitulos>> ObterCapitulosHome()
        {
            throw new NotImplementedException();
        }
    }
}
