using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IVolumeNovelService
    {
        Task<List<VolumeNovel>> ObterVolumesNovelPorIdObra(Guid idObra);
        Task<List<VolumeNovel>> ObterVolumesNovelPorSlugObra(string slugObra);
    }
}
