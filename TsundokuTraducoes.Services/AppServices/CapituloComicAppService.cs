using AutoMapper;
using Newtonsoft.Json;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{   
    public class CapituloComicAppService(ICapituloComicService service, IMapper mapper) : ICapituloComicAppService, IBaseService<ICapituloComicAppService, IMapper>
    {
        public async Task<List<RetornoCapituloComic>> ObterCapitulosPorComicPorIdObra(Guid idObra)
        {
            var listaRetornoCapituloComic = new List<RetornoCapituloComic>();
            var listaCapituloComic = await service.ObterCapitulosPorComicPorIdObra(idObra);

            foreach (var capituloComic in listaCapituloComic)
            {
                listaRetornoCapituloComic.Add(TrataRetornoCapituloComic(capituloComic));
            }

            return listaRetornoCapituloComic;
        }

        private RetornoCapituloComic TrataRetornoCapituloComic(CapituloComic CapituloComic)
        {
            var retornoCapitulo = mapper.Map<RetornoCapituloComic>(CapituloComic);
            retornoCapitulo.DataInclusao = CapituloComic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = CapituloComic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(CapituloComic.UsuarioAlteracao) ? CapituloComic.UsuarioAlteracao : null;

            if (!string.IsNullOrEmpty(CapituloComic.ListaImagensJson))
            {
                retornoCapitulo.ListaImagens = JsonConvert.DeserializeObject<List<EnderecoImagemDTO>>(CapituloComic.ListaImagensJson);
            }

            return retornoCapitulo;
        }
    }
}