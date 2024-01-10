using Dapper;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin;

#nullable disable

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
            object parametro = null;

            if (novelId != null)
            {
                parametro = new { NovelId = novelId.Value };
            }

            var listaVolumesNovel = _context.Connection.Query<VolumeNovel>(RetornaQueryListaVolumes(novelId), parametro);
            return listaVolumesNovel.ToList();
        }

        public List<VolumeComic> RetornaListaVolumesComic(Guid? comicId = null)
        {
            object parametro = null;

            if (comicId != null)
            {
                parametro = new { ComicId = comicId.Value };
            }

            var listaVolumesComic = _context.Connection.Query<VolumeComic>(RetornaQueryListaComics(comicId), parametro);
            return listaVolumesComic.ToList();
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

            _context.Connection.Query(sql, parametros);
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

            _context.Connection.Query(sql, parametros);
        }


        public VolumeNovel RetornaVolumeNovelExistente(VolumeDTO volumeDTO)
        {
            var parametros = new
            {
                volumeDTO.NovelId,
                volumeDTO.Numero,
                volumeDTO.Slug,
            };

            var sql = @"SELECT * 
                          FROM VolumesNovel
                         WHERE NovelId = @NovelId
                           AND (Numero LIKE @Numero OR Slug LIKE @Slug);";

            var volumeExistente = _context.Connection.Query<VolumeNovel>(sql, parametros);
            return volumeExistente.FirstOrDefault();
        }

        public VolumeComic RetornaVolumeComicExistente(VolumeDTO volumeDTO)
        {
            var parametros = new
            {
                volumeDTO.ComicId,
                volumeDTO.Numero,
                volumeDTO.Slug,
            };

            var sql = @"SELECT * 
                          FROM VolumesComic
                         WHERE ComicId = @ComicId
                           AND (Numero LIKE @Numero OR Slug LIKE @Slug);";

            var volumeExistente = _context.Connection.Query<VolumeComic>(sql, parametros);
            return volumeExistente.FirstOrDefault();
        }

        private static string RetornaQueryListaVolumes(Guid? novelId)
        {
            var condicao = (novelId != null) ? $"WHERE NovelId = @NovelId" : "";
            return $@"SELECT *
                       FROM VolumesNovel
                      {condicao}
                      ORDER BY Numero ASC";
        }

        private static string RetornaQueryListaComics(Guid? comicId)
        {
            var condicao = (comicId != null) ? $"WHERE ComicId = @ComicId" : "";
            return $@"SELECT *
                       FROM VolumesComic
                      {condicao}
                      ORDER BY Numero ASC";
        }

        private static bool VerificaCampoVazio(string campoVolumeEncontrado, string campoVolumeDTO)
        {
            return string.IsNullOrEmpty(campoVolumeDTO) ||
               !string.IsNullOrEmpty(campoVolumeDTO) && campoVolumeDTO.ToLower().Contains("null") ||
               !string.IsNullOrEmpty(campoVolumeEncontrado) && campoVolumeEncontrado.ToLower().Contains("null");
        }

        public bool AlteracoesSalvass()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
