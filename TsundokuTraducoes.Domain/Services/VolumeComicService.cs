using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.Services.Interfaces;

namespace TsundokuTraducoes.Domain.Services
{
    public class VolumeComicService(IVolumeComicRepository repository) : IVolumeComicService, IBaseService<IVolumeComicRepository>
    {
        public async Task<List<VolumeComic>> ObterVolumesComicPorIdObra(Guid idObra)
        {
            return await repository.ObterVolumesComicPorIdObra(idObra);
        }

        public async Task<List<VolumeComic>> ObterVolumesComicPorSlugObra(string slugObra)
        {
            return await repository.ObterVolumesComicPorSlugObra(slugObra);
        }
    }
}
