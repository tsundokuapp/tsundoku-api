using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Repositories
{
    public interface IGeneroRepository
    {
        Task<List<Genero>> RetornaListaGeneros();
        Task<Genero> RetornaGeneroPorId(Guid id);
        Task AdicionaGenero(Genero genero);
        Genero AtualizaGenero(GeneroDTO generoDTO);
        void ExcluiGenero(Genero genero);
        Task<bool> AlteracoesSalvas();
        Task<Genero> RetornaGeneroExistente(string slugGenero);
    }
}