using Microsoft.AspNetCore.Mvc;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response;
using TsundokuTraducoes.Helpers.Services.Interfaces;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers.Publico.genero
{
    [ApiController]
    public class GenerosController(IGeneroAppService service) : Controller, IBaseService<IGeneroAppService>
    {
        [HttpGet("api/generos")]
        [ProducesResponseType(typeof(ObjetoRetornoGenerosCadastradosResponse), statusCode: 200)]
        public async Task<IActionResult> RetornaListaGeneros()
        {
            var result = await service.RetornaListaGenerosCadastrados();
            if (result.Value == null || result.Value.Count == 0)
               return NoContent();
            
            return Ok(result.Value);
        }
    }
}
