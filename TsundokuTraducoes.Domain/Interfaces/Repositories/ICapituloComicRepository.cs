using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface ICapituloComicRepository
    {
        Task<List<CapituloComic>> ObterCapitulosPorComicPorIdObra(Guid idObra);
    }
}
