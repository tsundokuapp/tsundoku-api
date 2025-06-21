using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response;
using TsundokuTraducoes.Helpers.Validacao;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Controllers
{
    [ApiController]
    public class ObrasController : Controller
    {   
        private readonly IObrasAppService _obrasAppServices;

        public ObrasController(IObrasAppService obrasAppServices)
        {
            _obrasAppServices = obrasAppServices;
        }

        [HttpGet("api/obras/novels")]
        [ProducesResponseType(typeof(List<RetornoNovel>), statusCode: 200)]
        public async Task<IActionResult> ObterNovels([FromQuery] RequestObras requestObras)
        {
            var capitulos = await _obrasAppServices.ObterListaNovels(requestObras);
            if (capitulos.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoObras(HttpContext, capitulos, requestObras);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/novels/recentes")]
        [ProducesResponseType(typeof(ObjetoRetornoNovelsRecentes), statusCode: 200)]
        public async Task<IActionResult> ObterNovelsRecentes([FromQuery] int? skip, int? take)
        {
            var novels = await _obrasAppServices.ObterListaNovelsRecentes();
            if (novels.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoNovelsRecentes(HttpContext, novels, skip, take);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/novel/id/{id}")]
        [ProducesResponseType(typeof(RetornoNovel), statusCode: 200)]
        public async Task<IActionResult> ObterNovelPorId(Guid id)
        {
            var retorno = await _obrasAppServices.ObterNovelPorId(id);
            if (retorno.IsFailed)
                return NotFound(retorno.Errors[0].Message);

            return Ok(retorno.Value);
        }
        
        [HttpGet("api/obras/novel/slug/{slug}")]
        [ProducesResponseType(typeof(RetornoNovel), statusCode: 200)]
        public async Task<IActionResult> ObterNovelPorSlug(string slug)
        {
            var retorno = await _obrasAppServices.ObterNovelPorSlug(slug);
            if (retorno.IsFailed)
                return NotFound(retorno.Errors[0].Message);

            return Ok(retorno.Value);
        }

        [HttpGet("api/obras/comics")]
        [ProducesResponseType(typeof(List<RetornoObras>), statusCode: 200)]
        public async Task<IActionResult> ObterComics([FromQuery] RequestObras requestObras)
        {
            var capitulos = await _obrasAppServices.ObterListaComics(requestObras);
            if (capitulos.Count == 0)
                return NoContent();
                
            var objetoRetorno = RequestHelper.CriarObjetoRetonoObras(HttpContext, capitulos, requestObras);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/comics/recentes")]
        [ProducesResponseType(typeof(ObjetoRetornoComicsRecentes), statusCode: 200)]
        public async Task<IActionResult> ObterComicsRecentes([FromQuery] int? skip, int? take)
        {
            var comics = await _obrasAppServices.ObterListaComicsRecentes();
            if (comics.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoComicsRecentes(HttpContext, comics, skip, take);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/comic/id/{id}")]
        [ProducesResponseType(typeof(RetornoComic), statusCode: 200)]
        public async Task<IActionResult> ObterComicPorId(Guid id)
        {
            var retorno = await _obrasAppServices.ObterComicPorId(id);
            if (retorno.IsFailed)
                return NotFound(retorno.Errors[0].Message);

            return Ok(retorno.Value);
        }
        
        [HttpGet("api/obras/comic/slug/{slug}")]
        [ProducesResponseType(typeof(RetornoComic), statusCode: 200)]
        public async Task<IActionResult> ObterComicPorId(string slug)
        {
            var retorno = await _obrasAppServices.ObterComicPorSlug(slug);
            if (retorno.IsFailed)
                return NotFound(retorno.Errors[0].Message);

            return Ok(retorno.Value);
        }
        
        [HttpGet("api/obras/home")]
        [ProducesResponseType(typeof(ObjetoCapitulosHome), statusCode: 200)]
        public async Task<IActionResult> ObterCapitulosHome()
        {
            var capitulos = await _obrasAppServices.ObterCapitulosHome();
            if (capitulos.Count == 0)
                return NoContent();

            return Ok(new ObjetoCapitulosHome { Data = capitulos, Total = capitulos.Count});
        }

        [HttpGet("api/obras/recomendadas")]
        [ProducesResponseType(typeof(List<RetornoObrasRecomendadas>), statusCode: 200)]
        public async Task<IActionResult> ObterObrasRecomendadas()
        {
            var obrasRecomendadas = await _obrasAppServices.ObterObrasRecomendadas();
            if (obrasRecomendadas.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoObrasRecomendadas(HttpContext, obrasRecomendadas);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/volume/indice")]
        [ProducesResponseType(typeof(List<RetornoVolumes>), statusCode:200)]
        public async Task<IActionResult> ObterListaVolumeCapitulos([FromQuery] RequestObras requestObras)
        {
            if (!ValidacaoRequest.ValidaListaVolumeCapitulo(requestObras))
                return BadRequest("Não informado o código da obra, verificar com os admins do site!");

            var volumes = await Task.Run(() => _obrasAppServices.ObterListaVolumeCapitulos(requestObras));
            if (volumes.Count == 0) 
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoObras(HttpContext, volumes, requestObras);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/comic/capitulo/{id}")]
        [ProducesResponseType(typeof(RetornoCapituloComic), statusCode:200)]
        public async Task<IActionResult> ObterCapituloComicPorId(Guid id)
        {
            var capituloComic = await _obrasAppServices.ObterCapituloComicPorId(id);
            if (capituloComic == null)
                return BadRequest("Capitulo não encontrado!");            
            
            return Ok(capituloComic);
        }

        [HttpGet("api/obras/novel/capitulo/{id}")]
        [ProducesResponseType(typeof(RetornoCapituloNovel), statusCode: 200)]
        public async Task<IActionResult> ObterCapituloNovelPorId(Guid id)
        {
            var capituloNovel = await _obrasAppServices.ObterCapituloNovelPorId(id);
            if (capituloNovel == null)
                return BadRequest("Capitulo não encontrado!");

            return Ok(capituloNovel);
        }
    }
}