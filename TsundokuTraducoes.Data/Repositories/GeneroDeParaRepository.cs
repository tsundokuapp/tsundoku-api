using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;

namespace TsundokuTraducoes.Data.Repositories
{
    public class GeneroDeParaRepository : IGeneroDeParaRepository
    {
        private readonly ContextBase _contextBase;

        public GeneroDeParaRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }

        public async Task<List<Genero>> CarregaListaGenerosNovel(List<GeneroNovel> generoNovels)
        {
            var listaGeneros = new List<Genero>();

            foreach (var generoNovel in generoNovels)
            {
                var generoEncontrado = await _contextBase.Generos.SingleAsync(s => s.Id == generoNovel.GeneroId);
                listaGeneros.Add(new Genero{ Id = generoEncontrado.Id, Descricao = generoEncontrado.Descricao, Slug = generoEncontrado.Slug});
            }

            return listaGeneros;
        }

        public async Task<List<Genero>> CarregaListaGenerosComic(List<GeneroComic> generoComics)
        {
            var listaGeneros = new List<Genero>();

            foreach (var generoComic in generoComics)
            {
                var generoEncontrado = await _contextBase.Generos.SingleAsync(s => s.Id == generoComic.GeneroId);
                listaGeneros.Add(new Genero{ Id = generoEncontrado.Id, Descricao = generoEncontrado.Descricao, Slug = generoEncontrado.Slug });
            }

            return listaGeneros;
        }


        public async Task AdicionaGeneroNovel(GeneroNovel generoNovel)
        {
            await _contextBase.AddAsync(generoNovel);
        }

        public async Task AdicionaGeneroComic(GeneroComic generoComic)
        {
            await _contextBase.AddAsync(generoComic);
        }

        
        public void ExcluiGeneroNovel(GeneroNovel generoNovel)
        {
            _contextBase.Remove(generoNovel);
        }

        public void ExcluiGeneroComic(GeneroComic generoComic)
        {
            _contextBase.Remove(generoComic);
        }
    }
}
