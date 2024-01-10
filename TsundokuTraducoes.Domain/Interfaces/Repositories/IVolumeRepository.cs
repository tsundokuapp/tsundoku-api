using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IVolumeRepository
    {
        List<VolumeNovel> RetornaListaVolumesNovel(Guid? idObra);
        List<VolumeComic> RetornaListaVolumesComic(Guid? idObra);

        VolumeNovel RetornaVolumeNovelPorId(Guid volumeId);
        VolumeComic RetornaVolumeComicPorId(Guid volumeId);

        void AdicionaVolumeNovel(VolumeNovel volumeNovel);
        void AdicionaVolumeComic(VolumeComic volumeComic);

        VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO);
        VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO);

        void ExcluiVolumeNovel(VolumeNovel volumeNovel);
        void ExcluiVolumeComic(VolumeComic volumeComic);

        void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel);
        void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic);

        VolumeNovel RetornaVolumeNovelExistente(VolumeDTO volumeDTO);
        VolumeComic RetornaVolumeComicExistente(VolumeDTO volumeDTO);

        bool AlteracoesSalvass();
    }
}
