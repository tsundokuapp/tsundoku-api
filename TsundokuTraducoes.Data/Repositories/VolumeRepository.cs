using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Data.Repositories
{
    public class VolumeRepository : IVolumeRepository
    {
        private readonly ContextBase _context;
        
        public VolumeRepository(ContextBase context)
        {
            _context = context;
        }

        public List<VolumeNovel> RetornaListaVolumesNovel(Guid? novelId = null)
        {
            var listaVolumesNovel = novelId != null ? _context.VolumesNovel.AsNoTracking().Where(w => w.NovelId == novelId.Value) : _context.VolumesNovel;
            return listaVolumesNovel.ToList();
        }

        public List<VolumeComic> RetornaListaVolumesComic(Guid? comicId = null)
        {
            var listaVolumesNovel = comicId != null ? _context.VolumesComic.AsNoTracking().Where(w => w.ComicId == comicId.Value) : _context.VolumesComic;
            return listaVolumesNovel.ToList();
        }


        public VolumeNovel RetornaVolumeNovelPorId(Guid volumeId)
        {
            var volume = RetornaListaVolumesNovel();
            return volume.FirstOrDefault(f => f.Id == volumeId);
        }

        public VolumeComic RetornaVolumeComicPorId(Guid volumeId)
        {
            var volume = RetornaListaVolumesComic();
            return volume.FirstOrDefault(f => f.Id == volumeId);
        }       

        
        public void AdicionaVolumeNovel(VolumeNovel volumeNovel)
        {
            _context.Add(volumeNovel);
        }

        public void AdicionaVolumeComic(VolumeComic volumeComic)
        {
            _context.Add(volumeComic);
        }


        public VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO)
        {
            var volumeEncontrado = _context.VolumesNovel.SingleOrDefault(s => s.Id == volumeDTO.Id);
            var tituloVolumeVazio = VerificaCampoVazio(volumeEncontrado.Titulo, volumeDTO.Titulo);
            if (tituloVolumeVazio)
                volumeDTO.Titulo = string.Empty;

            var sinopseVolumeVazia = VerificaCampoVazio(volumeEncontrado.Sinopse, volumeDTO.Sinopse);
            if (sinopseVolumeVazia)
                volumeDTO.Sinopse = string.Empty;

            if (string.IsNullOrEmpty(volumeDTO.ImagemVolume))
                volumeDTO.ImagemVolume = volumeEncontrado.ImagemVolume;

            volumeDTO.DiretorioImagemVolume = volumeEncontrado.DiretorioImagemVolume;
            _context.Entry(volumeEncontrado).CurrentValues.SetValues(volumeDTO);
            volumeEncontrado.DataAlteracao = DateTime.Now;

            return volumeEncontrado;
        }

        public VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO)
        {
            var volumeEncontrado = _context.VolumesComic.SingleOrDefault(s => s.Id == volumeDTO.Id);
            var tituloVolumeVazio = VerificaCampoVazio(volumeEncontrado.Titulo, volumeDTO.Titulo);
            if (tituloVolumeVazio)
                volumeDTO.Titulo = string.Empty;

            var sinopseVolumeVazia = VerificaCampoVazio(volumeEncontrado.Sinopse, volumeDTO.Sinopse);
            if (sinopseVolumeVazia)
                volumeDTO.Sinopse = string.Empty;

            if (string.IsNullOrEmpty(volumeDTO.ImagemVolume))
                volumeDTO.ImagemVolume = volumeEncontrado.ImagemVolume;

            volumeDTO.DiretorioImagemVolume = volumeEncontrado.DiretorioImagemVolume;
            _context.Entry(volumeEncontrado).CurrentValues.SetValues(volumeDTO);
            volumeEncontrado.DataAlteracao = DateTime.Now;

            return volumeEncontrado;
        }


        public void ExcluiVolumeNovel(VolumeNovel volumeNovel)
        {
            _context.Remove(volumeNovel);
        }

        public void ExcluiVolumeComic(VolumeComic volumeComic)
        {
            _context.Remove(volumeComic);
        }

        public VolumeNovel RetornaVolumeNovelExistente(VolumeDTO volumeDTO)
        {
            var volumeExistente = _context.VolumesNovel
                .AsNoTracking()
                .Where(w => w.NovelId == volumeDTO.NovelId && (EF.Functions.Like(w.Numero, volumeDTO.Numero) || EF.Functions.Like(w.Slug, volumeDTO.Slug)));

            return volumeExistente.FirstOrDefault();
        }

        public VolumeComic RetornaVolumeComicExistente(VolumeDTO volumeDTO)
        {
            var volumeExistente = _context.VolumesComic
                .AsNoTracking()
                .Where(w => w.ComicId == volumeDTO.ComicId && (EF.Functions.Like(w.Numero, volumeDTO.Numero) || EF.Functions.Like(w.Slug, volumeDTO.Slug)));

            return volumeExistente.FirstOrDefault();
        }

        private static bool VerificaCampoVazio(string campoVolumeEncontrado, string campoVolumeDTO)
        {
            return string.IsNullOrEmpty(campoVolumeDTO) ||
               !string.IsNullOrEmpty(campoVolumeDTO) && campoVolumeDTO.ToLower().Contains("null") ||
               !string.IsNullOrEmpty(campoVolumeEncontrado) && campoVolumeEncontrado.ToLower().Contains("null");
        }

        public async Task<bool> AlteracoesSalvas()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
