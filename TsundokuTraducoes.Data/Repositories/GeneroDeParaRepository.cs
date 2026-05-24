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
            if (generoNovels == null || generoNovels.Count == 0)
            {
                return listaGeneros;
            }

            var idsSemGenero = generoNovels
                .Where(g => g.Genero == null)
                .Select(g => g.GeneroId)
                .Distinct()
                .ToList();

            var generosPorId = new Dictionary<Guid, Genero>();
            foreach (var generoNovel in generoNovels)
            {
                if (generoNovel.Genero != null)
                {
                    generosPorId[generoNovel.GeneroId] = generoNovel.Genero;
                }
            }

            if (idsSemGenero.Count > 0)
            {
                var generos = await _contextBase.Generos
                    .AsNoTracking()
                    .Where(g => idsSemGenero.Contains(g.Id))
                    .ToListAsync();

                foreach (var genero in generos)
                {
                    generosPorId[genero.Id] = genero;
                }
            }

            foreach (var generoNovel in generoNovels)
            {
                if (generosPorId.TryGetValue(generoNovel.GeneroId, out var generoEncontrado))
                {
                    listaGeneros.Add(new Genero
                    {
                        Id = generoEncontrado.Id,
                        Descricao = generoEncontrado.Descricao,
                        Slug = generoEncontrado.Slug
                    });
                }
            }

            return listaGeneros;
        }

        public async Task<List<Genero>> CarregaListaGenerosComic(List<GeneroComic> generoComics)
        {
            var listaGeneros = new List<Genero>();
            if (generoComics == null || generoComics.Count == 0)
            {
                return listaGeneros;
            }

            var idsSemGenero = generoComics
                .Where(g => g.Genero == null)
                .Select(g => g.GeneroId)
                .Distinct()
                .ToList();

            var generosPorId = new Dictionary<Guid, Genero>();
            foreach (var generoComic in generoComics)
            {
                if (generoComic.Genero != null)
                {
                    generosPorId[generoComic.GeneroId] = generoComic.Genero;
                }
            }

            if (idsSemGenero.Count > 0)
            {
                var generos = await _contextBase.Generos
                    .AsNoTracking()
                    .Where(g => idsSemGenero.Contains(g.Id))
                    .ToListAsync();

                foreach (var genero in generos)
                {
                    generosPorId[genero.Id] = genero;
                }
            }

            foreach (var generoComic in generoComics)
            {
                if (generosPorId.TryGetValue(generoComic.GeneroId, out var generoEncontrado))
                {
                    listaGeneros.Add(new Genero
                    {
                        Id = generoEncontrado.Id,
                        Descricao = generoEncontrado.Descricao,
                        Slug = generoEncontrado.Slug
                    });
                }
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
