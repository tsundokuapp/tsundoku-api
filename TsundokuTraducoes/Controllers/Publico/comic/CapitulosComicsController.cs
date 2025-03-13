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
        [HttpGet("api/publico/comics/{idObra}/{idCapitulo}")]
        public async Task<IActionResult> ObterCapituloPorComicPorId(Guid idObra, Guid idCapitulo)
        {
            var capitulos = await service.ObterCapitulosPorComicPorIdObra(idObra);
                        
            var objetoRetorno = RequestHelper.CriarObjetoRetonoCapitulosComics(HttpContext, capitulos, idCapitulo);
            return Ok(objetoRetorno);
        }
    }
}
