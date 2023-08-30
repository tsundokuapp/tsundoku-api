using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Volume;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IVolumeRepository
    {
        Task<List<VolumeNovel>> RetornaListaVolumesNovel(Guid? idObra);
        Task<List<VolumeComic>> RetornaListaVolumesComic(Guid? idObra);

        Task<VolumeNovel> RetornaVolumeNovelPorId(Guid volumeId);
        Task<VolumeComic> RetornaVolumeComicPorId(Guid volumeId);

        Task AdicionaVolumeNovel(VolumeNovel volumeNovel);
        Task AdicionaVolumeComic(VolumeComic volumeComic);
        
        VolumeNovel AtualizaVolumeNovel(VolumeDTO VolumeDTO);
        VolumeComic AtualizaVolumeComic(VolumeDTO VolumeDTO);
                
        void ExcluiVolumeNovel(VolumeNovel volumeNovel);
        void ExcluiVolumeComic(VolumeComic volumeComic);

        void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel);
        void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic);

        Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeDTO VolumeDTO);
        Task<VolumeComic> RetornaVolumeComicExistente(VolumeDTO VolumeDTO);

        Task<bool> AlteracoesSalvas();
    }
}
