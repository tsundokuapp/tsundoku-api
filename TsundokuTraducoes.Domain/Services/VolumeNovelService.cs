using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.Services.Interfaces;

namespace TsundokuTraducoes.Domain.Services
{
    public class VolumeNovelService(IVolumeNovelRepository repository) : IVolumeNovelService, IBaseService<IVolumeNovelRepository>
    {
        public async Task<List<VolumeNovel>> ObterVolumesNovelPorIdObra(Guid idObra)
        {
            return await repository.ObterVolumesNovelPorIdObra(idObra);
        }

        public async Task<List<VolumeNovel>> ObterVolumesNovelPorSlugObra(string slugObra)
        {
            return await repository.ObterVolumesNovelPorSlugObra(slugObra);
        }
    }
}
