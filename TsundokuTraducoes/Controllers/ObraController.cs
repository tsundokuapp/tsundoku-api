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

        [HttpPost]
        [Route("recomendada")]
        public IActionResult AdicionaObraRecomendada([FromForm] ObraRecomendadaDTO obraRecomendadaDTO)
        {
            var result = _obraService.AdicionaObraRecomendada(obraRecomendadaDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("recomendada")]
        public IActionResult RetornaListaObraRecomendada()
        {
            var result = _obraService.RetornaListaObraRecomendada();
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("recomendada/{id}")]
        public IActionResult RetornaObraRecomendadaPorId(int id)
        {
            var result =_obraService.RetornaObraRecomendadaPorId(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }        
        
        [HttpPost]
        [Route("recomendada/comentario")]
        public IActionResult AdicionaComentarioObraRecomendada([FromForm] ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var result = _obraService.AdicionaComentarioObraRecomendada(comentarioObraRecomendadaDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpPut]
        [Route("recomendada/comentario")]
        public IActionResult AtualizaComentarioObraRecomendada([FromForm] ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var result = _obraService.AtualizaComentarioObraRecomendada(comentarioObraRecomendadaDTO);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("recomendada/comentario/{id}")]
        public IActionResult RetornaComentarioObraRecomendadaPorId(int id)
        {
            var result = _obraService.RetornaComentarioObraRecomendadaPorId(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
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
