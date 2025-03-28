using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IVolumeComicRepository
    {
        Task<List<VolumeComic>> ObterVolumesComicPorIdObra(Guid idObra);
        Task<List<VolumeComic>> ObterVolumesComicPorSlugObra(string slugObra);
    }
}
