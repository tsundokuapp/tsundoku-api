using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Data.Repositories
{
    public class CapituloComicRepository : ICapituloComicRepository
    {
        public async Task<CapituloComic> ObterCapituloPorComicPorId(Guid idObra, Guid idCapitulo)
        {
            return new CapituloComic();
        }
    }
}
