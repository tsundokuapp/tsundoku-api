using Dapper;
using System;
using System.Data;
using System.Linq;
using TsundokuTraducoes.Api.Data;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;
using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Repository.Interfaces;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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

        public async Task Adiciona<t>(t entity) where t : class
        {
            await _context.AddAsync(entity);
        }

        public void Atualiza<t>(t entity) where t : class
        {
            _context.Update(entity);
        }

        public void Exclui<t>(t entity) where t : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> AlteracoesSalvas()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Obra>> RetornaListaObras()
        {
            var listaObras = new List<Obra>();
            await _contextDapper.QueryAsync(RetornaQueryListaObra(),
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

        public async Task<Obra> RetornaObraPorId(int obraId)
        {
            var listaObras = await RetornaListaObras();
            return listaObras.SingleOrDefault(f => f.Id == obraId);
        }

        public async Task<Obra> AtualizaObra(ObraDTO obraDTO)
        {
            var obraEncontrada = _context.Obra.Include(o => o.GenerosObra).SingleOrDefault(o => o.Id == obraDTO.Id);
            _context.Entry(obraEncontrada).CurrentValues.SetValues(obraDTO);
            obraEncontrada.DataAlteracao = DateTime.Now;

            await AtualizaDadosObraRecomendada(obraEncontrada);
            return obraEncontrada;
        }

        private async Task AtualizaDadosObraRecomendada(Obra obraEncontrada)
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

        private void AdicionaGeneroObra(Obra obra, GeneroObra generoObra)
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

        public async Task InsereGenerosObra(ObraDTO obraDTO, Obra obraEncontrada, bool inclusao)
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
                    await Adiciona(new GeneroObra
                    {
                        GeneroId = generoEncontrado.Id,
                        ObraId = obraEncontrada.Id
                    });

                    await AlteracoesSalvas();
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

        public async Task InsereComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            await Adiciona(new ComentarioObraRecomendada
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
            return _context.ObraRecomendada.Include(o => o.ListaComentarioObraRecomendada).ToList();
        }

        public ObraRecomendada RetornaObraRecomendadaPorId(int id)
        {
            return RetornaListaObraRecomendada().FirstOrDefault(o => o.Id == id);
        }

        public ObraRecomendada RetornaObraRecomendadaPorObraId(int idObra)
        {
            return RetornaListaObraRecomendada().FirstOrDefault(o => o.IdObra == idObra);
        }

        public async Task<Obra> RetornaObraExistente(string titulo)
        {
            titulo = titulo.Trim();

            var parametro = new
            {
                Titulo = $"%{titulo}%",
            };

            var sql = $@"SELECT * 
                           FROM Obra 
                          WHERE Titulo LIKE @Titulo";

            var obraExistente = await _contextDapper.QueryAsync<Obra>(sql, parametro);
            return obraExistente.FirstOrDefault();
        }
    }
}
