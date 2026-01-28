using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Request;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno.Response;
using TsundokuTraducoes.Helpers.Validacao;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers.Admin
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Forbidden)]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroAppService _generoAppService;
        public GeneroController(IGeneroAppService generoAppService)
        {
            _generoAppService = generoAppService;
        }

        [HttpGet("api/admin/genero")]
        [Authorize(Roles = "admin, staff, moderador")]
        [ProducesResponseType(typeof(ObjetoRetornoGenerosResponse), statusCode: 200)]
        public async Task<IActionResult> RetornaListaGeneros()
        {
            var result = await _generoAppService.RetornaListaGeneros();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetornoGeneros(result.Value);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/admin/genero/{id}")]
        [Authorize(Roles = "admin, staff, moderador")]
        [ProducesResponseType(typeof(RetornoGenero), statusCode: 200)]
        public async Task<IActionResult> RetornaGeneroPorId(Guid id)
        {
            var result = await _generoAppService.RetornaGeneroPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPost("api/admin/genero")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(RetornoGenero), statusCode: 200)]
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(RetornoGenero), statusCode: 200)]
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
        [Authorize(Roles = "admin")]
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