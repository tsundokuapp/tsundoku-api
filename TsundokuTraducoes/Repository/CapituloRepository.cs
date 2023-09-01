using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models.Capitulo;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Utilidades;
using static Dapper.SqlMapper;

namespace TsundokuTraducoes.Api.Repository
{
    public class CapituloRepository : Repository, ICapituloRepository
    {
        public CapituloRepository(TsundokuContext context) : base(context) { }

        public async Task<List<CapituloNovel>> RetornaListaCapitulosNovel(Guid? volumeId = null)
        {
            object parametro = null;

            if (volumeId != null)
            {
                parametro = new { VolumeId = volumeId.Value };
            }

            var listaCapitulosNovel = await _contextDapper.QueryAsync<CapituloNovel>(RetornaQueryListaCapitulosNovel(volumeId), parametro);
            return listaCapitulosNovel.ToList();
        }

        public async Task<List<CapituloComic>> RetornaListaCapitulosComic(Guid? volumeId = null)
        {
            object parametro = null;

            if (volumeId != null)
            {
                parametro = new { VolumeId = volumeId.Value };
            }

            var listaCapitulosNovel = await _contextDapper.QueryAsync<CapituloComic>(RetornaQueryListaCapitulosComic(volumeId), parametro);
            return listaCapitulosNovel.ToList();
        }


        public async Task<CapituloNovel> RetornaCapituloNovelPorId(Guid capituloId)
        {
            var capitulo = await RetornaListaCapitulosNovel();
            return capitulo.FirstOrDefault(f => f.Id == capituloId);
        }


        public async Task<CapituloComic> RetornaCapituloComicPorId(Guid capituloId)
        {
            var capitulo = await RetornaListaCapitulosComic();
            return capitulo.SingleOrDefault(f => f.Id == capituloId);
        }


        public async Task AdicionaCapituloNovel(CapituloNovel volumeNovel)
        {
            await AdicionaEntidadeBancoDados(volumeNovel);
        }

        public async Task AdicionaCapituloComic(CapituloComic volumeComic)
        {
            await AdicionaEntidadeBancoDados(volumeComic);
        }


        public async Task<CapituloNovel> AtualizaCapituloNovel(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = await _context.CapitulosNovel.SingleOrDefaultAsync(s => s.Id == capituloDTO.Id);

            capituloDTO.DiretorioImagemCapitulo = capituloEncontrado.DiretorioImagemCapitulo;
            _context.Entry(capituloEncontrado).CurrentValues.SetValues(capituloDTO);
            capituloEncontrado.DataAlteracao = DateTime.Now;

            return capituloEncontrado;
        }

        public async Task<CapituloComic> AtualizaCapituloComic(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = await _context.CapitulosComic.SingleOrDefaultAsync(s => s.Id == capituloDTO.Id);

            capituloDTO.DiretorioImagemCapitulo = capituloEncontrado.DiretorioImagemCapitulo;
            _context.Entry(capituloEncontrado).CurrentValues.SetValues(capituloDTO);
            capituloEncontrado.DataAlteracao = DateTime.Now;

            return capituloEncontrado;
        }


        public void ExcluiCapituloNovel(CapituloNovel capituloNovel)
        {
            ExcluiEntidadeBancoDados(capituloNovel);
        }

        public void ExcluiCapituloComic(CapituloComic capituloComic)
        {
            ExcluiEntidadeBancoDados(capituloComic);
        }


        public async Task<CapituloNovel> RetornaCapituloNovelExistente(CapituloDTO capituloDTO)
        {
            var parametros = new
            {
                VolumeId = capituloDTO.VolumeId,
                Slug = capituloDTO.Slug
            };

            var sql = @"SELECT * 
                          FROM CapitulosNovel
                         WHERE VolumeId = @VolumeId
                           AND Slug LIKE @Slug;";

            
            var capituloExistente = await _contextDapper.QueryAsync<CapituloNovel>(sql, parametros);
            return capituloExistente.FirstOrDefault();
        }

        public async Task<CapituloComic> RetornaCapituloComicExistente(CapituloDTO capituloDTO)
        {
            var parametros = new
            {
                VolumeId = capituloDTO.VolumeId,
                Slug = capituloDTO.Slug
            };
            
            var sql = @"SELECT * 
                          FROM CapitulosComic
                         WHERE VolumeId = @VolumeId
                           AND Slug LIKE @Slug;";

            var capituloExistente = await _contextDapper.QueryAsync<CapituloComic>(sql, parametros);
            return capituloExistente.FirstOrDefault();
        }


        private string RetornaQueryListaCapitulosNovel(Guid? volumeId)
        {
            var condicao = (volumeId != null) ? $"WHERE VolumeId = @VolumeId" : "";
            return $@"SELECT *
                       FROM CapitulosNovel
                      {condicao}
                      ORDER BY OrdemCapitulo DESC";
        }

        private string RetornaQueryListaCapitulosComic(Guid? volumeId)
        {
            var condicao = (volumeId != null) ? $"WHERE VolumeId = @VolumeId" : "";
            return $@"SELECT *
                       FROM CapitulosComic
                      {condicao}
                      ORDER BY OrdemCapitulo DESC";
        }


        public void AtualizaNovelPorCapitulo(Novel novel, CapituloNovel capituloNovel)
        {
            var parametros = new
            {
                novel.Id,
                NumeroUltimoCapitulo = TratamentoDeStrings.RetornaDescritivoCapitulo(capituloNovel.Numero, capituloNovel.Parte),
                SlugUltimoCapitulo = capituloNovel.Slug,
                DataAtualizacaoUltimoCapitulo = capituloNovel.DataInclusao
            };

            var sql = @"UPDATE Novels 
                           SET NumeroUltimoCapitulo = @NumeroUltimoCapitulo, 
                               SlugUltimoCapitulo = @SlugUltimoCapitulo, 
                               DataAtualizacaoUltimoCapitulo = @DataAtualizacaoUltimoCapitulo 
                         WHERE Id = @Id;";

            _contextDapper.Query(sql, parametros);
        }

        public void AtualizaComicPorCapitulo(Comic comic, CapituloComic capituloComic)
        {
            var parametros = new
            {
                comic.Id,
                NumeroUltimoCapitulo = TratamentoDeStrings.RetornaDescritivoCapitulo(capituloComic.Numero, capituloComic.Parte),
                SlugUltimoCapitulo = capituloComic.Slug,
                DataAtualizacaoUltimoCapitulo = capituloComic.DataInclusao
            };

            var sql = @"UPDATE Comics 
                           SET NumeroUltimoCapitulo = @NumeroUltimoCapitulo, 
                               SlugUltimoCapitulo = @SlugUltimoCapitulo, 
                               DataAtualizacaoUltimoCapitulo = @DataAtualizacaoUltimoCapitulo 
                         WHERE Id = @Id;";

            _contextDapper.Query(sql, parametros);
        }
    }
}
