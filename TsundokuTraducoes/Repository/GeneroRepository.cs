using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Genero;
using TsundokuTraducoes.Api.Repository.Interfaces;

namespace TsundokuTraducoes.Api.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly TsundokuContext _context;

        public GeneroRepository(TsundokuContext context)
        {
            _context = context;
        }

        public async Task AdicionaGeneroObra(GeneroObra generoObra)
        {
            await _context.AddAsync(generoObra);
        }

        public void ExcluiGeneroObra(GeneroObra generoObra)
        {
            _context.Remove(generoObra);
        }

        public async Task<List<RetornoGenero>> CarregaListaGeneros(List<GeneroObra> generoObras)
        {
            var listaGeneros = new List<RetornoGenero>();

            foreach (var item in generoObras)
            {
                var generoEncontrado = await _context.Genero.SingleAsync(s => s.Id == item.GeneroId);
                listaGeneros.Add( new RetornoGenero
                {
                    Id = generoEncontrado.Id,
                    Descricao = generoEncontrado.Descricao,
                    Slug = generoEncontrado.Slug
                });
            }

            return listaGeneros;
        }
    }
}
