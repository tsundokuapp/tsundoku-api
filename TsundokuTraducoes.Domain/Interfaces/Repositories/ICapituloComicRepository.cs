using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface ICapituloComicRepository
    {
        Task<CapituloComic> ObterCapituloPorComicPorId(Guid idObra, Guid idCapitulo);
    }
}
