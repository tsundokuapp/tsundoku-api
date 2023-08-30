using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Models
{
    [ApiController]
    public class ObraController : ControllerBase
    {
        // TODO - Criar as verificações de request, para saber se estar vindo corretamente.
        private readonly IObraService _obraService;
        public ObraController(IObraService obraService)
        {
            _obraService = obraService;
        }

        [HttpGet("api/obra/")]
        public async Task<IActionResult> RetornaListaObras()
        {
            var result = await _obraService.RetornaListaObras();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        
        [HttpGet("api/obra/novel/{id}")]
        public async Task<IActionResult> RetornaNovelPorId(Guid id)
        {
            var result = await _obraService.RetornaNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/obra/comic/{id}")]
        public async Task<IActionResult> RetornaComicPorId(Guid id)
        {
            var result = await _obraService.RetornaComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        
        [HttpPost("api/obra/novel")]
        public async Task<IActionResult> AdicionaNovel([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraService.AdicionaNovel(obraDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/obra/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/obra/comic")]
        public async Task<IActionResult> AdicionaComic([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraService.AdicionaComic(obraDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/obra/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/obra/novel")]
        public async Task<IActionResult> AtualizarNovel([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraService.AtualizaNovel(obraDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrada"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpPut("api/obra/comic")]
        public async Task<IActionResult> AtualizarComic([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraService.AtualizaComic(obraDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrada"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }


        [HttpDelete("api/obra/novel/{id}")]
        public async Task<IActionResult> ExcluirNovel(Guid id)
        {
            var result = await _obraService.ExcluiNovel(id);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrada"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }

        [HttpDelete("api/obra/comic/{id}")]
        public async Task<IActionResult> ExcluirComic(Guid id)
        {
            var result = await _obraService.ExcluiComic(id);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrada"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }


        [Route("informacoes-obra")]
        [HttpGet]
        public async Task<IActionResult> RetornaInformacoes()
        {
            var result = await _obraService.RetornaInformacaoObraDTO();
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [Route("informacoes-obra/{idObra}")]
        [HttpGet]
        public async Task<IActionResult> RetornaInformacoesObra(Guid? idObra)
        {
            var result = await _obraService.RetornaInformacaoObraDTO(idObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }
    }
}
