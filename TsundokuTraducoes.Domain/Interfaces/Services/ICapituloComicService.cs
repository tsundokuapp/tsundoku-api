using TsundokuTraducoes.Entities.Entities.Capitulo;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface ICapituloComicService
    {
        Task<List<CapituloComic>> ObterCapitulosComicPorIdObra(Guid idObra);
    }
}
