using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IVolumeNovelRepository
    {
        Task<List<VolumeNovel>> ObterVolumesNovelPorIdObra(Guid idObra);
        Task<List<VolumeNovel>> ObterVolumesNovelPorSlugObra(string slugObra);
    }
}
