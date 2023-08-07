using Microsoft.AspNetCore.Mvc;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapituloController : ControllerBase
    {   
        private readonly ICapituloService _capituloService;

        public CapituloController(ICapituloService capituloService)
        {
            _capituloService = capituloService;
        }
        
        [HttpGet]
        public IActionResult RetornaListaCapitulos()
        {
            var result = _capituloService.RetornaListaCapitulos();
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        #region Comic

        [HttpGet]
        [Route("comic/{id}")]
        public IActionResult RetornaCapituloComicPorId(int id)
        {
            var result = _capituloService.RetornaCapituloComicPorId(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("comic")]
        public IActionResult AdicionaCapituloComic([FromForm] CapituloDTO capituloDTO)
        {
            var result = _capituloService.AdicionaCapituloComic(capituloDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpPut]
        [Route("comic")]
        public IActionResult AtualizaCapituloComic([FromForm] CapituloDTO capituloDTO)
        {
            var result = _capituloService.AtualizaCapituloComic(capituloDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpDelete]
        [Route("comic/{id}")]
        public IActionResult ExcluiCapituloComic(int id)
        {
            var result = _capituloService.ExcluiCapituloComic(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }

        #endregion

        #region Novel

        [HttpGet]
        [Route("novel/{id}")]
        public IActionResult RetornaCapituloNovelPorId(int id)
        {
            var result = _capituloService.RetornaCapituloNovelPorId(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("novel")]
        public IActionResult AdicionaCapituloNovel([FromForm] CapituloDTO capituloDTO)
        {
            var result = _capituloService.AdicionaCapituloNovel(capituloDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpPut]
        [Route("novel")]
        public IActionResult AtualizaCapituloNovel([FromForm] CapituloDTO capituloDTO)
        {
            var result = _capituloService.AtualizaCapituloNovel(capituloDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpDelete]
        [Route("novel/{id}")]
        public IActionResult ExcluiCapitulo(int id)
        {
            var result = _capituloService.ExcluiCapituloNovel(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }

        #endregion

        [HttpGet]
        [Route("dados-obra/{id}")]
        public IActionResult RetornaDadosObra(int id)
        {
            var result = _capituloService.RetornaDadosObra(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("dados-capitulo/{id}")]
        public IActionResult RetornaDadosCapitulo(int id)
        {
            var result = _capituloService.RetornaDadosCapitulo(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }
    }
}
