using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Data.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        protected readonly ContextBase _context;

        public GeneroRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<List<Genero>> RetornaListaGeneros()
        {
            return await _context.Generos.AsNoTracking()
                .Include(o => o.GenerosComic)
                .Include(o => o.GenerosNovel)
                .ToListAsync();
        }

        public async Task<Genero> RetornaGeneroPorId(Guid id)
        {
            return await _context.Generos.AsNoTracking()
                .Include(o => o.GenerosComic)
                .Include(o => o.GenerosNovel)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AdicionaGenero(Genero genero)
        {
            await _context.AddAsync(genero);
        }       

        public Genero AtualizaGenero(GeneroDTO generoDTO)
        {
            var generoEncontrado = _context.Generos.SingleOrDefault(x => x.Id == generoDTO.Id);
            _context.Entry(generoEncontrado).CurrentValues.SetValues(generoDTO);

            return generoEncontrado;
        }

        public void ExcluiGenero(Genero genero)
        {
            _context.Remove(genero);
        }

        public async Task<Genero> RetornaGeneroExistente(string slugGenero)
        {
            return await _context.Generos.AsNoTracking().Where(w => EF.Functions.Like(w.Slug, slugGenero)).FirstOrDefaultAsync();
        }

        public async Task<bool> AlteracoesSalvas()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}