using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
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
        
        public async Task<List<RetornoObras>> ObterListaNovels(RequestObras requestObras)
        {
            return await _obrasRepository.ObterListaNovels(requestObras);
        }
        
        public async Task<List<RetornoObras>> ObterListaComics(RequestObras requestObras)
        {
            return await _obrasRepository.ObterListaComics(requestObras);
        }
        
        
        public async Task<List<RetornoObras>> ObterListaNovelsRecentes()
        {
            return await _obrasRepository.ObterListaNovelsRecentes();
        }
        
        public async Task<List<RetornoObras>> ObterListaComicsRecentes()
        {
            return await _obrasRepository.ObterListaComicsRecentes();
        }
                
        
        public async Task<RetornoNovel> ObterNovelPorId(Guid id)
        {
            return await _obrasRepository.ObterNovelPorId(id);
        }
        
        public async Task<RetornoNovel> ObterNovelPorSlug(string slug)
        {
            return await _obrasRepository.ObterNovelPorSlug(slug);
        }
        
        public async Task<RetornoComic> ObterComicPorId(Guid id)
        {
            return await _obrasRepository.ObterComicPorId(id);
        }
        
        public async Task<RetornoComic> ObterComicPorSlug(string slug)
        {
            return await _obrasRepository.ObterComicPorSlug(slug);
        }
        
        public async Task<List<RetornoCapitulosHome>> ObterCapitulosHome()
        {
            return await _obrasRepository.ObterCapitulosHome();
        }

        public async Task<List<RetornoObrasRecomendadas>> ObterObrasRecomendadas()
        {
            return await _obrasRepository.ObterObrasRecomendadas();
        }

        public List<RetornoVolumes> ObterListaVolumeCapitulos(string idObra)
        {
            return _obrasRepository.ObterListaVolumeCapitulos(idObra);
        }

        public async Task<CapituloComic> ObterCapituloComicPorId(Guid id)
        {
            return await _obrasRepository.ObterCapituloComicPorId(id);
        }

        public async Task<CapituloNovel> ObterCapituloNovelPorId(Guid id)
        {
            return await _obrasRepository.ObterCapituloNovelPorId(id);
        }
    }
}
