using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Public.Retorno;
using TsundokuTraducoes.Api.DTOs.Public;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace TsundokuTraducoes.Api.Services
{
    public class InfosObrasService : IInfosObrasServices
    {
        private readonly IInfosObrasRepository _infosObrasRepository;
        public InfosObrasService(IInfosObrasRepository infosObrasRepository)
        {
            _infosObrasRepository = infosObrasRepository;
        }

        public async Task<List<RetornoObra>> ObterListaNovels(string pesquisar, string nacionalidade, string status, string tipo, string genero)
        {
            var listaRetornoObra = await _infosObrasRepository.ObterListaNovels(pesquisar, nacionalidade,status, tipo, genero);
            return listaRetornoObra;
        }

        public async Task<List<RetornoObra>> ObterListaNovelsRecentes()
        {            
            var listaRetornoObra = await _infosObrasRepository.ObterListaNovelsRecentes();
            return listaRetornoObra;
        }


        // TODO - Será verificado se vai ser reaproveitado enquanto é trabalhado nos backlogs

        public List<DadosCapitulosDTO> ObterCapitulos()
        {
            return _infosObrasRepository.ObterCapitulos();
        }       

        public List<DadosCapitulosDTO> ObterCapitulos(string pesquisar, string nacionalidade, string status, string tipo, string genero, bool ehNovel)
        {
            var listaDadosCapitulosDTO = new List<DadosCapitulosDTO>();

            if (!string.IsNullOrEmpty(pesquisar))
            {
                listaDadosCapitulosDTO = _infosObrasRepository.ObterCapitulos(pesquisar, ehNovel);
            }
            else
            {
                listaDadosCapitulosDTO = _infosObrasRepository.ObterCapitulos(nacionalidade, status, tipo, genero, ehNovel);
            }

            return listaDadosCapitulosDTO;
        }

        public ObraDTO ObterObraPorSlug(string slug)
        {
            return _infosObrasRepository.ObterObraPorSlug(slug);
        }

        public ConteudoCapituloNovelDTO ObterCapituloNovelPorSlug(string slug)
        {
            return _infosObrasRepository.ObterCapituloNovelPorSlug(slug);
        }

        public ConteudoCapituloComicDTO ObterCapituloComicPorSlug(string slugCapitulo)
        {
            return _infosObrasRepository.ObterCapituloComicPorSlug(slugCapitulo);
        }
    }
}
