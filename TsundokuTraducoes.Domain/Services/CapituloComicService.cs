using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.Services.Interfaces;

namespace TsundokuTraducoes.Domain.Services
{
    public class CapituloComicService(ICapituloComicRepository repository) : ICapituloComicService, IBaseService<ICapituloComicRepository>
    {
        public async Task<List<CapituloComic>> ObterCapitulosComicPorIdObra(Guid idObra)
        {
            return await repository.ObterCapitulosComicPorIdObra(idObra);
        }
    }
}
