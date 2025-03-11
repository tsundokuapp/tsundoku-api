using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers.Publico.manga
{
    [ApiController]
    public class CapitulosComicsController(ICapituloComicAppService service) : Controller, IBaseService<ICapituloComicAppService>
    {
        [HttpGet("api/publico/comics/{idObra}/{idCapitulo}")]
        public async Task<IActionResult> ObterCapituloPorComicPorId(Guid idObra, Guid idCapitulo)
        {
            var capitulos = await service.ObterCapituloPorComicPorId(idObra, idCapitulo);
            return Ok(capitulos);
        }
    }
}
