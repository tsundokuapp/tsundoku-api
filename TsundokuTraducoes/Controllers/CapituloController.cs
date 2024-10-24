using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Request;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Helpers.Validacao;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Controllers
{
    [ApiController]
    public class CapituloController : ControllerBase
    {        
        private readonly ICapituloAppService _capituloService;

        public CapituloController(ICapituloAppService capituloService)
        {
            _capituloService = capituloService;
        }

        [HttpGet("api/admin/capitulo/")]
        [ProducesResponseType(typeof(List<RetornoCapitulo>), statusCode: 200)]
        public IActionResult RetornaListaCapitulos([FromQuery] RequestCapitulo requestCapitulo)
        {
            var result = _capituloService.RetornaListaCapitulos(requestCapitulo.volumeId);
            if (result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestCapitulo.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestCapitulo.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/admin/capitulo/novel")]
        [ProducesResponseType(typeof(List<RetornoCapitulo>), statusCode: 200)]
        public IActionResult RetornaListaCapitulosNovel([FromQuery] RequestCapitulo requestCapitulo)
        {
            var result = _capituloService.RetornaListaCapitulosNovel(requestCapitulo.volumeId);
            if (result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestCapitulo.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestCapitulo.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/admin/capitulo/comic")]
        [ProducesResponseType(typeof(List<RetornoCapitulo>), statusCode: 200)]
        public IActionResult RetornaListaCapitulosComic([FromQuery] RequestCapitulo requestCapitulo)
        {
            var result = _capituloService.RetornaListaCapitulosComic(requestCapitulo.volumeId);
            if (result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestCapitulo.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestCapitulo.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }


        [HttpGet("api/admin/capitulo/novel/{id}")]
        [ProducesResponseType(typeof(RetornoCapitulo), statusCode: 200)]
        public IActionResult RetornaCapituloNovelPorId(Guid id)
        {
            var result = _capituloService.RetornaCapituloNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/admin/capitulo/comic/{id}")]
        [ProducesResponseType(typeof(RetornoCapitulo), statusCode: 200)]
        public IActionResult RetornaCapituloComicPorId(Guid id)
        {
            var result = _capituloService.RetornaCapituloComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }


        [HttpPost("api/admin/capitulo/novel/")]
        [ProducesResponseType(typeof(RetornoCapitulo), statusCode: 200)]
        public async Task<IActionResult> AdicionaCapituloNovel([FromForm] CapituloDTO capituloDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestCapituloNovel(capituloDTO))
                return BadRequest("Verifique os campos obrigatórios e tente adicionar o capitulo novamente!");

            var listaImagemEnviada = capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0;
            if (listaImagemEnviada)
                if (!ValidacaoRequest.ValidaListaImagemRequest(capituloDTO.ListaImagensForm))
                    return BadRequest("Alguma imagem da lista de imagens é invalida!");

            var result = await _capituloService.AdicionaCapituloNovel(capituloDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/capitulo/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/admin/capitulo/comic/")]
        [ProducesResponseType(typeof(RetornoCapitulo), statusCode: 200)]
        public async Task<IActionResult> AdicionaCapituloComic([FromForm] CapituloDTO capituloDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestCapituloComic(capituloDTO))
                return BadRequest("Verifique os campos obrigatórios e tente adicionar o capitulo novamente!");

            if (!ValidacaoRequest.ValidaListaImagemRequest(capituloDTO.ListaImagensForm))
                return BadRequest("Alguma imagem da lista de imagens é invalida!");

            var result = await _capituloService.AdicionaCapituloComic(capituloDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/capitulo/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/admin/capitulo/novel/")]
        [ProducesResponseType(typeof(RetornoCapitulo), statusCode: 200)]
        public async Task<IActionResult> AtualizaCapituloNovel([FromForm] CapituloDTO capituloDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestCapituloAtualizacao(capituloDTO))
                return BadRequest("Verifique os campos obrigatórios e tente atualizar o capitulo novamente!");

            var listaImagemEnviada = capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0;
            if (listaImagemEnviada)
                if (!ValidacaoRequest.ValidaListaImagemRequest(capituloDTO.ListaImagensForm))
                    return BadRequest("Alguma imagem da lista de imagens é invalida!");

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

        [HttpPut("api/admin/capitulo/comic/")]
        [ProducesResponseType(typeof(RetornoCapitulo), statusCode: 200)]
        public async Task<IActionResult> AtualizaCapituloComic([FromForm] CapituloDTO capituloDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestCapituloAtualizacao(capituloDTO))
                return BadRequest("Verifique os campos obrigatórios e tente atualizar o capitulo novamente!");

            if (!ValidacaoRequest.ValidaListaImagemRequest(capituloDTO.ListaImagensForm))
                return BadRequest("Alguma imagem da lista de imagens é invalida!");

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


        [HttpDelete("api/admin/capitulo/novel/{id}/{arquivoLocal}")]
        public async Task<IActionResult> ExcluiCapituloNovel(Guid id, bool arquivoLocal)
        {
            var result = await _capituloService.ExcluiCapituloNovel(id, arquivoLocal);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Successes[0].Message);
        }

        [HttpDelete("api/admin/capitulo/comic/{id}/{arquivoLocal}")]
        public async Task<IActionResult> ExcluiCapituloComic(Guid id, bool arquivoLocal)
        {
            var result = await _capituloService.ExcluiCapituloComic(id, arquivoLocal);
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