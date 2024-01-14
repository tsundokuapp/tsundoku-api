using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IVolumeService
    {        
        List<VolumeNovel> RetornaListaVolumesNovel(Guid? idObra);
        List<VolumeComic> RetornaListaVolumesComic(Guid? idObra);

        VolumeNovel RetornaVolumeNovelPorId(Guid id);
        VolumeComic RetornaVolumeComicPorId(Guid id);

        Task<bool> AdicionaVolumeNovel(VolumeNovel volumeNovel);
        Task<bool> AdicionaVolumeComic(VolumeComic volumeComic);

        VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO);
        VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO);

        VolumeNovel RetornaVolumeNovelExistente(VolumeDTO VolumeDTO);
        VolumeComic RetornaVolumeComicExistente(VolumeDTO VolumeDTO);

        Task<bool> ExcluiVolumeNovel(VolumeNovel volumeNovel);
        Task<bool> ExcluiVolumeComic(VolumeComic volumeComic);

        Task<bool> AlteracoesSalvas();
    }
}
