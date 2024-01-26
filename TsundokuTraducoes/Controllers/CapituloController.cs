using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Controllers
{
    [ApiController]
    public class CapituloController : ControllerBase
    {
        // TODO - Criar as verificações de request, para saber se estar vindo corretamente.
        private readonly ICapituloServiceOld _capituloService;

        public CapituloController(ICapituloServiceOld capituloService)
        {
            _capituloService = capituloService;
        }

        [HttpGet("api/capitulo/")]
        public async Task<IActionResult> RetornaListaCapitulos([FromQuery] Guid? volumeId)
        {
            var result = await _capituloService.RetornaListaCapitulos(volumeId);
            if (result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }


        [HttpGet("api/capitulo/novel/{id}")]
        public async Task<IActionResult> RetornaCapituloNovelPorId(Guid id)
        {
            var result = await _capituloService.RetornaCapituloNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/capitulo/comic/{id}")]
        public async Task<IActionResult> RetornaCapituloComicPorId(Guid id)
        {
            var result = await _capituloService.RetornaCapituloComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }


        [HttpPost("api/capitulo/novel/")]
        public async Task<IActionResult> AdicionaCapituloNovel([FromForm] CapituloDTO capituloDTO)
        {
            var result = await _capituloService.AdicionaCapituloNovel(capituloDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/capitulo/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/capitulo/comic/")]
        public async Task<IActionResult> AdicionaCapituloComic([FromForm] CapituloDTO capituloDTO)
        {
            var result = await _capituloService.AdicionaCapituloComic(capituloDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/capitulo/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/capitulo/novel/")]
        public async Task<IActionResult> AtualizaCapituloNovel([FromForm] CapituloDTO capituloDTO)
        {
            var result = await _capituloService.AtualizaCapituloNovel(capituloDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Value);
        }

        [HttpPut("api/capitulo/comic/")]
        public async Task<IActionResult> AtualizaCapituloComic([FromForm] CapituloDTO capituloDTO)
        {
            var result = await _capituloService.AtualizaCapituloComic(capituloDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Value);
        }


        [HttpDelete("api/capitulo/novel/{id}")]
        public async Task<IActionResult> ExcluiCapituloNovel(Guid id)
        {
            var result = await _capituloService.ExcluiCapituloNovel(id);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Successes[0].Message);
        }

        [HttpDelete("api/capitulo/comic/{id}")]
        public async Task<IActionResult> ExcluiCapituloComic(Guid id)
        {
            var result = await _capituloService.ExcluiCapituloComic(id);
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
