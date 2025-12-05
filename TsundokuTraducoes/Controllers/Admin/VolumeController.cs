using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Request;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Helpers.Validacao;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers.Admin
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.Forbidden)]
    public class VolumeController : ControllerBase
    {
        private readonly IVolumeAppService _volumeAppService;

        public VolumeController(IVolumeAppService volumeAppService)
        {
            _volumeAppService = volumeAppService;
        }

        [HttpGet("api/admin/volume/")]
        [Authorize(Roles = "admin, staff, moderador")]
        [ProducesResponseType(typeof(List<RetornoVolume>), statusCode: 200)]
        public IActionResult RetornaListaVolume([FromQuery] RequestVolume requestVolume)
        {
            var result = _volumeAppService.RetornaListaVolumes(requestVolume.IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolume(HttpContext, result.Value, requestVolume);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/admin/volume/novel")]
        [Authorize(Roles = "admin, staff, moderador")]
        [ProducesResponseType(typeof(List<RetornoVolume>), statusCode: 200)]
        public IActionResult RetornaListaVolumesNovel([FromQuery] RequestVolume requestVolume)
        {
            // TODO: adicionar validação para casos em que o idObra está ausente.
            var result = _volumeAppService.RetornaListaVolumesNovel(requestVolume.IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolume(HttpContext, result.Value, requestVolume);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/admin/volume/comic")]
        [Authorize(Roles = "admin, staff, moderador")]
        [ProducesResponseType(typeof(List<RetornoVolume>), statusCode: 200)]
        public IActionResult RetornaListaVolumesComic([FromQuery] RequestVolume requestVolume)
        {
            var result = _volumeAppService.RetornaListaVolumesComic(requestVolume.IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolume(HttpContext, result.Value, requestVolume);
            return Ok(objetoRetorno);
        }


        [HttpGet("api/admin/volume/novel/{id}")]
        [Authorize(Roles = "admin, staff, moderador")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public IActionResult RetornaVolumeNovelPorId(Guid id)
        {
            var result = _volumeAppService.RetornaVolumeNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/admin/volume/comic/{id}")]
        [Authorize(Roles = "admin, staff, moderador")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public IActionResult RetornaVolumeComicPorId(Guid id)
        {
            var result = _volumeAppService.RetornaVolumeComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpPost("api/admin/volume/novel/")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public async Task<IActionResult> AdicionaVolumeNovel([FromForm] VolumeDTO volumeDTO)
        {
            if (!ValidacaoRequest.ValidaImagemRequest(volumeDTO.ImagemVolumeFile))
                return BadRequest("Imagem Capa volume inválida!");

            var result = await _volumeAppService.AdicionaVolumeNovel(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/volume/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/admin/volume/comic/")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public async Task<IActionResult> AdicionaVolumeComic([FromForm] VolumeDTO volumeDTO)
        {
            if (!ValidacaoRequest.ValidaImagemCapaVolume(volumeDTO))
                return BadRequest("Capa do volume não informada!");

            if (!ValidacaoRequest.ValidaImagemRequest(volumeDTO.ImagemVolumeFile))
                return BadRequest("Imagem Capa volume inválida!");

            var result = await _volumeAppService.AdicionaVolumeComic(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/volume/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/admin/volume/novel/")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public async Task<IActionResult> AtualizaVolumeNovel([FromForm] VolumeDTO volumeDTO)
        {
            if (volumeDTO.ImagemVolumeFile != null)
                if (!ValidacaoRequest.ValidaImagemRequest(volumeDTO.ImagemVolumeFile))
                    return BadRequest("Imagem Capa volume inválida!");

            var result = await _volumeAppService.AtualizaVolumeNovel(volumeDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Value);
        }

        [HttpPut("api/admin/volume/comic/")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public async Task<IActionResult> AtualizaVolumeComic([FromForm] VolumeDTO volumeDTO)
        {
            if (volumeDTO.ImagemVolumeFile != null)
                if (!ValidacaoRequest.ValidaImagemRequest(volumeDTO.ImagemVolumeFile))
                    return BadRequest("Imagem Capa volume inválida!");

            var result = await _volumeAppService.AtualizaVolumeComic(volumeDTO);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Value);
        }


        [HttpDelete("api/admin/volume/novel/{id}/{arquivoLocal}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ExcluiVolumeNovel(Guid id, bool arquivoLocal)
        {
            var result = await _volumeAppService.ExcluiVolumeNovel(id, arquivoLocal);
            if (result.IsFailed)
            {
                var mensagemErro = result.Errors[0].Message;
                if (mensagemErro.Contains("não encontrado"))
                    return NotFound(mensagemErro);

                return BadRequest(mensagemErro);
            }

            return Ok(result.Successes[0].Message);
        }

        [HttpDelete("api/admin/volume/comic/{id}/{arquivoLocal}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ExcluiVolumeComic(Guid id, bool arquivoLocal)
        {
            var result = await _volumeAppService.ExcluiVolumeComic(id, arquivoLocal);
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
