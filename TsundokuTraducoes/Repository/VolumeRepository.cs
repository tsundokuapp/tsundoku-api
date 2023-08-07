using Dapper;
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

        public List<Volume> RetornaListaVolumes()
        {
            var listaVolumes = new List<Volume>();
            _contextDapper.Query<Volume, CapituloNovel, Volume>(RetornaQueryListaVolumes(),
                (volume, capitulo) =>
                {
                    if (listaVolumes.SingleOrDefault(v => v.Id == volume.Id) == null)
                    {                        
                        listaVolumes.Add(volume);
                    }
                    else
                    {
                        volume = listaVolumes.SingleOrDefault(o => o.Id == volume.Id);
                    }

                    _obraRepository.CarregaCapitulos(listaVolumes, capitulo);
                    return volume;
                });

            return listaVolumes;
        }

        private string RetornaQueryListaVolumes()
        {
            return @"SELECT V.*, C.* 
                       FROM Volumes V
                       LEFT JOIN Capitulos C ON C.VolumeId = V.Id
                      ORDER BY V.Id, C.Id";
        }

        public Volume RetornaVolumePorId(int volumeId)
        {
            return RetornaListaVolumes().FirstOrDefault(f => f.Id == volumeId);
        }

        public Volume AtualizaVolume(VolumeDTO VolumeDTO)
        {
            var volumeEncontrado = _context.Volumes.SingleOrDefault(s => s.Id == VolumeDTO.Id);
            var tituloVolumeVazio = VerificaCampoVazio(volumeEncontrado.Titulo, VolumeDTO.Titulo);
            if (tituloVolumeVazio)
            {
                VolumeDTO.Titulo = string.Empty;
            }

            var sinopseVolumeVazia = VerificaCampoVazio(volumeEncontrado.Titulo, VolumeDTO.Titulo);
            if (sinopseVolumeVazia)
            {
                VolumeDTO.Sinopse = string.Empty;
            }

            if (string.IsNullOrEmpty(VolumeDTO.UrlImagemVolume))
            {
                VolumeDTO.UrlImagemVolume = volumeEncontrado.ImagemVolume;
            }

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

        public Obra RetornaObraPorId(int obraId)
        {
            var repositoriObra = new ObraRepository(_context);
            return repositoriObra.RetornaObraPorId(obraId);
        }

        public void AtualizaObraPorVolume(Obra obra, Volume volume)
        {
            var parametros = new
            {
                obra.Id,
                UrlImagemUltimoVolume = volume.ImagemVolume,
                NumeroUltimoVolume = volume.DescritivoVolume,
                SlugUltimoVolume = volume.Slug
            };

            var sql = @"update Obras
                           set UrlImagemUltimoVolume = @UrlImagemUltimoVolume,
                               NumeroUltimoVolume = @NumeroUltimoVolume,
                               SlugUltimoVolume = @SlugUltimoVolume
                         where Id = @Id;";

            _contextDapper.Query(sql, parametros);
        }
    }
}
