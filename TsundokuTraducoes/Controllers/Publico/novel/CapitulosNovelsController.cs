using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers.Publico.novel
{
    [ApiController]
    public class CapitulosNovelsController(ICapituloNovelAppService service) : Controller, IBaseService<ICapituloNovelAppService>
    {
        [HttpGet("api/novels/capitulos/id/{idObra}/{idCapitulo}")]
        public async Task<IActionResult> ObterCapituloNovelPorId(Guid idObra, Guid idCapitulo)
        {
            var result = await service.ObterCapitulosNovelPorIdObraEIdCapitulo(idObra, idCapitulo);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosNovel(HttpContext, result.Value, idCapitulo);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/novels/capitulos/slug/{slugObra}/{idCapitulo}")]
        public async Task<IActionResult> ObterCapitulosNovelPorSlugObraEIdCapitulo(string slugObra, Guid idCapitulo)
        {
            var result = await service.ObterCapitulosNovelPorSlugObraEIdCapitulo(slugObra, idCapitulo);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosPorSlugNovels(HttpContext, result.Value, idCapitulo);
            return Ok(objetoRetorno);
        }
        
        [HttpGet("api/novels/capitulos/{slugObra}/{slugCapitulo}")]
        public async Task<IActionResult> ObterCapitulosNovelPorSlugObraESlugCapitulo(string slugObra, string slugCapitulo)
        {
            var result = await service.ObterCapitulosNovelPorSlugObraESlugCapitulo(slugObra, slugCapitulo);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoNovelSlugECapituloSlug(HttpContext, result.Value, slugCapitulo);
            return Ok(objetoRetorno);
        }
    }
}
