using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Services.AppServices
{
    public class GeneroDeParaAppService : IGeneroDeParaAppService
    {
        private readonly IGeneroDeParaService _generoDeParaService;

        public GeneroDeParaAppService(IGeneroDeParaService generoDeParaService)
        {
            _generoDeParaService = generoDeParaService;
        }

        public async Task<List<RetornoGeneroObra>> CarregaListaGenerosNovel(List<GeneroNovel> generoNovels)
        {
            var listaGeneroNovel = await _generoDeParaService.CarregaListaGenerosNovel(generoNovels);
            var listaRetornoGenero = new List<RetornoGeneroObra>();

            foreach (var genero in listaGeneroNovel)
            {
                listaRetornoGenero.Add(new RetornoGeneroObra
                {                    
                    Label = genero.Descricao,
                    Value = genero.Slug
                });
            }

            return listaRetornoGenero;
        }

        public async Task<List<RetornoGeneroObra>> CarregaListaGenerosComic(List<GeneroComic> generoComics)
        {
            var listaGeneroComic = await _generoDeParaService.CarregaListaGenerosComic(generoComics);
            var listaRetornoGenero = new List<RetornoGeneroObra>();

            foreach (var genero in listaGeneroComic)
            {
                listaRetornoGenero.Add(new RetornoGeneroObra
                {
                    Label = genero.Descricao,
                    Value = genero.Slug
                });
            }

            return listaRetornoGenero;
        }       
    }
}