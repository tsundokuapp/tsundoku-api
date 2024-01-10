using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Controllers
{
    [ApiController]
    public class VolumeController : ControllerBase
    {         
        private readonly IVolumeAppService _volumeAppService;

        public VolumeController(IVolumeAppService volumeAppService)
        {
            _volumeAppService = volumeAppService;
        }

        [HttpGet("api/volume/")]
        public IActionResult RetornaListaVolume([FromQuery] Guid? IdObra)
        {
            var result = _volumeAppService.RetornaListaVolumes(IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        [HttpGet("api/volume/novel")]
        public IActionResult RetornaListaVolumesNovel([FromQuery] Guid? IdObra)
        {
            var result = _volumeAppService.RetornaListaVolumesNovel(IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        [HttpGet("api/volume/comic")]
        public IActionResult RetornaListaVolumesComic([FromQuery] Guid? IdObra)
        {
            var result = _volumeAppService.RetornaListaVolumesComic(IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }


        [HttpGet("api/volume/novel/{id}")]
        public IActionResult RetornaVolumeNovelPorId(Guid id)
        {
            var result = _volumeAppService.RetornaVolumeNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/volume/comic/{id}")]
        public IActionResult RetornaVolumeComicPorId(Guid id)
        {
            var result = _volumeAppService.RetornaVolumeComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }


        [HttpPost("api/volume/novel/")]
        public IActionResult AdicionaVolumeNovel([FromForm] VolumeDTO volumeDTO)
        {
            var result = _volumeAppService.AdicionaVolumeNovel(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/volume/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/volume/comic/")]
        public IActionResult AdicionaVolumeComic([FromForm] VolumeDTO volumeDTO)
        {
            var result = _volumeAppService.AdicionaVolumeComic(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/volume/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/volume/novel/")]
        public IActionResult AtualizaVolumeNovel([FromForm] VolumeDTO volumeDTO)
        {
            var result = _volumeAppService.AtualizaVolumeNovel(volumeDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Value);
        }

        [HttpPut("api/volume/comic/")]
        public IActionResult AtualizaVolumeComic([FromForm] VolumeDTO volumeDTO)
        {
            var result = _volumeAppService.AtualizaVolumeComic(volumeDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Value);
        }


        [HttpDelete("api/volume/novel/{id}")]
        public IActionResult ExcluiVolumeNovel(Guid id)
        {
            var result = _volumeAppService.ExcluiVolumeNovel(id);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Successes[0].Message);
        }

        [HttpDelete("api/volume/comic/{id}")]
        public IActionResult ExcluiVolumeComic(Guid id)
        {
            var result = _volumeAppService.ExcluiVolumeComic(id);
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
