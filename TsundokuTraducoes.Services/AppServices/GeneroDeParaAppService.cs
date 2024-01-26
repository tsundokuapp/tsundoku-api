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

        public async Task<List<RetornoGenero>> CarregaListaGenerosNovel(List<GeneroNovel> generoNovels)
        {
            var listaGeneroNovel = await _generoDeParaService.CarregaListaGenerosNovel(generoNovels);
            var listaRetornoGenero = new List<RetornoGenero>();

            foreach (var genero in listaGeneroNovel)
            {
                listaRetornoGenero.Add(new RetornoGenero
                {
                    Id = genero.Id,
                    Descricao = genero.Descricao,
                    Slug = genero.Slug
                });
            }

            return listaRetornoGenero;
        }

        public async Task<List<RetornoGenero>> CarregaListaGenerosComic(List<GeneroComic> generoComics)
        {
            var listaGeneroComic = await _generoDeParaService.CarregaListaGenerosComic(generoComics);
            var listaRetornoGenero = new List<RetornoGenero>();

            foreach (var genero in listaGeneroComic)
            {
                listaRetornoGenero.Add(new RetornoGenero
                {
                    Id = genero.Id,
                    Descricao = genero.Descricao,
                    Slug = genero.Slug
                });
            }

            return listaRetornoGenero;
        }       


        public Task AdicionaGeneroComic(GeneroComic generoComic)
        {
            throw new NotImplementedException();
        }

        public Task AdicionaGeneroNovel(GeneroNovel generoNovel)
        {
            throw new NotImplementedException();
        }

        
        public void ExcluiGeneroComic(GeneroComic generoComic)
        {
            throw new NotImplementedException();
        }

        public void ExcluiGeneroNovel(GeneroNovel generoNovel)
        {
            throw new NotImplementedException();
        }
    }
}
