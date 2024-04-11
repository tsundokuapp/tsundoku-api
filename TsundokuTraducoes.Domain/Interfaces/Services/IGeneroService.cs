using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Interfaces.Services
{
    public interface IGeneroService
    {
        Task<List<Genero>> RetornaListaGeneros();
        Task<Genero> RetornaGeneroPorId(Guid id);
        Task<bool> AdicionaGenero(Genero genero);
        Genero AtualizaGenero(GeneroDTO generoDTO);
        Task<bool> ExcluiGenero(Genero genero);
        Task<bool> AlteracoesSalvas();
        Task<Genero> RetornaGeneroExistente(string slugGenero);
    }
}