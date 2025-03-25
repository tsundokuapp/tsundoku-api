using TsundokuTraducoes.Entities.Entities.Volume;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IVolumeComicService
    {
        Task<List<VolumeComic>> ObterVolumesComicPorIdObra(Guid idObra);
        Task<List<VolumeComic>> ObterVolumesComicPorSlugObra(string slugObra);
    }
}
