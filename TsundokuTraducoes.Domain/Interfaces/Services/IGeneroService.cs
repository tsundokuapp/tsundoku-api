using TsundokuTraducoes.Entities.Entities.Generos;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IGeneroService
    {
        Task<List<Genero>> RetornaListaGeneros();
        Task AdicionaGenero(Genero genero);
        void ExcluiGenero(Genero genero);
        Task AtualizaGenero(Genero genero);
    }
}
