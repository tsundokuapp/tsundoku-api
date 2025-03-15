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
        [HttpGet("api/publico/novels/{idObra}/{idCapitulo}")]
        public async Task<IActionResult> ObterCapituloNovelPorId(Guid idObra, Guid idCapitulo)
        {
            var capitulos = await service.ObterCapitulosNovelPorIdObra(idObra);

            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosNovel(HttpContext, capitulos, idCapitulo);
            return Ok(objetoRetorno);
        }
    }
}
