using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.Services.Interfaces;

namespace TsundokuTraducoes.Domain.Services
{
    public class CapituloNovelService(ICapituloNovelRepository repository) : ICapituloNovelService, IBaseService<ICapituloNovelRepository>
    {
        public async Task<List<CapituloNovel>> ObterCapitulosNovelPorIdObra(Guid idObra)
        {
            return await repository.ObterCapitulosNovelPorIdObra(idObra);
        }
    }
}
