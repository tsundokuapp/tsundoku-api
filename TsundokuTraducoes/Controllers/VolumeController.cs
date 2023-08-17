using Microsoft.AspNetCore.Mvc;
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
        public IActionResult RetornaListaVolume([FromQuery] int? IdObra)
        {
            var result = _volumeService.RetornaListaVolume(IdObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public IActionResult RetornaVolumePorId(int id)
        {
            var result = _volumeService.RetornaVolumePorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPost]
        public IActionResult AdicionaVolume([FromForm] VolumeDTO volumeDTO)
        {
            var result = _volumeService.AdicionaVolume(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPut]
        public IActionResult AtualizaVolume([FromForm] VolumeDTO volumeDTO)
        {
            var result = _volumeService.AtualizaVolume(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirVolume(int id)
        {
            //var result = _volumeService.ExcluirVolume(id);
            //if (result.IsFailed)
            //{
            //    return BadRequest(result.Errors[0].Message);
            //}

            //return Ok(result.Successes[0].Message);

            return Ok("Contatar os administradores do site para essa solicitação");
        }
    }
}
