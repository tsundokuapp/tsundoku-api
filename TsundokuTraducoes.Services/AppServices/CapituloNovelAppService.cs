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
    public class CapituloNovelAppService(ICapituloNovelService service, IMapper mapper) : ICapituloNovelAppService, IBaseService<ICapituloNovelAppService, IMapper>
    {
        public async Task<List<RetornoCapituloNovel>> ObterCapitulosNovelPorIdObra(Guid idObra)
        {
            var listaRetornoCapituloNovel = new List<RetornoCapituloNovel>();
            var listaCapituloNovel = await service.ObterCapitulosNovelPorIdObra(idObra);

            foreach (var capituloNovel in listaCapituloNovel)
            {
                listaRetornoCapituloNovel.Add(TrataRetornoCapituloNovel(capituloNovel));
            }

            return listaRetornoCapituloNovel;
        }

        public RetornoCapituloNovel TrataRetornoCapituloNovel(CapituloNovel capituloNovel)
        {
            var retornoCapitulo = mapper.Map<RetornoCapituloNovel>(capituloNovel);
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
