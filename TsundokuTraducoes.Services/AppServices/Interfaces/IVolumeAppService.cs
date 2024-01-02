using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IVolumeAppService
    {
        Task<Result<List<RetornoVolume>>> RetornaListaVolumes(Guid? idObra);
        Task<Result<List<RetornoVolume>>> RetornaListaVolumesNovel(Guid? idObra);
        Task<Result<List<RetornoVolume>>> RetornaListaVolumesComic(Guid? idObra);

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
