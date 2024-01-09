using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IVolumeRepository
    {
        Task<List<VolumeNovel>> RetornaListaVolumesNovel(Guid? idObra);
        Task<List<VolumeComic>> RetornaListaVolumesComic(Guid? idObra);

        Task<VolumeNovel> RetornaVolumeNovelPorId(Guid volumeId);
        Task<VolumeComic> RetornaVolumeComicPorId(Guid volumeId);

        Task AdicionaVolumeNovel(VolumeNovel volumeNovel);
        Task AdicionaVolumeComic(VolumeComic volumeComic);

        VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO);
        VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO);

        void ExcluiVolumeNovel(VolumeNovel volumeNovel);
        void ExcluiVolumeComic(VolumeComic volumeComic);

        void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel);
        void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic);

        Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeDTO volumeDTO);
        Task<VolumeComic> RetornaVolumeComicExistente(VolumeDTO volumeDTO);

        Task<bool> AlteracoesSalvas();

        bool AlteracoesSalvass();
    }
}
