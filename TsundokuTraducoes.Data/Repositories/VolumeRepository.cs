using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Data.Repositories
{
    public class VolumeRepository : IVolumeRepository
    {
        public Task AdicionaVolumeComic(VolumeComic volumeComic)
        {
            throw new NotImplementedException();
        }

        public Task AdicionaVolumeNovel(VolumeNovel volumeNovel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AlteracoesSalvas()
        {
            throw new NotImplementedException();
        }

        public void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic)
        {
            throw new NotImplementedException();
        }

        public void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel)
        {
            throw new NotImplementedException();
        }

        public VolumeComic AtualizaVolumeComic(VolumeComic volumeComic)
        {
            throw new NotImplementedException();
        }

        public VolumeNovel AtualizaVolumeNovel(VolumeNovel volumeNovel)
        {
            throw new NotImplementedException();
        }

        public void ExcluiVolumeComic(VolumeComic volumeComic)
        {
            throw new NotImplementedException();
        }

        public void ExcluiVolumeNovel(VolumeNovel volumeNovel)
        {
            throw new NotImplementedException();
        }

        public Task<List<VolumeComic>> RetornaListaVolumesComic(Guid? idObra)
        {
            throw new NotImplementedException();
        }

        public Task<List<VolumeNovel>> RetornaListaVolumesNovel(Guid? idObra)
        {
            throw new NotImplementedException();
        }

        public Task<VolumeComic> RetornaVolumeComicExistente(VolumeComic volumeComic)
        {
            throw new NotImplementedException();
        }

        public Task<VolumeComic> RetornaVolumeComicPorId(Guid volumeId)
        {
            throw new NotImplementedException();
        }

        public Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeNovel volumeNovel)
        {
            throw new NotImplementedException();
        }

        public Task<VolumeNovel> RetornaVolumeNovelPorId(Guid volumeId)
        {
            throw new NotImplementedException();
        }
    }
}
