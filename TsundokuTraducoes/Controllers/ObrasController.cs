﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.Validacao;

namespace TsundokuTraducoes.Api.Controllers
{
    [ApiController]
    public class ObrasController : Controller
    {   
        private readonly IObrasService _obrasServices;

        public ObrasController(IObrasService obrasServices)
        {
            _obrasServices = obrasServices;
        }

        [HttpGet("api/obras/novels")]
        public async Task<IActionResult> ObterNovels([FromQuery] RequestObras requestObras)
        {
            var parametrosValidados = ValidacaoRequest.ValidaParametrosNovel(requestObras);

            if (!parametrosValidados)
                return BadRequest("Informe ao menos uma opção para realizar a consulta!");

            var skipTratado = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _obrasServices.ObterListaNovels(requestObras);
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/novels/recentes")]
        public async Task<IActionResult> ObterNovelsRecentes([FromQuery] RequestObras requestObras)
        {
            var skipTratado = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _obrasServices.ObterListaNovelsRecentes();
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/novel")]
        public async Task<IActionResult> ObterNovelPorId([FromQuery] RequestObras requestObras)
        {
            var capitulo = await _obrasServices.ObterNovelPorId(requestObras);
            if (capitulo == null)
                return NotFound("Novel não encontra!");

            return Ok(capitulo);
        }


        //[HttpGet("api/obras/comics")]
        //public async Task<IActionResult> ObterComics([FromQuery] RequestObras requestObras)
        //{
        //    var parametrosValidados = _validacaoTratamentoObrasService.ValidaParametrosNovel(requestObras);

        //    if (!parametrosValidados)
        //        return BadRequest("Informe ao menos uma opção para realizar a consulta!");

        //    var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(requestObras.Skip);
        //    var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(requestObras.Take);

        //    var capitulos = await _obrasServices.ObterListaComics(requestObras);
        //    if (capitulos.Count == 0)
        //        return NoContent();

        //    var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
        //    var total = capitulos.Count;

        //    return Ok(new { total = total, data = dados });
        //}

        //[HttpGet("api/obras/comics/recentes")]
        //public async Task<IActionResult> ObterComicsRecentes([FromQuery] RequestObras requestObras)
        //{
        //    var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(requestObras.Skip);
        //    var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(requestObras.Take);

        //    var capitulos = await _obrasServices.ObterListaComicsRecentes();
        //    if (capitulos.Count == 0)
        //        return NoContent();

        //    var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
        //    var total = capitulos.Count;

        //    return Ok(new { total = total, data = dados });
        //}

        //[HttpGet("api/obras/comic")]
        //public async Task<IActionResult> ObterComicPorId([FromQuery] RequestObras requestObras)
        //{
        //    var capitulo = await _obrasServices.ObterComicPorId(requestObras);
        //    if (capitulo == null)
        //        return NotFound();

        //    return Ok(capitulo);
        //}


        //[HttpGet("api/obras/home")]
        //public async Task<IActionResult> ObterCapitulosHome([FromQuery] RequestObras requestObras)
        //{
        //    var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(requestObras.Skip);
        //    var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(requestObras.Take, true);

        //    var capitulos = await _obrasServices.ObterCapitulosHome();
        //    if (capitulos.Count == 0)
        //        return NoContent();

        //    var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
        //    var total = capitulos.Count;

        //    return Ok(new { total = total, data = dados });
        //}
    }
}
