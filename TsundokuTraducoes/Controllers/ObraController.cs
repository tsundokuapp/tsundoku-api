using Microsoft.AspNetCore.Mvc;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Models
{
    [Route("api/[controller]")]
    [ApiController]

    public class ObraController : ControllerBase
    {
        private readonly IObraService _obraService;
        public ObraController(IObraService obraService)
        {  
            _obraService = obraService;
        }       

        [HttpGet]
        public IActionResult RetornaListaObras()
        {
            var result = _obraService.RetornaListaObras();               
            if (result.IsFailed)
            {  
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }
        
        [HttpGet("{id}")]
        public IActionResult RetornaObraPorId(int id)
        {
            var result = _obraService.RetornaObraPorId(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }
        
        [HttpPost]
        public IActionResult AdicionaObra([FromForm] ObraDTO obraDTO)
        {  
            var result = _obraService.AdicionaObra(obraDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);                      
        }

        [HttpPut]
        public IActionResult AtualizarObra([FromForm] ObraDTO obraDTO)
        {
            var result = _obraService.AtualizarObra(obraDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{idObra}")]
        public IActionResult ExcluirObra(int idObra)
        {
            var result = _obraService.ExcluirObra(idObra);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }

        [Route("informacoes-obra")]
        [HttpGet]
        public IActionResult RetornaInformacoes()
        {
            var result = _obraService.RetornaInformacaoObraDTO();
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [Route("informacoes-obra/{idObra}")]
        [HttpGet]
        public IActionResult RetornaInformacoesObra(int idObra)
        {
            var result = _obraService.RetornaInformacaoObraDTO(idObra);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }       
    }
}
