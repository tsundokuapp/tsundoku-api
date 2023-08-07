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
        
        [HttpGet("{id}")]
        public IActionResult RetornaCapituloPorId(int id)
        {
            var result = _capituloService.RetornaCapituloPorId(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }
        
        [HttpPost]
        public IActionResult AdcionaCapitulo([FromForm]CapituloDTO capituloDTO)
        {
            var result = _capituloService.AdicionaCapitulo(capituloDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpPut]
        public IActionResult AtualizaCapitulo([FromForm]CapituloDTO capituloDTO)
        {
            var result = _capituloService.AtualizaCapitulo(capituloDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }       

        [HttpDelete("{id}")]
        public IActionResult ExcluiCapitulo(int id)
        {
            var result = _capituloService.ExcluiCapitulo(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }

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
