using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface ICapituloComicService
    {
        Task<CapituloComic> ObterCapituloPorComicPorId(Guid idObra, Guid idCapitulo);
    }
}
