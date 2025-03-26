using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers.Publico.novel
{
    [ApiController]
    public class VolumesNovelsController(IVolumeNovelAppService service) : Controller, IBaseService<IVolumeNovelAppService>
    {
        [HttpGet("api/novels/volumes/{idObra}")]
        [ProducesResponseType(typeof(RetornoVolumeNovel), statusCode: 200)]
        public async Task<IActionResult> ObterVolumesNovelPorIdObra(Guid idObra, [FromQuery] int? skip, int? take)
        {
            var result = await service.ObterVolumesNovelPorIdObra(idObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(HttpContext, result.Value, skip, take);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/novels/volumes/slug/{slugObra}")]
        [ProducesResponseType(typeof(RetornoVolumeNovel), statusCode: 200)]
        public async Task<IActionResult> ObterVolumesNovelPorSlugObra(string slugObra, [FromQuery] int? skip, int? take)
        {
            var result = await service.ObterVolumesNovelPorSlugObra(slugObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(HttpContext, result.Value, skip, take);
            return Ok(objetoRetorno);
        }
    }
}
