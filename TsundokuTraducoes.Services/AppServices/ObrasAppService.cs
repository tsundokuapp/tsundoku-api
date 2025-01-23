using AutoMapper;
using Newtonsoft.Json;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class ObrasAppService : IObrasAppService
    {
        private readonly IObrasService _obrasService;
        private readonly IMapper _mapper;

        public ObrasAppService(IObrasService obrasService, IMapper mapper)
        {
            _obrasService = obrasService;
            _mapper = mapper;
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

        public async Task<RetornoCapituloComic> ObterCapituloComicPorId(Guid id)
        {
            var capituloComic = await _obrasService.ObterCapituloComicPorId(id);
            if (capituloComic == null)
                return null;

            return TrataRetornoCapituloComic(capituloComic);
        }

        public async Task<RetornoCapituloNovel> ObterCapituloNovelPorId(Guid id)
        {
            var capituloNovel = await _obrasService.ObterCapituloNovelPorId(id);
            if (capituloNovel == null)
                return null;

            return TrataRetornoCapituloNovel(capituloNovel);
        }


        private RetornoCapituloComic TrataRetornoCapituloComic(CapituloComic CapituloComic)
        {
            var retornoCapitulo = _mapper.Map<RetornoCapituloComic>(CapituloComic);
            retornoCapitulo.DataInclusao = CapituloComic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = CapituloComic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(CapituloComic.UsuarioAlteracao) ? CapituloComic.UsuarioAlteracao : null;

            if (!string.IsNullOrEmpty(CapituloComic.ListaImagensJson))
            {
                retornoCapitulo.ListaImagens = JsonConvert.DeserializeObject<List<EnderecoImagemDTO>>(CapituloComic.ListaImagensJson);
            }

            return retornoCapitulo;
        }

        private RetornoCapituloNovel TrataRetornoCapituloNovel(CapituloNovel capituloNovel)
        {
            var retornoCapitulo = _mapper.Map<RetornoCapituloNovel>(capituloNovel);
            retornoCapitulo.DataInclusao = capituloNovel.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = capituloNovel.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao) ? capituloNovel.UsuarioAlteracao : null;

            if (capituloNovel.EhIlustracoesNovel && !string.IsNullOrEmpty(capituloNovel.ListaImagensJson))
            {
                retornoCapitulo.ListaImagens = JsonConvert.DeserializeObject<List<EnderecoImagemDTO>>(capituloNovel.ListaImagensJson);
            }

            return retornoCapitulo;
        }
    }
}
