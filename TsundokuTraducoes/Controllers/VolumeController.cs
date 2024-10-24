﻿using Microsoft.AspNetCore.Mvc;
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
    public class VolumeController : ControllerBase
    {
        private readonly IVolumeAppService _volumeAppService;

        public VolumeController(IVolumeAppService volumeAppService)
        {
            _volumeAppService = volumeAppService;
        }

        [HttpGet("api/admin/volume/")]
        [ProducesResponseType(typeof(List<RetornoVolume>), statusCode: 200)]
        public IActionResult RetornaListaVolume([FromQuery] RequestVolume requestVolume)
        {
            var result = _volumeAppService.RetornaListaVolumes(requestVolume.IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestVolume.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestVolume.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/admin/volume/novel")]
        [ProducesResponseType(typeof(List<RetornoVolume>), statusCode: 200)]
        public IActionResult RetornaListaVolumesNovel([FromQuery] RequestVolume requestVolume)
        {
            var result = _volumeAppService.RetornaListaVolumesNovel(requestVolume.IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestVolume.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestVolume.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/admin/volume/comic")]
        [ProducesResponseType(typeof(List<RetornoVolume>), statusCode: 200)]
        public IActionResult RetornaListaVolumesComic([FromQuery] RequestVolume requestVolume)
        {
            var result = _volumeAppService.RetornaListaVolumesComic(requestVolume.IdObra);
            if (result.Value.Count == 0)
                return NoContent();

            var skipTratado = ValidacaoRequest.RetornaSkipTratadoAdmin(requestVolume.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratadoAdmin(requestVolume.Take);

            var dados = result.Value.Skip(skipTratado).Take(takeTratado).ToList();
            var total = result.Value.Count;

            return Ok(new { total = total, data = dados });
        }


        [HttpGet("api/admin/volume/novel/{id}")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public IActionResult RetornaVolumeNovelPorId(Guid id)
        {
            var result = _volumeAppService.RetornaVolumeNovelPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }

        [HttpGet("api/admin/volume/comic/{id}")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public IActionResult RetornaVolumeComicPorId(Guid id)
        {
            var result = _volumeAppService.RetornaVolumeComicPorId(id);
            if (result.IsFailed)
                return NotFound(result.Errors[0].Message);

            return Ok(result.Value);
        }


        [HttpPost("api/admin/volume/novel/")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public async Task<IActionResult> AdicionaVolumeNovel([FromForm] VolumeDTO volumeDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestVolume(volumeDTO))
                return BadRequest("Verifique os campos obrigatórios e tente adicionar o volume da novel novamente!");

            if (!ValidacaoRequest.ValidaImagemRequest(volumeDTO.ImagemVolumeFile))
                return BadRequest("Imagem Capa volume inválida!");

            var result = await _volumeAppService.AdicionaVolumeNovel(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/volume/novel/{result.Value.Id}", result.Value);
        }

        [HttpPost("api/admin/volume/comic/")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public async Task<IActionResult> AdicionaVolumeComic([FromForm] VolumeDTO volumeDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestVolume(volumeDTO))
                return BadRequest("Verifique os campos obrigatórios e tente adicionar o volume da comic novamente!");

            if (!ValidacaoRequest.ValidaImagemRequest(volumeDTO.ImagemVolumeFile))
                return BadRequest("Imagem Capa volume inválida!");

            var result = await _volumeAppService.AdicionaVolumeComic(volumeDTO);
            if (result.IsFailed)
                return BadRequest(result.Errors[0].Message);

            return Created($"api/admin/volume/comic/{result.Value.Id}", result.Value);
        }


        [HttpPut("api/admin/volume/novel/")]
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public async Task<IActionResult> AtualizaVolumeNovel([FromForm] VolumeDTO volumeDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestVolumeAtualizacao(volumeDTO))
                return BadRequest("Verifique os campos obrigatórios e tente atualizar o volume da novel novamente!");

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
        [ProducesResponseType(typeof(RetornoVolume), statusCode: 200)]
        public async Task<IActionResult> AtualizaVolumeComic([FromForm] VolumeDTO volumeDTO)
        {
            if (!ValidacaoRequest.ValidaDadosRequestVolumeAtualizacao(volumeDTO))
                return BadRequest("Verifique os campos obrigatórios e tente atualizar o volume da comic novamente!");

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
