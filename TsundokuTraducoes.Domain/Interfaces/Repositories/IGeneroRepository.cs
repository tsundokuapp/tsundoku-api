using TsundokuTraducoes.Entities.Entities.Generos;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IGeneroRepository
    {
        Task<List<Genero>> RetornaListaGeneros();
        Task AdicionaGenero(Genero genero);
        void ExcluiGenero(Genero genero);
        Task AtualizaGenero(Genero genero);
    }
}
