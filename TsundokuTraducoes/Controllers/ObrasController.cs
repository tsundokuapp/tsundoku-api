using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObrasController : Controller
    {
        // TODO - Criar as verificações de request, para saber se estar vindo corretamente.
        private readonly IInfosObrasServices _infosObrasServices;

        public ObrasController(IInfosObrasServices infosObrasServices)
        {
            _infosObrasServices = infosObrasServices;
        }

        [HttpGet]        
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
        
        [HttpGet]
        [Route("novels")]
        public IActionResult ObterNovels([FromQuery] string pesquisar, string nacionalidade, string status, string tipo, string genero, int? pagina, int? capitulosPorPagina)
        {  
            pagina = pagina == null ? 0 : pagina;
            capitulosPorPagina = capitulosPorPagina == null ? 4 : capitulosPorPagina;

            var capitulos = _infosObrasServices.ObterCapitulos(pesquisar, nacionalidade, status, tipo, genero, true);
            return Ok(capitulos.Skip(pagina.GetValueOrDefault()).Take(capitulosPorPagina.GetValueOrDefault()).ToList());
        }

        [HttpGet]
        [Route("comics")]
        public IActionResult ObterComics([FromQuery] string pesquisar, string nacionalidade, string status, string tipo, string genero, int? pagina, int? capitulosPorPagina)
        {
            pagina = pagina == null ? 0 : pagina;
            capitulosPorPagina = capitulosPorPagina == null ? 4 : capitulosPorPagina;
            
            var capitulos = _infosObrasServices.ObterCapitulos(pesquisar, nacionalidade, status, tipo, genero, false);
            return Ok(capitulos.Skip(pagina.GetValueOrDefault()).Take(capitulosPorPagina.GetValueOrDefault()).ToList());
        }

        [HttpGet]
        [Route("novels/capitulo/{slugCapitulo}")]
        public IActionResult ObterCapituloNovel(string slugCapitulo)
        {
            var capitulo = _infosObrasServices.ObterCapituloNovelPorSlug(slugCapitulo);
            return Ok(capitulo);
        }

        [HttpGet]
        [Route("comics/capitulo/{slugCapitulo}")]
        public IActionResult ObterCapituloComic(string slugCapitulo)
        {
            var capitulo = _infosObrasServices.ObterCapituloComicPorSlug(slugCapitulo);
            return Ok(capitulo);
        }
    }
}
