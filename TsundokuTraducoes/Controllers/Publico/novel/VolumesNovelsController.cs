using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [ProducesResponseType(typeof(List<RetornoVolumeNovel>), statusCode: 200)]
        public async Task<IActionResult> ObterVolumesNovelPorIdObra(Guid idObra)
        {
            var result = await service.ObterVolumesNovelPorIdObra(idObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(result.Value);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/novels/volumes/slug/{slugObra}")]
        [ProducesResponseType(typeof(List<RetornoVolumeNovel>), statusCode: 200)]
        public async Task<IActionResult> ObterVolumesNovelPorSlugObra(string slugObra)
        {
            var result = await service.ObterVolumesNovelPorSlugObra(slugObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(result.Value);
            return Ok(objetoRetorno);
        }
    }
}
