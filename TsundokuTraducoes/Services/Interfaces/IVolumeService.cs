using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IVolumeService
    {
        Task<Result<List<RetornoVolume>>> RetornaListaVolumes(Guid? idObra);
        
        Task<Result<RetornoVolume>> RetornaVolumeNovelPorId(Guid id);
        Task<Result<RetornoVolume>> RetornaVolumeComicPorId(Guid id);
        
        Task<Result<RetornoVolume>> AdicionaVolumeNovel(VolumeDTO volumeDTO);
        Task<Result<RetornoVolume>> AdicionaVolumeComic(VolumeDTO volumeDTO);
        
        Task<Result<RetornoVolume>> AtualizaVolumeNovel(VolumeDTO volumeDTO);
        Task<Result<RetornoVolume>> AtualizaVolumeComic(VolumeDTO volumeDTO);
        
        Task<Result<bool>> ExcluiVolumeNovel(Guid novelId);
        Task<Result<bool>> ExcluiVolumeComic(Guid comicId);
    }
}
