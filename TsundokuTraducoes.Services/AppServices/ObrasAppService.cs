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


        public async Task<RetornoNovel> ObterNovelPorId(Guid id)
        {
            var retornoNovel = await _obrasService.ObterNovelPorId(id);
            return retornoNovel;
        }
        
        public async Task<RetornoNovel> ObterNovelPorSlug(string slug)
        {
            var retornoNovel = await _obrasService.ObterNovelPorSlug(slug);
            return retornoNovel;
        }

        public async Task<RetornoComic> ObterComicPorId(Guid id)
        {
            var comic = await _obrasService.ObterComicPorId(id);
            return comic;
        }
        
        public async Task<RetornoComic> ObterComicPorSlug(string slug)
        {
            var comic = await _obrasService.ObterComicPorSlug(slug);
            return comic;
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
