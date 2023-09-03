using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> ObterNovels([FromQuery] string pesquisar, string nacionalidade, string status, string tipo, string genero, int? skip, int? take)
        {
            var parametrosValidados = _validacaoTratamentoObrasService.ValidaParametrosNovel(pesquisar, nacionalidade, status, tipo, genero, skip, take);

            if (!parametrosValidados)
                return BadRequest("Informe ao menos uma opção para realizar a consulta!");
                        
            var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(skip);            
            var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(take);

            var capitulos = await _infosObrasServices.ObterListaNovels(pesquisar, nacionalidade, status, tipo, genero);
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados });
        }

        [HttpGet("api/obras/novels/recentes")]
        public async Task<IActionResult> ObterNovelsRecentes([FromQuery] int? skip, int? take)
        {
            var skipTratado = _validacaoTratamentoObrasService.RetornaSkipTratado(skip);
            var takeTratado = _validacaoTratamentoObrasService.RetornaTakeTratado(take);

            var capitulos = await _infosObrasServices.ObterListaNovelsRecentes();
            if (capitulos.Count == 0)
                return NoContent();

            var dados = capitulos.Skip(skipTratado).Take(takeTratado).ToList();
            var total = capitulos.Count;

            return Ok(new { total = total, data = dados});
        }




        // TODO - Será verificado se vai ser reaproveitado enquanto é trabalhado nos backlogs
        // Foi adicionado as rotas direto nos atributos HttpGet para que servidor possa subir

        [HttpGet("api/obras/")]
        public IActionResult ObterCapitulos()
        {
            var capitulos = _infosObrasServices.ObterCapitulos();
            return Ok(capitulos);
        }
                
        [HttpGet("{slug}")]
        public IActionResult ObterObra(string slug)
        {
            var obra = _infosObrasServices.ObterObraPorSlug(slug);
            return Ok(obra);
        }

        [HttpGet("comics")]
        public IActionResult ObterComics([FromQuery] string pesquisar, string nacionalidade, string status, string tipo, string genero, int? pagina, int? capitulosPorPagina)
        {
            pagina = pagina == null ? 0 : pagina;
            capitulosPorPagina = capitulosPorPagina == null ? 4 : capitulosPorPagina;
            
            var capitulos = _infosObrasServices.ObterCapitulos(pesquisar, nacionalidade, status, tipo, genero, false);
            return Ok(capitulos.Skip(pagina.GetValueOrDefault()).Take(capitulosPorPagina.GetValueOrDefault()).ToList());
        }

        [HttpGet("novels/capitulo/{slugCapitulo}")]
        public IActionResult ObterCapituloNovel(string slugCapitulo)
        {
            var capitulo = _infosObrasServices.ObterCapituloNovelPorSlug(slugCapitulo);
            return Ok(capitulo);
        }

        [HttpGet("comics/capitulo/{slugCapitulo}")]
        public IActionResult ObterCapituloComic(string slugCapitulo)
        {
            var capitulo = _infosObrasServices.ObterCapituloComicPorSlug(slugCapitulo);
            return Ok(capitulo);
        }
    }
}
