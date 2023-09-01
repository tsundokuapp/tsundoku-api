﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Volume;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.Repository
{
    public class VolumeRepository : Repository, IVolumeRepository
    {
        public VolumeRepository(TsundokuContext context) : base(context) { }

        public async Task<List<VolumeNovel>> RetornaListaVolumesNovel(Guid? novelId = null)
        {
            object parametro = null;

            if (novelId != null)
            {
                parametro = new { NovelId = novelId.Value };
            }

            var listaVolumesNovel = await _contextDapper.QueryAsync<VolumeNovel>(RetornaQueryListaVolumes(novelId), parametro);
            return listaVolumesNovel.ToList();
        }

        public async Task<List<VolumeComic>> RetornaListaVolumesComic(Guid? comicId = null)
        {
            object parametro = null;

            if (comicId != null)
            {
                parametro = new { ComicId = comicId.Value };
            }

            var listaVolumesComic = await _contextDapper.QueryAsync<VolumeComic>(RetornaQueryListaComics(comicId), parametro);
            return listaVolumesComic.ToList();
        }


        public async Task<VolumeNovel> RetornaVolumeNovelPorId(Guid volumeId)
        {
            var volume = await RetornaListaVolumesNovel();
            return volume.FirstOrDefault(f => f.Id == volumeId);
        }

        public async Task<VolumeComic> RetornaVolumeComicPorId(Guid volumeId)
        {
            var volume = await RetornaListaVolumesComic();
            return volume.FirstOrDefault(f => f.Id == volumeId);
        }


        public async Task AdicionaVolumeNovel(VolumeNovel volumeNovel)
        {
            await AdicionaEntidadeBancoDados(volumeNovel);
        }

        public async Task AdicionaVolumeComic(VolumeComic volumeComic)
        {
            await AdicionaEntidadeBancoDados(volumeComic);
        }


        public VolumeNovel AtualizaVolumeNovel(VolumeDTO VolumeDTO)
        {
            var volumeEncontrado = _context.VolumesNovel.SingleOrDefault(s => s.Id == VolumeDTO.Id);
            var tituloVolumeVazio = VerificaCampoVazio(volumeEncontrado.Titulo, VolumeDTO.Titulo);
            if (tituloVolumeVazio)
                VolumeDTO.Titulo = string.Empty;

            var sinopseVolumeVazia = VerificaCampoVazio(volumeEncontrado.Sinopse, VolumeDTO.Sinopse);
            if (sinopseVolumeVazia)
                VolumeDTO.Sinopse = string.Empty;

            if (string.IsNullOrEmpty(VolumeDTO.ImagemVolume))
                VolumeDTO.ImagemVolume = volumeEncontrado.ImagemVolume;

            VolumeDTO.DiretorioImagemVolume = volumeEncontrado.DiretorioImagemVolume;
            _context.Entry(volumeEncontrado).CurrentValues.SetValues(VolumeDTO);
            volumeEncontrado.DataAlteracao = DateTime.Now;

            return volumeEncontrado;
        }

        public VolumeComic AtualizaVolumeComic(VolumeDTO VolumeDTO)
        {
            var volumeEncontrado = _context.VolumesComic.SingleOrDefault(s => s.Id == VolumeDTO.Id);
            var tituloVolumeVazio = VerificaCampoVazio(volumeEncontrado.Titulo, VolumeDTO.Titulo);
            if (tituloVolumeVazio)
                VolumeDTO.Titulo = string.Empty;

            var sinopseVolumeVazia = VerificaCampoVazio(volumeEncontrado.Sinopse, VolumeDTO.Sinopse);
            if (sinopseVolumeVazia)
                VolumeDTO.Sinopse = string.Empty;

            if (string.IsNullOrEmpty(VolumeDTO.ImagemVolume))
                VolumeDTO.ImagemVolume = volumeEncontrado.ImagemVolume;

            VolumeDTO.DiretorioImagemVolume = volumeEncontrado.DiretorioImagemVolume;
            _context.Entry(volumeEncontrado).CurrentValues.SetValues(VolumeDTO);
            volumeEncontrado.DataAlteracao = DateTime.Now;

            return volumeEncontrado;
        }


        public void ExcluiVolumeNovel(VolumeNovel volumeNovel)
        {
            ExcluiEntidadeBancoDados(volumeNovel);
        }

        public void ExcluiVolumeComic(VolumeComic volumeComic)
        {
            ExcluiEntidadeBancoDados(volumeComic);
        }


        public void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel)
        {
            var parametros = new
            {
                novel.Id,
                ImagemCapaUltimoVolume = volumeNovel.ImagemVolume,
                NumeroUltimoVolume = TratamentoDeStrings.RetornaDescritivoVolume(volumeNovel.Numero),
                SlugUltimoVolume = volumeNovel.Slug
            };

            var sql = @"UPDATE Novels
                           SET ImagemCapaUltimoVolume = @ImagemCapaUltimoVolume,
                               NumeroUltimoVolume = @NumeroUltimoVolume,
                               SlugUltimoVolume = @SlugUltimoVolume
                         WHERE Id = @Id;";

            _contextDapper.Query(sql, parametros);
        }

        public void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic)
        {
            var parametros = new
            {
                comic.Id,
                ImagemCapaUltimoVolume = volumeComic.ImagemVolume,
                NumeroUltimoVolume = TratamentoDeStrings.RetornaDescritivoVolume(volumeComic.Numero),
                SlugUltimoVolume = volumeComic.Slug
            };

            var sql = @"UPDATE Comics
                           SET ImagemCapaUltimoVolume = @ImagemCapaUltimoVolume,
                               NumeroUltimoVolume = @NumeroUltimoVolume,
                               SlugUltimoVolume = @SlugUltimoVolume
                         WHERE Id = @Id;";

            _contextDapper.Query(sql, parametros);
        }


        public async Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeDTO volumeDTO)
        {
            var parametros = new
            {
                NovelId = volumeDTO.NovelId,
                Numero = volumeDTO.Numero,
                Slug = volumeDTO.Slug,
            };

            var sql = @"SELECT * 
                          FROM VolumesNovel
                         WHERE NovelId = @NovelId
                           AND (Numero LIKE @Numero OR Slug LIKE @Slug);";


            var volumeExistente = await _contextDapper.QueryAsync<VolumeNovel>(sql, parametros);
            return volumeExistente.FirstOrDefault();
        }

        public async Task<VolumeComic> RetornaVolumeComicExistente(VolumeDTO volumeDTO)
        {
            var parametros = new
            {
                ComicId = volumeDTO.ComicId,
                Numero = volumeDTO.Numero,
                Slug = volumeDTO.Slug,
            };

            var sql = @"SELECT * 
                          FROM VolumesComic
                         WHERE ComicId = @ComicId
                           AND (Numero LIKE @Numero OR Slug LIKE @Slug);";


            var volumeExistente = await _contextDapper.QueryAsync<VolumeComic>(sql, parametros);
            return volumeExistente.FirstOrDefault();
        }


        private string RetornaQueryListaVolumes(Guid? novelId)
        {
            var condicao = (novelId != null) ? $"WHERE NovelId = @NovelId" : "";
            return $@"SELECT *
                       FROM VolumesNovel
                      {condicao}
                      ORDER BY Numero ASC";
        }

        private string RetornaQueryListaComics(Guid? comicId)
        {
            var condicao = (comicId != null) ? $"WHERE ComicId = @ComicId" : "";
            return $@"SELECT *
                       FROM VolumesComic
                      {condicao}
                      ORDER BY Numero ASC";
        }


        private bool VerificaCampoVazio(string campoVolumeEncontrado, string campoVolumeDTO)
        {
            return string.IsNullOrEmpty(campoVolumeDTO) ||
               !string.IsNullOrEmpty(campoVolumeDTO) && campoVolumeDTO.ToLower().Contains("null") ||
               !string.IsNullOrEmpty(campoVolumeEncontrado) && campoVolumeEncontrado.ToLower().Contains("null");
        }
    }
}
