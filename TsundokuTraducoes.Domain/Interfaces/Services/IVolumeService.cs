using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IVolumeService
    {        
        Task<List<VolumeNovel>> RetornaListaVolumesNovel(Guid? idObra);
        Task<List<VolumeComic>> RetornaListaVolumesComic(Guid? idObra);

        Task<VolumeNovel> RetornaVolumeNovelPorId(Guid id);
        Task<VolumeComic> RetornaVolumeComicPorId(Guid id);

        Task<bool> AdicionaVolumeNovel(VolumeNovel volumeNovel);
        Task<bool> AdicionaVolumeComic(VolumeComic volumeComic);

        VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO);
        VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO);

        void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel);
        void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic);

        Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeDTO VolumeDTO);
        Task<VolumeComic> RetornaVolumeComicExistente(VolumeDTO VolumeDTO);

        Task<bool> ExcluiVolumeNovel(VolumeNovel volumeNovel);
        Task<bool> ExcluiVolumeComic(VolumeComic volumeComic);

        Task<bool> AlteracoesSalvas();
    }
}
