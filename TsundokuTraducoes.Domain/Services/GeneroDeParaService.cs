using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;

namespace TsundokuTraducoes.Domain.Services
{
    public class GeneroDeParaService : IGeneroDeParaService
    {
        private readonly IGeneroDeParaRepository _generoDeParaRepository;

        public GeneroDeParaService(IGeneroDeParaRepository generoDeParaRepository)
        {
            _generoDeParaRepository = generoDeParaRepository;
        }

        public async Task<List<Genero>> CarregaListaGenerosNovel(List<GeneroNovel> generoNovels)
        {
            return await _generoDeParaRepository.CarregaListaGenerosNovel(generoNovels);
        }
        
        public async Task<List<Genero>> CarregaListaGenerosComic(List<GeneroComic> generoComics)
        {
            return await _generoDeParaRepository.CarregaListaGenerosComic(generoComics);
        }

        
        public Task AdicionaGeneroNovel(GeneroNovel generoNovel)
        {
            throw new NotImplementedException();
        }

        public Task AdicionaGeneroComic(GeneroComic generoComic)
        {
            throw new NotImplementedException();
        }


        public void ExcluiGeneroNovel(GeneroNovel generoNovel)
        {
            throw new NotImplementedException();
        }

        public void ExcluiGeneroComic(GeneroComic generoComic)
        {
            throw new NotImplementedException();
        }
    }
}
