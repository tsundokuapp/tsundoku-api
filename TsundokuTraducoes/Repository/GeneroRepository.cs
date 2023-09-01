﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.DePara;
using TsundokuTraducoes.Api.Models.Generos;
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

        public async Task<List<Genero>> RetornaListaGeneros()
        {
            var listaGeneros = await _context.Generos.ToListAsync();
            return listaGeneros;
        }


        public async Task AdicionaGeneroNovel(GeneroNovel generoNovel)
        {
            await _context.AddAsync(generoNovel);
        }

        public async Task AdicionaGeneroComic(GeneroComic generoComic)
        {
            await _context.AddAsync(generoComic);
        }

                
        public void ExcluiGeneroNovel(GeneroNovel generoNovel)
        {
            _context.Remove(generoNovel);
        }

        public void ExcluiGeneroComic(GeneroComic generoComic)
        {
            _context.Remove(generoComic);
        }
        

        public async Task<List<RetornoGenero>> CarregaListaGenerosNovel(List<GeneroNovel> generosNovel)
        {
            var listaGeneros = new List<RetornoGenero>();

            foreach (var generoNovel in generosNovel)
            {
                var generoEncontrado = await _context.Generos.SingleAsync(s => s.Id == generoNovel.GeneroId);
                listaGeneros.Add(new RetornoGenero
                {
                    Id = generoEncontrado.Id,
                    Descricao = generoEncontrado.Descricao,
                    Slug = generoEncontrado.Slug
                });
            }

            return listaGeneros;
        }

        public async Task<List<RetornoGenero>> CarregaListaGenerosComic(List<GeneroComic> generosComic)
        {
            var listaGeneros = new List<RetornoGenero>();

            foreach (var generoComic in generosComic)
            {
                var generoEncontrado = await _context.Generos.SingleAsync(s => s.Id == generoComic.GeneroId);
                listaGeneros.Add(new RetornoGenero
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
