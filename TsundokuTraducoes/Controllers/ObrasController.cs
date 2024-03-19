using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
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
        public async Task<IActionResult> ObterNovels([FromQuery] RequestObras requestObras)
        {
            var parametrosValidados = ValidacaoRequest.ValidaParametrosNovel(requestObras);

            if (!parametrosValidados)
                return BadRequest("Informe ao menos uma opção para realizar a consulta!");

            var skipTratado = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _obrasAppServices.ObterListaNovels(requestObras);
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

            var capitulos = await _obrasAppServices.ObterListaNovelsRecentes();
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/novel")]
        public async Task<IActionResult> ObterNovelPorId([FromQuery] RequestObras requestObras)
        {
            var capitulo = await _obrasAppServices.ObterNovelPorId(requestObras);
            if (capitulo == null)
                return NotFound("Novel não encontra!");

            return Ok(capitulo);
        }


        [HttpGet("api/obras/comics")]
        public async Task<IActionResult> ObterComics([FromQuery] RequestObras requestObras)
        {
            var parametrosValidados = ValidacaoRequest.ValidaParametrosNovel(requestObras);

            if (!parametrosValidados)
                return BadRequest("Informe ao menos uma opção para realizar a consulta!");

            var skipTratado = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _obrasAppServices.ObterListaComics(requestObras);
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/comics/recentes")]
        public async Task<IActionResult> ObterComicsRecentes([FromQuery] RequestObras requestObras)
        {
            var skipTratado = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _obrasAppServices.ObterListaComicsRecentes();
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/comic")]
        public async Task<IActionResult> ObterComicPorId([FromQuery] RequestObras requestObras)
        {
            var capitulo = await _obrasAppServices.ObterComicPorId(requestObras);
            if (capitulo == null)
                return NotFound();

            return Ok(capitulo);
        }


        [HttpGet("api/obras/home")]
        public async Task<IActionResult> ObterCapitulosHome([FromQuery] RequestObras requestObras)
        {
            var skipTratado = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratado(requestObras.Take, true);

            var capitulos = await _obrasAppServices.ObterCapitulosHome();
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/recomendadas")]
        public async Task<IActionResult> ObterObrasRecomendadas([FromQuery] RequestObras requestObras)
        {
            var skipTratado = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = ValidacaoRequest.RetornaTakeTratado(requestObras.Take, false);

            var obrasRecomendadas = await _obrasAppServices.ObterObrasRecomendadas();
            if (obrasRecomendadas.Count == 0)
                return NoContent();

            var dados = obrasRecomendadas.Skip(skipTratado).Take(takeTratado).ToList();
            var total = obrasRecomendadas.Count;

            return Ok(new { total = total, data = dados });
        }
    }
}
