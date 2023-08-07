using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Public;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Api.Services
{
    public class InfosObrasService : IInfosObrasServices
    {
        private readonly IInfosObrasRepository _infosObrasRepository;
        public InfosObrasService(IInfosObrasRepository infosObrasRepository)
        {
            _infosObrasRepository = infosObrasRepository;
        }

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

        public ConteudoCapituloNovelDTO ObterCapituloPorSlug(string slug)
        {
            return _infosObrasRepository.ObterCapituloPorSlug(slug);
        }
    }
}
