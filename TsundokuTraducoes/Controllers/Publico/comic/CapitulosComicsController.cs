using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers.Publico.manga
{
    [ApiController]
    public class CapitulosComicsController(ICapituloComicAppService service) : Controller, IBaseService<ICapituloComicAppService>
    {
        [HttpGet("api/comics/{idObra}/{idCapitulo}")]
        public async Task<IActionResult> ObterCapitulosComicPorIdObraEIdCapitulo(Guid idObra, Guid idCapitulo)
        {
            var result = await service.ObterCapitulosComicPorIdObraEIdCapitulo(idObra, idCapitulo);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosComics(HttpContext, result.Value, idCapitulo);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/comics/slug/{slugObra}/{idCapitulo}")]
        public async Task<IActionResult> ObterCapitulosComicPorSlugObraEIdCapitulo(string slugObra, Guid idCapitulo)
        {
            var result = await service.ObterCapitulosComicPorSlugObraEIdCapitulo(slugObra, idCapitulo);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosPorSlugComics(HttpContext, result.Value, idCapitulo);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/comics/slug/{slugObra}")]
        public async Task<IActionResult> ObterListaCapitulosComicPorSlugObra(string slugObra, [FromQuery] int? skip, int? take)
        {
            var result = await service.ObterListaCapitulosComicPorSlugObra(slugObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetornoCapitulosPorSlugObra(HttpContext, result.Value, skip, take);
            return Ok(objetoRetorno);
        }
    }
}
