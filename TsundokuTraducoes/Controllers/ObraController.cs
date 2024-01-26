using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TsundokuTraducoes.Services.AppServices.Interfaces;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Models
{
    [ApiController]
    public class ObraController : ControllerBase
    {        
        private readonly IObraAppService _obraAppService;
        public ObraController(IObraAppService obraAppService)
        {
            _obraAppService = obraAppService;
        }

        [HttpGet("api/obra/")]
        public async Task<IActionResult> RetornaListaObras()
        {
            var result = await _obraAppService.RetornaListaObras();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        [HttpGet("api/obra/novels")]
        public async Task<IActionResult> RetornaListaNovels()
        {
            var result = await _obraAppService.RetornaListaNovels();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        [HttpGet("api/obra/comics")]
        public async Task<IActionResult> RetornaListaComics()
        {
            var result = await _obraAppService.RetornaListaComics();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }


        [HttpGet("api/obra/novel/{id}")]
        public async Task<IActionResult> RetornaNovelPorId(Guid id)
        {
            var result = await _obraAppService.RetornaNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/obra/comic/{id}")]
        public async Task<IActionResult> RetornaComicPorId(Guid id)
        {
            var result = await _obraAppService.RetornaComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        
        [HttpPost("api/obra/novel")]
        public async Task<IActionResult> AdicionaNovel([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraAppService.AdicionaNovel(obraDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/obra/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/obra/comic")]
        public async Task<IActionResult> AdicionaComic([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraAppService.AdicionaComic(obraDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/obra/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/obra/novel")]
        public async Task<IActionResult> AtualizarNovel([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraAppService.AtualizaNovel(obraDTO);
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
            var result = await _obraAppService.AtualizaComic(obraDTO);
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
            var result = await _obraAppService.ExcluiNovel(id);
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
            var result = await _obraAppService.ExcluiComic(id);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrada"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }
    }
}
