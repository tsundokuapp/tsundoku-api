using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<IActionResult> RetornaListaObras()
        {
            var result = await _obraService.RetornaListaObras();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RetornaObraPorId(int id)
        {
            var result = await _obraService.RetornaObraPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaObra([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraService.AdicionaObra(obraDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"obra/{result.Value.Id}", result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarObra([FromForm] ObraDTO obraDTO)
        {
            var result = await _obraService.AtualizarObra(obraDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrada"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirObra(int id)
        {
            var result = await _obraService.ExcluirObra(id);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrada"))
                    return NotFound(result.Errors[0].Message);

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
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("recomendada")]
        public IActionResult RetornaListaObraRecomendada()
        {
            var result = _obraService.RetornaListaObraRecomendada();
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("recomendada/{id}")]
        public IActionResult RetornaObraRecomendadaPorId(int id)
        {
            var result = _obraService.RetornaObraRecomendadaPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("recomendada/comentario")]
        public IActionResult AdicionaComentarioObraRecomendada([FromForm] ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var result = _obraService.AdicionaComentarioObraRecomendada(comentarioObraRecomendadaDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPut]
        [Route("recomendada/comentario")]
        public IActionResult AtualizaComentarioObraRecomendada([FromForm] ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var result = _obraService.AtualizaComentarioObraRecomendada(comentarioObraRecomendadaDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("recomendada/comentario/{id}")]
        public IActionResult RetornaComentarioObraRecomendadaPorId(int id)
        {
            var result = _obraService.RetornaComentarioObraRecomendadaPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
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
        public async Task<IActionResult> RetornaInformacoesObra(int idObra)
        {
            var result = await _obraService.RetornaInformacaoObraDTO(idObra);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }
    }
}
