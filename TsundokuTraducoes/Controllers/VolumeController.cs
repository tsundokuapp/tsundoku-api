using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Controllers
{
    [ApiController]
    public class VolumeController : ControllerBase
    {
        // TODO - Criar as verificações de request, para saber se estar vindo corretamente.
        private readonly IVolumeService _volumeService;

        public VolumeController(IVolumeService volumeService)
        {
            _volumeService = volumeService;
        }
                
        [HttpGet("api/volume/")]
        public async Task<IActionResult> RetornaListaVolume([FromQuery] Guid? IdObra)
        {
            var result = await _volumeService.RetornaListaVolumes(IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        
        [HttpGet("api/volume/novel/{id}")]
        public async Task<IActionResult> RetornaVolumeNovelPorId(Guid id)
        {
            var result = await _volumeService.RetornaVolumeNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/volume/comic/{id}")]
        public async Task<IActionResult> RetornaVolumeComicPorId(Guid id)
        {
            var result = await _volumeService.RetornaVolumeComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }


        [HttpPost("api/volume/novel/")]
        public async Task<IActionResult> AdicionaVolumeNovel([FromForm] VolumeDTO volumeDTO)
        {
            var result = await _volumeService.AdicionaVolumeNovel(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/volume/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/volume/comic/")]
        public async Task<IActionResult> AdicionaVolumeComic([FromForm] VolumeDTO volumeDTO)
        {
            var result = await _volumeService.AdicionaVolumeComic(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/volume/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/volume/novel/")]
        public async Task<IActionResult> AtualizaVolumeNovel([FromForm] VolumeDTO volumeDTO)
        {
            var result = await _volumeService.AtualizaVolumeNovel(volumeDTO);
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
        public async Task<IActionResult> AtualizaVolumeComic([FromForm] VolumeDTO volumeDTO)
        {
            var result = await _volumeService.AtualizaVolumeComic(volumeDTO);
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
        public async Task<IActionResult> ExcluiVolumeNovel(Guid id)
        {
            var result = await _volumeService.ExcluiVolumeNovel(id);
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
        public async Task<IActionResult> ExcluiVolumeComic(Guid id)
        {
            var result = await _volumeService.ExcluiVolumeComic(id);
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
