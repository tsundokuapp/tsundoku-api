using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Request;
using TsundokuTraducoes.Helpers.Validacao;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers
{
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroAppService _generoAppService;
        public GeneroController(IGeneroAppService generoAppService)
        {
            _generoAppService = generoAppService;
        }

        [HttpGet("api/admin/genero")]
        public async Task<IActionResult> RetornaListaGeneros([FromQuery] RequestGenero requestGenero)
        {
            var result = await _generoAppService.RetornaListaGeneros();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestGenero.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestGenero.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/admin/genero/{id}")]
        public async Task<IActionResult> RetornaGeneroPorId(Guid id)
        {
            var result = await _generoAppService.RetornaGeneroPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPost("api/admin/genero")]
        public async Task<IActionResult> AdicionaGenero([FromForm] GeneroDTO generoDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestGenero(generoDTO))
                return BadRequest("Verifique os campos obrigatórios e tente adicionar o Gênero novamente!");

            var result = await _generoAppService.AdicionaGenero(generoDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/genero/{result.Value.Id}", result.Value);
        }

        [HttpPut("api/admin/genero")]
        public async Task<IActionResult> AtualizarGenero([FromForm] GeneroDTO generoDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestGenero(generoDTO))
                return BadRequest("Verifique os campos obrigatórios e tente atualizar o Genero novamente!");
            
            var result = await _generoAppService.AtualizaGenero(generoDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Value);
        }

        [HttpDelete("api/admin/genero/{id}")]
        public async Task<IActionResult> ExcluirGenero(Guid id)
        {
            var result = await _generoAppService.ExcluiGenero(id);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }
    }
}