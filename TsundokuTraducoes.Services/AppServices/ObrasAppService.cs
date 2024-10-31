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

        public async Task<List<RetornoObras>> ObterListaNovels(RequestObras requestObras)
        {
            var listaRetornoObra = await _obrasService.ObterListaNovels(requestObras);
            return listaRetornoObra;
        }

        public async Task<List<RetornoObras>> ObterListaComics(RequestObras requestObras)
        {
            var listaRetornoObra = await _obrasService.ObterListaComics(requestObras);
            return listaRetornoObra;
        }


        public async Task<List<RetornoObras>> ObterListaNovelsRecentes()
        {
            var listaRetornoObra = await _obrasService.ObterListaNovelsRecentes();
            return listaRetornoObra;
        }

        public async Task<List<RetornoObras>> ObterListaComicsRecentes()
        {
            var listaRetornoObra = await _obrasService.ObterListaComicsRecentes();
            return listaRetornoObra;
        }


        public async Task<RetornoObras> ObterNovelPorId(RequestObras requestObras)
        {
            var retornoObra = await _obrasService.ObterNovelPorId(requestObras);
            return retornoObra;
        }

        public async Task<RetornoObras> ObterComicPorId(RequestObras requestObras)
        {
            var retornoObra = await _obrasService.ObterComicPorId(requestObras);
            return retornoObra;
        }


        public async Task<List<RetornoCapitulosHome>> ObterCapitulosHome()
        {
            return await _obrasService.ObterCapitulosHome();
        }

        public async Task<List<RetornoObrasRecomendadas>> ObterObrasRecomendadas()
        {
            return await _obrasService.ObterObrasRecomendadas();
        }

        public List<RetornoVolumes> ObterListaVolumeCapitulos(RequestObras requestObras)
        {
            return _obrasService.ObterListaVolumeCapitulos(requestObras.IdObra);
        }
    }
}
