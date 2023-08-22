using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Repository.Interfaces;
using System.Threading.Tasks;

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

        public List<Volume> RetornaListaVolumes(int? idObra = null)
        {
            return _contextDapper.Query<Volume>(RetornaQueryListaVolumes(idObra)).ToList();
        }

        private string RetornaQueryListaVolumes(int? idObra)
        {
            var condicao = (idObra != null && idObra > 0) ? $"WHERE ObraId = {idObra.Value}" : "";
            return $@"SELECT *
                       FROM Volume
                      {condicao}
                      ORDER BY Numero ASC";
        }

        public Volume RetornaVolumePorId(int volumeId)
        {
            return RetornaListaVolumes().FirstOrDefault(f => f.Id == volumeId);
        }

        public Volume AtualizaVolume(VolumeDTO VolumeDTO)
        {
            var volumeEncontrado = _context.Volume.SingleOrDefault(s => s.Id == VolumeDTO.Id);
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

        public async Task<Obra> RetornaObraPorId(int obraId)
        {
            var repositoriObra = new ObraRepository(_context);
            return await repositoriObra.RetornaObraPorId(obraId);
        }

        public void AtualizaObraPorVolume(Obra obra, Volume volume)
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

        public Volume RetornaVolumeExistente(int obraId, string numeroVolume)
        {
            var parametros = new
            {
                obraId = obraId,
                numeroVolume = numeroVolume
            };

            var sql = @"SELECT * 
                          FROM Volume 
                         WHERE ObraId = @obraId 
                           AND Numero = @numeroVolume";


            return _contextDapper.Query<Volume>(sql, parametros).FirstOrDefault();
        }
    }
}
