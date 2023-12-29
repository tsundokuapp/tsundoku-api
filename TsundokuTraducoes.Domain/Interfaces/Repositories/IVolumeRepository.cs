using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;

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

        VolumeNovel AtualizaVolumeNovel(VolumeNovel volumeNovel);
        VolumeComic AtualizaVolumeComic(VolumeComic volumeComic);

        void ExcluiVolumeNovel(VolumeNovel volumeNovel);
        void ExcluiVolumeComic(VolumeComic volumeComic);

        void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel);
        void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic);

        Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeNovel volumeNovel);
        Task<VolumeComic> RetornaVolumeComicExistente(VolumeComic volumeComic);

        Task<bool> AlteracoesSalvas();
    }
}
