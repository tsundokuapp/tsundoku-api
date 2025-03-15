using AutoMapper;
using FluentResults;
using Newtonsoft.Json;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{   
    public class CapituloComicAppService(ICapituloComicService service, IObraService obraservice, IMapper mapper) : ICapituloComicAppService, IBaseService<ICapituloComicAppService, IObraService, IMapper>
    {
        public async Task<Result<List<RetornoCapituloComic>>> ObterCapitulosComicPorIdObraEIdCapitulo(Guid idObra, Guid idCapitulo)
        {
            var listaRetornoCapituloComic = new List<RetornoCapituloComic>();
            
            var obra = obraservice.RetornaComicPorId(idObra);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var listaCapituloComic = await service.ObterCapitulosComicPorIdObra(idObra);

            foreach (var capituloComic in listaCapituloComic)
            {
                listaRetornoCapituloComic.Add(TrataRetornoCapituloComic(capituloComic));            
            }

            var resultExisteCapitulo = ValidaExisteCapituloNaLista(listaRetornoCapituloComic, idCapitulo);
            if (resultExisteCapitulo.IsFailed)
                return Result.Fail(resultExisteCapitulo.Errors[0].Message);

            return Result.Ok(listaRetornoCapituloComic);
        }

        public Result ValidaExisteCapituloNaLista(List<RetornoCapituloComic> capitulos, Guid idCapitulo)
        {

            if (capitulos.FirstOrDefault(x => x.Id == idCapitulo) is null)
                return Result.Fail("Capítulo não encontrado!");

            return Result.Ok();
        }

        public RetornoCapituloComic TrataRetornoCapituloComic(CapituloComic CapituloComic)
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