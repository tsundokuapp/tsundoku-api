using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface ICapituloComicRepository
    {
        Task<List<CapituloComic>> ObterCapitulosComicPorIdObra(Guid idObra);
        Task<List<CapituloComic>> ObterCapitulosComicPorSlugObra(string slugObra);
    }
}
