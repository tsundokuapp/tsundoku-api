using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin.Request;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Api.Controllers
{
    [ApiController]
    public class ObrasController : Controller
    {
        // TODO - Criar as verificações de request, para saber se estar vindo corretamente.
        private readonly IInfosObrasServices _infosObrasServices;
        private readonly IValidacaoTratamentoObrasService _validacaoTratamentoObrasService;

        public ObrasController(IInfosObrasServices infosObrasServices, IValidacaoTratamentoObrasService validacaoObrasService)
        {
            _infosObrasServices = infosObrasServices;
            _validacaoTratamentoObrasService = validacaoObrasService;
        }

        [HttpGet("api/obras/novels")]
        public async Task<IActionResult> ObterNovels([FromQuery] RequestObras requestObras)
        {
            var parametrosValidados = _validacaoTratamentoObrasService.ValidaParametrosNovel(requestObras);

            if (!parametrosValidados)
                return BadRequest("Informe ao menos uma opção para realizar a consulta!");
                        
            var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(requestObras.Skip);            
            var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _infosObrasServices.ObterListaNovels(requestObras);
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/novels/recentes")]
        public async Task<IActionResult> ObterNovelsRecentes([FromQuery] RequestObras requestObras)
        {
            var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _infosObrasServices.ObterListaNovelsRecentes();
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados});
        }

        [HttpGet("api/obras/novel")]
        public async Task<IActionResult> ObterNovelPorId([FromQuery] RequestObras requestObras)
        {
            var capitulo = await _infosObrasServices.ObterNovelPorId(requestObras);
            if (capitulo == null)
                return NotFound();

            return Ok(capitulo);
        }

        
        [HttpGet("api/obras/comics")]
        public async Task<IActionResult> ObterComics([FromQuery] RequestObras requestObras)
        {
            var parametrosValidados = _validacaoTratamentoObrasService.ValidaParametrosNovel(requestObras);

            if (!parametrosValidados)
                return BadRequest("Informe ao menos uma opção para realizar a consulta!");

            var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _infosObrasServices.ObterListaComics(requestObras);
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/comics/recentes")]
        public async Task<IActionResult> ObterComicsRecentes([FromQuery] RequestObras requestObras)
        {
            var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(requestObras.Take);

            var capitulos = await _infosObrasServices.ObterListaComicsRecentes();
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/comic")]
        public async Task<IActionResult> ObterComicPorId([FromQuery] RequestObras requestObras)
        {
            var capitulo = await _infosObrasServices.ObterComicPorId(requestObras);
            if (capitulo == null)
                return NotFound();

            return Ok(capitulo);
        }

        
        [HttpGet("api/obras/home")]
        public async Task<IActionResult> ObterCapitulosHome([FromQuery] RequestObras requestObras)
        {
            var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(requestObras.Skip);
            var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(requestObras.Take, true);

            var capitulos = await _infosObrasServices.ObterCapitulosHome();
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }
    }
}
