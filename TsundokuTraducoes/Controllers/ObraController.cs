using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Request;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Helpers.Validacao;
using TsundokuTraducoes.Services.AppServices.Interfaces;

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

        [HttpGet("api/admin/obra/")]
        [ProducesResponseType(typeof(List<RetornoObra>), statusCode: 200)]
        public async Task<IActionResult> RetornaListaObras([FromQuery] RequestObra requestObra)
        {
            var result = await _obraAppService.RetornaListaObras();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriaObjetoRetono(HttpContext, result.Value, requestObra.Skip, requestObra.Take);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/admin/obra/novels")]
        [ProducesResponseType(typeof(List<RetornoObra>), statusCode: 200)]
        public async Task<IActionResult> RetornaListaNovels([FromQuery] RequestObra requestObra)
        {
            var result = await _obraAppService.RetornaListaNovels();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriaObjetoRetono(HttpContext, result.Value, requestObra.Skip, requestObra.Take);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/admin/obra/comics")]
        [ProducesResponseType(typeof(List<RetornoObra>), statusCode: 200)]
        public async Task<IActionResult> RetornaListaComics([FromQuery] RequestObra requestObra)
        {
            var result = await _obraAppService.RetornaListaComics();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriaObjetoRetono(HttpContext, result.Value, requestObra.Skip, requestObra.Take);
            return Ok(objetoRetorno);
        }


        [HttpGet("api/admin/obra/novel/id/{id}")]
        [ProducesResponseType(typeof(RetornoObra), statusCode: 200)]
        public async Task<IActionResult> RetornaNovelPorId(Guid id)
        {
            var result = await _obraAppService.RetornaNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/admin/obra/comic/id/{id}")]
        [ProducesResponseType(typeof(RetornoObra), statusCode: 200)]
        public async Task<IActionResult> RetornaComicPorId(Guid id)
        {
            var result = await _obraAppService.RetornaComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/admin/obra/novel/slug/{slug}")]
        [ProducesResponseType(typeof(RetornoObra), statusCode: 200)]
        public async Task<IActionResult> RetornaNovelPorSlug(string slug)
        {
            var result = await _obraAppService.RetornaNovelPorSlug(slug);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/admin/obra/comic/slug/{slug}")]
        [ProducesResponseType(typeof(RetornoObra), statusCode: 200)]
        public async Task<IActionResult> RetornaComicPorSlug(string slug)
        {
            var result = await _obraAppService.RetornaComicPorSlug(slug);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPost("api/admin/obra/novel")]
        [ProducesResponseType(typeof(RetornoObra), statusCode: 200)]
        public async Task<IActionResult> AdicionaNovel([FromForm] ObraDTO obraDTO)
        {
            if (!ValidacaoRequest.ValidaImagemRequest(obraDTO.ImagemCapaPrincipalFile))
                return BadRequest("Imagem Capa principal inválida!");

            if (obraDTO.ImagemBannerFile != null)
                if (!ValidacaoRequest.ValidaImagemRequest(obraDTO.ImagemBannerFile))
                    return BadRequest("Imagem banner inválida!");

            if (!ValidacaoRequest.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return BadRequest("Erro ao adicionar a Novel, código hexadecimal informada fora do padrão!");

            var result = await _obraAppService.AdicionaNovel(obraDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/obra/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/admin/obra/comic")]
        [ProducesResponseType(typeof(RetornoObra), statusCode: 200)]
        public async Task<IActionResult> AdicionaComic([FromForm] ObraDTO obraDTO)
        {
            if (!ValidacaoRequest.ValidaImagemRequest(obraDTO.ImagemCapaPrincipalFile))
                return BadRequest("Imagem Capa principal inválida!");

            if (obraDTO.ImagemBannerFile != null)
                if (!ValidacaoRequest.ValidaImagemRequest(obraDTO.ImagemBannerFile))
                    return BadRequest("Imagem banner inválida!");

            if (!ValidacaoRequest.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return BadRequest("Erro ao adicionar a Comic, código hexadecimal informada fora do padrão!");

            var result = await _obraAppService.AdicionaComic(obraDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/obra/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/admin/obra/novel")]
        [ProducesResponseType(typeof(RetornoObra), statusCode: 200)]
        public async Task<IActionResult> AtualizarNovel([FromForm] ObraDTO obraDTO)
        {
            if (obraDTO.ImagemBannerFile != null)
                if (!ValidacaoRequest.ValidaImagemRequest(obraDTO.ImagemCapaPrincipalFile))
                    return BadRequest("Imagem Capa principal inválida!");

            if (obraDTO.ImagemBannerFile != null)
                if (!ValidacaoRequest.ValidaImagemRequest(obraDTO.ImagemBannerFile))
                    return BadRequest("Imagem banner inválida!");

            if (!ValidacaoRequest.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return BadRequest("Erro ao atualizar a Novel, código hexadecimal informada fora do padrão!");

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

        [HttpPut("api/admin/obra/comic")]
        [ProducesResponseType(typeof(RetornoObra), statusCode: 200)]
        public async Task<IActionResult> AtualizarComic([FromForm] ObraDTO obraDTO)
        {
            if (!ValidacaoRequest.ValidaImagemRequest(obraDTO.ImagemCapaPrincipalFile))
                return BadRequest("Imagem Capa principal inválida!");

            if (obraDTO.ImagemBannerFile != null)
                if (!ValidacaoRequest.ValidaImagemRequest(obraDTO.ImagemBannerFile))
                    return BadRequest("Imagem banner inválida!");

            if (!ValidacaoRequest.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return BadRequest("Erro ao atualizar a Comic, código hexadecimal informada fora do padrão!");

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


        [HttpDelete("api/admin/obra/novel/{id}/{arquivoLocal}")]
        public async Task<IActionResult> ExcluirNovel(Guid id, bool arquivoLocal)
        {
            var result = await _obraAppService.ExcluiNovel(id, arquivoLocal);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrada"))
                    return NotFound(result.Errors[0].Message);

                return BadRequest(result.Errors[0].Message);
            }

            return Ok(result.Successes[0].Message);
        }

        [HttpDelete("api/admin/obra/comic/{id}/{arquivoLocal}")]
        public async Task<IActionResult> ExcluirComic(Guid id, bool arquivoLocal)
        {
            var result = await _obraAppService.ExcluiComic(id, arquivoLocal);
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