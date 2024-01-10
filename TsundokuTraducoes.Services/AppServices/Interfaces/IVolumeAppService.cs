using FluentResults;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Services.AppServices.Interfaces
{
    public interface IVolumeAppService
    {
        Result<List<RetornoVolume>> RetornaListaVolumes(Guid? idObra);
        Result<List<RetornoVolume>> RetornaListaVolumesNovel(Guid? idObra);
        Result<List<RetornoVolume>> RetornaListaVolumesComic(Guid? idObra);

        Result<RetornoVolume> RetornaVolumeNovelPorId(Guid id);
        Result<RetornoVolume> RetornaVolumeComicPorId(Guid id);

        Result<RetornoVolume> AdicionaVolumeNovel(VolumeDTO volumeDTO);
        Result<RetornoVolume> AdicionaVolumeComic(VolumeDTO volumeDTO);

        Result<RetornoVolume> AtualizaVolumeNovel(VolumeDTO volumeDTO);
        Result<RetornoVolume> AtualizaVolumeComic(VolumeDTO volumeDTO);

        Result<bool> ExcluiVolumeNovel(Guid novelId);
        Result<bool> ExcluiVolumeComic(Guid comicId);
    }
}
