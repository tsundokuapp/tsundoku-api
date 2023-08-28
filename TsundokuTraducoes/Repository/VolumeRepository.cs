﻿using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Repository.Interfaces;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Volume;

namespace TsundokuTraducoes.Api.Repository
{
    public class VolumeRepository : IVolumeRepository
    {
        private readonly TsundokuContext _context;
        private readonly IDbConnection _contextDapper;
        private readonly IObraRepository _obraRepository;

        public VolumeRepository(TsundokuContext context, IObraRepository obraRepository)
        {
            _context = context;
            _obraRepository = obraRepository;
            _contextDapper = new TsundokuContextDapper().RetornaSqlConnetionDapper();
        }

        public void AdicionaVolume(VolumeNovel volume)
        {
            _context.Add(volume);
        }       

        public void ExcluiVolume(VolumeNovel volume)
        {
            _context.Remove(volume);
        }

        public bool AlteracoesSalvas()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<List<VolumeNovel>> RetornaListaVolumes(int? idObra = null)
        {
            var listaVolumes = await _contextDapper.QueryAsync<VolumeNovel>(RetornaQueryListaVolumes(idObra));
            return listaVolumes.ToList();
        }

        private string RetornaQueryListaVolumes(int? idObra)
        {
            var condicao = (idObra != null && idObra > 0) ? $"WHERE ObraId = {idObra.Value}" : "";
            return $@"SELECT *
                       FROM Volume
                      {condicao}
                      ORDER BY Numero ASC";
        }

        public async Task<VolumeNovel> RetornaVolumePorId(int volumeId)
        {
            var volume = await RetornaListaVolumes();
            return volume.FirstOrDefault(f => f.Id == volumeId);
        }

        public VolumeNovel AtualizaVolume(VolumeDTO VolumeDTO)
        {
            var volumeEncontrado = _context.VolumeNovel.SingleOrDefault(s => s.Id == VolumeDTO.Id);
            var tituloVolumeVazio = VerificaCampoVazio(volumeEncontrado.Titulo, VolumeDTO.Titulo);
            if (tituloVolumeVazio)
                VolumeDTO.Titulo = string.Empty;

            var sinopseVolumeVazia = VerificaCampoVazio(volumeEncontrado.Sinopse, VolumeDTO.Sinopse);
            if (sinopseVolumeVazia)
                VolumeDTO.Sinopse = string.Empty;

            if (string.IsNullOrEmpty(VolumeDTO.ImagemCapaVolume))
                VolumeDTO.ImagemCapaVolume = volumeEncontrado.ImagemVolume;

            _context.Entry(volumeEncontrado).CurrentValues.SetValues(VolumeDTO);
            volumeEncontrado.DataAlteracao = DateTime.Now;

            return volumeEncontrado;
        }

        private bool VerificaCampoVazio(string campoVolumeEncontrado, string campoVolumeDTO)
        {
            return string.IsNullOrEmpty(campoVolumeDTO) ||
               !string.IsNullOrEmpty(campoVolumeDTO) && campoVolumeDTO.ToLower().Contains("null") ||
               !string.IsNullOrEmpty(campoVolumeEncontrado) && campoVolumeEncontrado.ToLower().Contains("null");
        }

        public async Task<Novel> RetornaObraPorId(int obraId)
        {
            // TODO validar se dá erro quando ocorrer a refatoração do Crud do Volume - Olha eu aqui ainda e vou continuar até a próxima refatoração ^^
            return await _obraRepository.RetornaObraPorId(obraId);
        }

        public void AtualizaObraPorVolume(Novel obra, VolumeNovel volume)
        {
            var parametros = new
            {
                obra.Id,
                ImagemCapaUltimoVolume = volume.ImagemVolume,
                NumeroUltimoVolume = volume.DescritivoVolume,
                SlugUltimoVolume = volume.Slug
            };

            var sql = @"UPDATE Obra
                           SET ImagemCapaUltimoVolume = @ImagemCapaUltimoVolume,
                               NumeroUltimoVolume = @NumeroUltimoVolume,
                               SlugUltimoVolume = @SlugUltimoVolume
                         WHERE Id = @Id;";

            _contextDapper.Query(sql, parametros);
        }

        public async Task<VolumeNovel> RetornaVolumeExistente(int obraId, string numeroVolume)
        {
            var parametros = new
            {
                ObraId = obraId,
                NumeroVolume = numeroVolume
            };

            var sql = @"SELECT * 
                          FROM Volume 
                         WHERE ObraId = @ObraId 
                           AND Numero = @NumeroVolume";


            var volumeExistente = await _contextDapper.QueryAsync<VolumeNovel>(sql, parametros);
            return volumeExistente.FirstOrDefault();
        }
    }
}
