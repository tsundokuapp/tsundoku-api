using Dapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.Repository.Interfaces;

namespace TsundokuTraducoes.Api.Repository
{
    public class ObraRepository : IObraRepository
    {
        private readonly TsundokuContext _context;
        private readonly IDbConnection _contextDapper;

        public ObraRepository(TsundokuContext context)
        {
            _context = context;
            _contextDapper = new TsundokuContextDapper().RetornaSqlConnetionDapper();
        }

        public void Adiciona<t>(t entity) where t : class
        {
            _context.Add(entity);
        }

        public void Atualiza<t>(t entity) where t : class
        {
            _context.Update(entity);
        }

        public void Exclui<t>(t entity) where t : class
        {
            _context.Remove(entity);
        }

        public bool AlteracoesSalvas()
        {
            return _context.SaveChanges() > 0;
        }

        public List<Obra> RetornaListaObras()
        {
            var listaObras = new List<Obra>();
            _contextDapper.Query(RetornaQueryListaObra(),
                new[]
                {
                    typeof(Obra),
                    typeof(GeneroObra)
                },
                (objects) =>
                {
                    var obra = (Obra)objects[0];
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

        public Obra RetornaObraPorId(int obraId)
        {
            return RetornaListaObras().SingleOrDefault(f => f.Id == obraId);
        }

        public Obra AtualizaObra(ObraDTO obraDTO)
        {
            var obraEncontrada = _context.Obra.Include(o => o.GenerosObra).SingleOrDefault(o => o.Id == obraDTO.Id);
            _context.Entry(obraEncontrada).CurrentValues.SetValues(obraDTO);
            obraEncontrada.DataAlteracao = DateTime.Now;

            AtualizaDadosObraRecomendada(obraEncontrada);

            return obraEncontrada;
        }

        private void AtualizaDadosObraRecomendada(Obra obraEncontrada)
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

            _contextDapper.Execute(sql, parametros);
        }

        private void AdicionaGeneroObra(Obra obra, GeneroObra generoObra)
        {
            if (generoObra != null)
            {
                obra.GenerosObra.Add(generoObra);
            }
        }

        public string RetornaQueryListaObra()
        {
            var sql = @"SELECT O.*, GO.*
                          FROM Obra O
                         INNER JOIN GeneroObra GO ON GO.ObraId = O.Id
                         ORDER BY O.Titulo;";

            return sql;
        }

        public List<Genero> RetornaListaGeneros()
        {   
            return _contextDapper.Query<Genero>("SELECT * FROM Genero ORDER BY Id").ToList();
        }

        public void InsereGenerosObra(ObraDTO obraDTO, Obra obraEncontrada, bool inclusao)
        {
            if (inclusao)
            {
                obraEncontrada.GenerosObra = new List<GeneroObra>();
            }
            else
            {
                foreach (var generoObra in obraEncontrada.GenerosObra)
                {
                    Exclui(generoObra);
                }
            }

            var arrayGenero = obraDTO.ListaGeneros[0]?.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);

            if (arrayGenero != null && arrayGenero.Length > 0)
            {
                foreach (var genero in arrayGenero)
                {
                    var generoEncontrado = _context.Genero.Single(s => s.Slug == genero);
                    Adiciona(new GeneroObra
                    {
                        GeneroId = generoEncontrado.Id,
                        ObraId = obraEncontrada.Id
                    });

                    AlteracoesSalvas();
                }
            }
        }

        public void InsereListaComentariosObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO, ObraRecomendada obraRecomendada)
        {
            if (obraRecomendadaDTO.ListaComentarioObraRecomendadaDTO != null && obraRecomendadaDTO.ListaComentarioObraRecomendadaDTO.Count > 0)
            {
                foreach (var comentarioObraRecomendadaDTO in obraRecomendadaDTO.ListaComentarioObraRecomendadaDTO)
                {
                    obraRecomendada.ListaComentarioObraRecomendada.Add(new ComentarioObraRecomendada
                    {
                        AutorComentario = comentarioObraRecomendadaDTO.AutorComentario,
                        Comentario = comentarioObraRecomendadaDTO.Comentario,
                        ObraRecomendadaId = obraRecomendada.Id,
                    });
                }
            }
        }

        public void InsereComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            Adiciona(new ComentarioObraRecomendada
            {
                AutorComentario = comentarioObraRecomendadaDTO.AutorComentario,
                Comentario = comentarioObraRecomendadaDTO.Comentario,
                ObraRecomendadaId = comentarioObraRecomendadaDTO.ObraRecomendadaId,
            });
        }

        public ComentarioObraRecomendada RetornaComentarioObraRecomendadaPorId(int id)
        {
            return _context.ComentarioObraRecomendada.SingleOrDefault(s => s.Id == id);
        }

        public ComentarioObraRecomendada AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var comentarioObraRecomendada = _context.ComentarioObraRecomendada.SingleOrDefault(s => s.Id == comentarioObraRecomendadaDTO.Id);
            _context.Entry(comentarioObraRecomendada).CurrentValues.SetValues(comentarioObraRecomendadaDTO);

            return comentarioObraRecomendada;
        }

        public List<ObraRecomendada> RetornaListaObraRecomendada()
        {
            var listaObraRecomenda = _context.ObraRecomendada.Include(o => o.ListaComentarioObraRecomendada).ToList();
            return listaObraRecomenda;
        }

        public ObraRecomendada RetornaObraRecomendadaPorId(int id)
        { 
            var obraRecomendada = RetornaListaObraRecomendada().FirstOrDefault(o => o.Id == id);
            return obraRecomendada;
        }

        public ObraRecomendada RetornaObraRecomendadaPorObraId(int idObra)
        {
            var obraRecomendada = RetornaListaObraRecomendada().FirstOrDefault(o => o.IdObra == idObra);
            return obraRecomendada;
        }
    }
}
