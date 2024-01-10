using TsundokuTraducoes.Entities.Entities.Obra;
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

        bool AdicionaVolumeNovel(VolumeNovel volumeNovel);
        bool AdicionaVolumeComic(VolumeComic volumeComic);

        VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO);
        VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO);

        void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel);
        void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic);

        VolumeNovel RetornaVolumeNovelExistente(VolumeDTO VolumeDTO);
        VolumeComic RetornaVolumeComicExistente(VolumeDTO VolumeDTO);

        bool ExcluiVolumeNovel(VolumeNovel volumeNovel);
        bool ExcluiVolumeComic(VolumeComic volumeComic);

        bool AlteracoesSalvas();
    }
}
