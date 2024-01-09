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

        bool AdicionaVolumeNovel(VolumeNovel volumeNovel);
        bool AdicionaVolumeComic(VolumeComic volumeComic);

        VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO);
        VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO);

        void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel);
        void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic);

        Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeDTO VolumeDTO);
        Task<VolumeComic> RetornaVolumeComicExistente(VolumeDTO VolumeDTO);

        bool ExcluiVolumeNovel(VolumeNovel volumeNovel);
        bool ExcluiVolumeComic(VolumeComic volumeComic);

        bool AlteracoesSalvas();
    }
}
