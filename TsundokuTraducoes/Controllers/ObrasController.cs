using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
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
        [ProducesResponseType(typeof(List<RetornoObras>), statusCode: 200)]
        public async Task<IActionResult> ObterNovels([FromQuery] RequestObras requestObras)
        {
            var capitulos = await _obrasAppServices.ObterListaNovels(requestObras);
            if (capitulos.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoObras(HttpContext, capitulos, requestObras);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/novels/recentes")]
        [ProducesResponseType(typeof(List<RetornoObras>), statusCode: 200)]
        public async Task<IActionResult> ObterNovelsRecentes([FromQuery] RequestObras requestObras)
        {
            var capitulos = await _obrasAppServices.ObterListaNovelsRecentes();
            if (capitulos.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoObras(HttpContext, capitulos, requestObras);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/novel/id/{id}")]
        [ProducesResponseType(typeof(RetornoNovel), statusCode: 200)]
        public async Task<IActionResult> ObterNovelPorId(Guid id)
        {
            var novel = await _obrasAppServices.ObterNovelPorId(id);
            if (novel == null)
                return NotFound("Novel não encontra!");

            return Ok(novel);
        }
        
        [HttpGet("api/obras/novel/slug/{slug}")]
        [ProducesResponseType(typeof(RetornoAppNovel), statusCode: 200)]
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
        [ProducesResponseType(typeof(List<RetornoObras>), statusCode: 200)]
        public async Task<IActionResult> ObterComicsRecentes([FromQuery] RequestObras requestObras)
        {
            var capitulos = await _obrasAppServices.ObterListaComicsRecentes();
            if (capitulos.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoObras(HttpContext, capitulos, requestObras);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/comic/id/{id}")]
        [ProducesResponseType(typeof(RetornoObras), statusCode: 200)]
        public async Task<IActionResult> ObterComicPorId(Guid id)
        {
            var capitulo = await _obrasAppServices.ObterComicPorId(id);
            if (capitulo == null)
                return NotFound();

            return Ok(capitulo);
        }
        
        [HttpGet("api/obras/comic/slug/{slug}")]
        [ProducesResponseType(typeof(RetornoObras), statusCode: 200)]
        public async Task<IActionResult> ObterComicPorId(string slug)
        {
            var capitulo = await _obrasAppServices.ObterComicPorSlug(slug);
            if (capitulo == null)
                return NotFound();

            return Ok(capitulo);
        }
        
        [HttpGet("api/obras/home")]
        [ProducesResponseType(typeof(List<RetornoCapitulos>), statusCode: 200)]
        public async Task<IActionResult> ObterCapitulosHome([FromQuery] RequestObras requestObras)
        {
            var capitulos = await _obrasAppServices.ObterCapitulosHome();
            if (capitulos.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoObras(HttpContext, capitulos, requestObras, true);
            return Ok(objetoRetorno);
        }

        [HttpGet("api/obras/recomendadas")]
        [ProducesResponseType(typeof(List<RetornoObrasRecomendadas>), statusCode: 200)]
        public async Task<IActionResult> ObterObrasRecomendadas([FromQuery] RequestObras requestObras)
        {
            var obrasRecomendadas = await _obrasAppServices.ObterObrasRecomendadas();
            if (obrasRecomendadas.Count == 0)
                return NoContent();

            var objetoRetorno = RequestHelper.CriarObjetoRetonoObras(HttpContext, obrasRecomendadas, requestObras);
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