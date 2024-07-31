using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Request;
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

        [HttpGet("api/obra/")]
        public async Task<IActionResult> RetornaListaObras([FromQuery] RequestObra requestObra)
        {
            var result = await _obraAppService.RetornaListaObras();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestObra.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestObra.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obra/novels")]
        public async Task<IActionResult> RetornaListaNovels([FromQuery] RequestObra requestObra)
        {
            var result = await _obraAppService.RetornaListaNovels();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestObra.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestObra.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obra/comics")]
        public async Task<IActionResult> RetornaListaComics([FromQuery] RequestObra requestObra)
        {
            var result = await _obraAppService.RetornaListaComics();
            if (result.Value == null || result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestObra.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestObra.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
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
            if (!ValidacaoRequest.ValidaDadosRequestObra(obraDTO))
                return BadRequest("Verifique os campos obrigatórios e tente adicionar a Novel novamente!");

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

            return Created($"api/obra/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/obra/comic")]
        public async Task<IActionResult> AdicionaComic([FromForm] ObraDTO obraDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestObra(obraDTO))
                return BadRequest("Verifique os campos obrigatórios e tente adicionar a Comic novamente!");

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

            return Created($"api/obra/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/obra/novel")]
        public async Task<IActionResult> AtualizarNovel([FromForm] ObraDTO obraDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestObraAtualizacao(obraDTO))
                return BadRequest("Verifique os campos obrigatórios e tente atualizar a Novel novamente!");

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

        [HttpPut("api/obra/comic")]
        public async Task<IActionResult> AtualizarComic([FromForm] ObraDTO obraDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestObraAtualizacao(obraDTO))
                return BadRequest("Verifique os campos obrigatórios e tente atualizar a Comic novamente!");

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


        [HttpDelete("api/obra/novel/{id}/{arquivoLocal}")]
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

        [HttpDelete("api/obra/comic/{id}/{arquivoLocal}")]
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