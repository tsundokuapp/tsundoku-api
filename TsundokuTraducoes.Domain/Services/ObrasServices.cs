using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Domain.Services
{
    public class ObrasServices : IObrasService
    {
        private readonly IObrasRepository _obrasRepository;

        public ObrasServices(IObrasRepository obrasRepository)
        {
            _obrasRepository = obrasRepository;
        }
        
        public async Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras)
        {
            return await _obrasRepository.ObterListaNovels(requestObras);
        }
        
        public async Task<List<RetornoObra>> ObterListaComics(RequestObras requestObras)
        {
            return await _obrasRepository.ObterListaComics(requestObras);
        }
        
        
        public async Task<List<RetornoObra>> ObterListaNovelsRecentes()
        {
            return await _obrasRepository.ObterListaNovelsRecentes();
        }
        
        public async Task<List<RetornoObra>> ObterListaComicsRecentes()
        {
            return await _obrasRepository.ObterListaComicsRecentes();
        }
                
        
        public async Task<RetornoObra> ObterNovelPorId(RequestObras requestObras)
        {
            return await _obrasRepository.ObterNovelPorId(requestObras);
        }
        
        public async Task<RetornoObra> ObterComicPorId(RequestObras requestObras)
        {
            return await _obrasRepository.ObterComicPorId(requestObras);
        }

        
        public async Task<List<RetornoCapitulos>> ObterCapitulosHome()
        {
            return await _obrasRepository.ObterCapitulosHome();
        }

        public async Task<List<RetornoObrasRecomendadas>> ObterObrasRecomendadas()
        {
            return await _obrasRepository.ObterObrasRecomendadas();
        }

        public List<RetornoVolume> ObterListaVolumeCapitulos(string idObra)
        {
            return _obrasRepository.ObterListaVolumeCapitulos(idObra);
        }
    }
}
