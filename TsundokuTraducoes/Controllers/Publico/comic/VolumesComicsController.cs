using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers.Publico.comic
{
    [ApiController]
    public class VolumesComicsController(IVolumeComicAppService service) : Controller, IBaseService<IVolumeComicAppService>
    {
        [HttpGet("api/comics/volumes/{idObra}")]
        public async Task<IActionResult> ObterVolumesComicPorIdObra(Guid idObra, [FromQuery] int? skip, int? take)
        {
            var result = await service.ObterVolumesComicPorIdObra(idObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesComics(HttpContext, result.Value, skip, take);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/comics/volumes/slug/{slugObra}")]
        public async Task<IActionResult> ObterVolumesComicPorSlugObra(string slugObra, [FromQuery] int? skip, int? take)
        {
            var result = await service.ObterVolumesComicPorSlugObra(slugObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesComics(HttpContext, result.Value, skip, take);
            return Ok(objetoRetorno);
        }
    }
}
