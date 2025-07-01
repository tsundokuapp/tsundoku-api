using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;
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
        
        public async Task<List<Novel>> ObterListaNovels(RequestObras requestObras)
        {
            return await _obrasRepository.ObterListaNovels(requestObras);
        }
        
        public async Task<List<Novel>> ObterListaNovelsRecentes()
        {
            return await _obrasRepository.ObterListaNovelsRecentes();
        }
        
        public async Task<Novel> ObterNovelPorId(Guid id)
        {
            return await _obrasRepository.ObterNovelPorId(id);
        }

        public async Task<Novel> ObterNovelPorSlug(string slug)
        {
            return await _obrasRepository.ObterNovelPorSlug(slug);
        }
        
        public async Task<List<Novel>> ObterListaNovelsRecomendadas()
        {
            return await _obrasRepository.ObterListaNovelsRecomendadas();
        }
        
        
        public async Task<List<Comic>> ObterListaComics(RequestObras requestObras)
        {
            return await _obrasRepository.ObterListaComics(requestObras);
        }        
        
        public async Task<List<Comic>> ObterListaComicsRecentes()
        {
            return await _obrasRepository.ObterListaComicsRecentes();
        }
        
        public async Task<Comic> ObterComicPorId(Guid id)
        {
            return await _obrasRepository.ObterComicPorId(id);
        }

        public async Task<Comic> ObterComicPorSlug(string slug)
        {
            return await _obrasRepository.ObterComicPorSlug(slug);
        }

        public async Task<List<Comic>> ObterListaComicsRecomendadas()
        {
            return await _obrasRepository.ObterListaComicsRecomendadas();
        }
                
                
        public async Task<List<RetornoCapitulosHome>> ObterCapitulosHome()
        {
            return await _obrasRepository.ObterCapitulosHome();
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

        public async Task<List<RetornoObrasPesquisa>> ObterObrasPesquisa(string obra)
        {
            return await _obrasRepository.ObterObrasPesquisa(obra);
        }
    }
}