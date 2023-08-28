﻿using Dapper;
using System;
using System.Data;
using System.Linq;
using TsundokuTraducoes.Api.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Repository.Interfaces;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Genero;
using TsundokuTraducoes.Api.Models.Recomendacao.Comic;

namespace TsundokuTraducoes.Api.Repository
{
    public class ObraRepository : IObraRepository
    {
        private readonly TsundokuContext _context;
        private readonly IGeneroRepository _generoRepository;
        private readonly IDbConnection _contextDapper;

        public ObraRepository(TsundokuContext context, IGeneroRepository generoRepository)
        {
            _context = context;
            _contextDapper = new TsundokuContextDapper().RetornaSqlConnetionDapper();
            _generoRepository = generoRepository;
        }

        public async Task AdicionaObra(Novel obra)
        {
            await _context.AddAsync(obra);
        }

        public async Task AdicionaObraRecomendada(ComicRecomendada obraRecomendada)
        {
            await _context.AddAsync(obraRecomendada);
        }

        public async Task AdicionaComentarioObraRecomendada(ComentarioComicRecomendada comentarioObraRecomendada)
        {
            await _context.AddAsync(comentarioObraRecomendada);
        }

        public void ExcluiObra(Novel obra)
        {
            _context.Remove(obra);
        }

        public async Task<bool> AlteracoesSalvas()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Novel>> RetornaListaObras()
        {
            var listaObras = new List<Novel>();
            await _contextDapper.QueryAsync(RetornaQueryListaObra(),
                new[]
                {
                    typeof(Novel),
                    typeof(GeneroObra)
                },
                (objects) =>
                {
                    var obra = (Novel)objects[0];
                    var generoObra = (GeneroObra)objects[1];

                    if (listaObras.SingleOrDefault(o => o.Id == obra.Id) == null)
                    {
                        listaObras.Add(obra);
                    }
                    else
                    {
                        obra = listaObras.SingleOrDefault(o => o.Id == obra.Id);
                    }

                    AdicionaGeneroObra(obra, generoObra);

                    return obra;
                }, splitOn: "Id, ObraId");

            return listaObras;
        }

        public async Task<Novel> RetornaObraPorId(int obraId)
        {
            var listaObras = await RetornaListaObras();
            return listaObras.SingleOrDefault(f => f.Id == obraId);
        }

        public async Task<Novel> AtualizaObra(ObraDTO obraDTO)
        {
            var obraEncontrada = _context.Novel.Include(o => o.GenerosObra).SingleOrDefault(o => o.Id == obraDTO.Id);
            _context.Entry(obraEncontrada).CurrentValues.SetValues(obraDTO);
            obraEncontrada.DataAlteracao = DateTime.Now;

            await AtualizaDadosObraRecomendada(obraEncontrada);
            return obraEncontrada;
        }

        private async Task AtualizaDadosObraRecomendada(Novel obraEncontrada)
        {
            var parametros = new
            {
                IdObra = obraEncontrada.Id,
                UrlImagemCapaPrincipal = obraEncontrada.ImagemCapaPrincipal,
                Sinopse = obraEncontrada.Sinopse,
                TituloAliasObra = obraEncontrada.Alias,
                SlugObra = obraEncontrada.Slug
            };

            var sql = @"UPDATE ObraRecomendada 
                           SET UrlImagemCapaPrincipal = @UrlImagemCapaPrincipal,
                               Sinopse = @Sinopse,
                               TituloAliasObra = @TituloAliasObra,
                               SlugObra = @SlugObra
                         WHERE IdObra = @IdObra;";

            await _contextDapper.ExecuteAsync(sql, parametros);
        }

        private void AdicionaGeneroObra(Novel obra, GeneroObra generoObra)
        {
            if (generoObra != null)
                obra.GenerosObra.Add(generoObra);
        }

        public string RetornaQueryListaObra()
        {
            var sql = @"SELECT O.*, GO.*
                          FROM Obra O
                         INNER JOIN GeneroObra GO ON GO.ObraId = O.Id
                         ORDER BY O.Titulo;";

            return sql;
        }

