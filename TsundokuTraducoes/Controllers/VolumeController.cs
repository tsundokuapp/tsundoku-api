using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumeController : ControllerBase
    {
        private readonly IVolumeService _volumeService;

        public VolumeController(IVolumeService volumeService)
        {
            _volumeService = volumeService;
        }

        [HttpGet]
        public async Task<IActionResult> RetornaListaVolume([FromQuery] int? IdObra)
        {
            var result = await _volumeService.RetornaListaVolume(IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaVolumePorId(int id)
        {
            var result = await _volumeService.RetornaVolumePorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaVolume([FromForm] VolumeDTO volumeDTO)
        {
            var result = await _volumeService.AdicionaVolume(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"volume/{result.Value.Id}", result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizaVolume([FromForm] VolumeDTO volumeDTO)
        {
            var result = await _volumeService.AtualizaVolume(volumeDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluiVolume(int id)
        {
            var result = await _volumeService.ExcluiVolume(id);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Successes[0].Message);
        }
    }
}
