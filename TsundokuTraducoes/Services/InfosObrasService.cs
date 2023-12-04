using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Public.Retorno;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin.Request;

namespace TsundokuTraducoes.Api.Services
{
    public class InfosObrasService : IInfosObrasServices
    {
        private readonly IInfosObrasRepository _infosObrasRepository;
        public InfosObrasService(IInfosObrasRepository infosObrasRepository)
        {
            _infosObrasRepository = infosObrasRepository;
        }

        public async Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras)
        {
            var listaRetornoObra = await _infosObrasRepository.ObterListaNovels(requestObras);
            return listaRetornoObra;
        }

        public async Task<List<RetornoObra>> ObterListaNovelsRecentes()
        {            
            var listaRetornoObra = await _infosObrasRepository.ObterListaNovelsRecentes();
            return listaRetornoObra;
        }

        public async Task<RetornoObra> ObterNovelPorId(RequestObras requestObras)
        {
            var retornoObra = await _infosObrasRepository.ObterNovelsPorId(requestObras);
            return retornoObra;
        }


        public async Task<List<RetornoObra>> ObterListaComics(RequestObras requestObras)
        {
            var listaRetornoObra = await _infosObrasRepository.ObterListaComics(requestObras);
            return listaRetornoObra;
        }

        public async Task<List<RetornoObra>> ObterListaComicsRecentes()
        {
            var listaRetornoObra = await _infosObrasRepository.ObterListaComicsRecentes();
            return listaRetornoObra;
        }

        public async Task<RetornoObra> ObterComicPorId(RequestObras requestObras)
        {
            var retornoObra = await _infosObrasRepository.ObterComicPorId(requestObras);
            return retornoObra;
        }


        public async Task<List<RetornoCapitulos>> ObterCapitulosHome()
        {
            var listaRetornoCapitulo = await _infosObrasRepository.ObterCapitulosHome();
            return listaRetornoCapitulo;
        }
    }
}