        public async Task<List<Genero>> RetornaListaGeneros()
        {
            var listaGeneros = await _contextDapper.QueryAsync<Genero>("SELECT * FROM Genero ORDER BY Id");
            return listaGeneros.ToList();
        }

        public async Task InsereGenerosObra(ObraDTO obraDTO, Novel obraEncontrada, bool inclusao)
        {
            if (inclusao)
            {
                obraEncontrada.GenerosObra = new List<GeneroObra>();
            }
            else
            {
                foreach (var generoObra in obraEncontrada.GenerosObra)
                {
                    _generoRepository.ExcluiGeneroObra(generoObra);
                }
            }

            var arrayGenero = obraDTO.ListaGeneros[0]?.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            if (arrayGenero != null && arrayGenero.Length > 0)
            {
                foreach (var genero in arrayGenero)
                {
                    var generoEncontrado = _context.Genero.Single(s => s.Slug == genero);
                    await _generoRepository.AdicionaGeneroObra(new GeneroObra
                    {
                        GeneroId = generoEncontrado.Id,
                        ObraId = obraEncontrada.Id
                    });

                    await AlteracoesSalvas();
                }
            }
        }

        public void InsereListaComentariosObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO, ComicRecomendada obraRecomendada)
        {
            if (obraRecomendadaDTO.ListaComentarioObraRecomendadaDTO != null && obraRecomendadaDTO.ListaComentarioObraRecomendadaDTO.Count > 0)
            {
                foreach (var comentarioObraRecomendadaDTO in obraRecomendadaDTO.ListaComentarioObraRecomendadaDTO)
                {
                    obraRecomendada.ListaComentarioComicRecomendada.Add(new ComentarioComicRecomendada
                    {
                        AutorComentario = comentarioObraRecomendadaDTO.AutorComentario,
                        Comentario = comentarioObraRecomendadaDTO.Comentario,
                        ComicRecomendadaId = obraRecomendada.Id,
                    });
                }
            }
        }

        public async Task InsereComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            await AdicionaComentarioObraRecomendada(new ComentarioComicRecomendada
            {
                AutorComentario = comentarioObraRecomendadaDTO.AutorComentario,
                Comentario = comentarioObraRecomendadaDTO.Comentario,
                ComicRecomendadaId = comentarioObraRecomendadaDTO.ObraRecomendadaId,
            });
        }

        public ComentarioComicRecomendada RetornaComentarioObraRecomendadaPorId(int id)
        {
            return _context.ComentarioComicRecomendada.SingleOrDefault(s => s.Id == id);
        }

        public ComentarioComicRecomendada AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var comentarioObraRecomendada = _context.ComentarioComicRecomendada.SingleOrDefault(s => s.Id == comentarioObraRecomendadaDTO.Id);
            _context.Entry(comentarioObraRecomendada).CurrentValues.SetValues(comentarioObraRecomendadaDTO);

            return comentarioObraRecomendada;
        }

        public List<ComicRecomendada> RetornaListaObraRecomendada()
        {
            return _context.ComicRecomendada.Include(o => o.ListaComentarioComicRecomendada).ToList();
        }

        public ComicRecomendada RetornaObraRecomendadaPorId(int id)
        {
            return RetornaListaObraRecomendada().FirstOrDefault(o => o.Id == id);
        }

        public ComicRecomendada RetornaObraRecomendadaPorObraId(int idObra)
        {
            return RetornaListaObraRecomendada().FirstOrDefault(o => o.IdObra == idObra);
        }

        public async Task<Novel> RetornaObraExistente(string titulo)
        {
            titulo = titulo.Trim();

            var parametro = new
            {
                Titulo = $"%{titulo}%",
            };

            var sql = $@"SELECT * 
                           FROM Obra 
                          WHERE Titulo LIKE @Titulo";

            var obraExistente = await _contextDapper.QueryAsync<Novel>(sql, parametro);
            return obraExistente.FirstOrDefault();
        }
    }
}
