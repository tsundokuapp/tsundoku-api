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
    public class CapituloNovelAppService(ICapituloNovelService service, IObraService obraservice, IMapper mapper) : ICapituloNovelAppService, IBaseService<ICapituloNovelAppService, IObraService, IMapper>
    {
        public async Task<Result<List<RetornoCapituloNovel>>> ObterCapitulosNovelPorIdObraEIdCapitulo(Guid idObra, Guid idCapitulo)
        {
            var obra = obraservice.RetornaNovelPorId(idObra);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var listaRetornoCapituloNovel = new List<RetornoCapituloNovel>();
            var listaCapituloNovel = await service.ObterCapitulosNovelPorIdObra(idObra);

            foreach (var capituloNovel in listaCapituloNovel)
            {
                listaRetornoCapituloNovel.Add(TrataRetornoCapituloNovel(capituloNovel));
            }

            var resultExisteCapitulo = ValidaExisteCapituloNaLista(listaRetornoCapituloNovel, idCapitulo);
            if (resultExisteCapitulo.IsFailed)
                return Result.Fail(resultExisteCapitulo.Errors[0].Message);

            return Result.Ok(listaRetornoCapituloNovel);
        }

        public async Task<Result<List<RetornoCapituloNovel>>> ObterCapitulosNovelPorSlugObraEIdCapitulo(string slugObra, Guid idCapitulo)
        {
            var obra = obraservice.RetornaNovelPorSlug(slugObra);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var listaRetornoCapituloNovel = new List<RetornoCapituloNovel>();
            var listaCapituloNovel = await service.ObterCapitulosNovelPorSlugObra(slugObra);

            foreach (var capituloNovel in listaCapituloNovel)
            {
                listaRetornoCapituloNovel.Add(TrataRetornoCapituloNovel(capituloNovel));
            }

            var resultExisteCapitulo = ValidaExisteCapituloNaLista(listaRetornoCapituloNovel, idCapitulo);
            if (resultExisteCapitulo.IsFailed)
                return Result.Fail(resultExisteCapitulo.Errors[0].Message);

            return Result.Ok(listaRetornoCapituloNovel);
        }
        
        public async Task<Result<List<RetornoCapituloNovel>>> ObterCapitulosNovelPorSlugObraESlugCapitulo(string slugObra, string slugCapitulo)
        {
            var obra = obraservice.RetornaNovelPorSlug(slugObra);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var listaRetornoCapituloNovel = new List<RetornoCapituloNovel>();
            var listaCapituloNovel = await service.ObterCapitulosNovelPorSlugObra(slugObra);

            foreach (var capituloNovel in listaCapituloNovel)
            {
                listaRetornoCapituloNovel.Add(TrataRetornoCapituloNovel(capituloNovel));
            }

            var resultExisteCapitulo = ValidaExisteCapituloNaLista(listaRetornoCapituloNovel, slugCapitulo);
            if (resultExisteCapitulo.IsFailed)
                return Result.Fail(resultExisteCapitulo.Errors[0].Message);

            return Result.Ok(listaRetornoCapituloNovel);
        }
        

        public Result ValidaExisteCapituloNaLista(List<RetornoCapituloNovel> capitulos, Guid idCapitulo)
        {

            if (capitulos.FirstOrDefault(x => x.Id == idCapitulo) is null)
                return Result.Fail("Capítulo não encontrado!");

            return Result.Ok();
        }
        
        public Result ValidaExisteCapituloNaLista(List<RetornoCapituloNovel> capitulos, string slugCapitulo)
        {

            if (capitulos.FirstOrDefault(x => x.Slug == slugCapitulo) is null)
                return Result.Fail("Capítulo não encontrado!");

            return Result.Ok();
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
