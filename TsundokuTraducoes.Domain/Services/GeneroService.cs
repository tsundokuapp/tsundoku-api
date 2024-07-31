using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<List<Genero>> RetornaListaGeneros()
        {
            return await _generoRepository.RetornaListaGeneros();
        }

        public async Task<Genero> RetornaGeneroPorId(Guid id)
        {
            return await _generoRepository.RetornaGeneroPorId(id);
        }

        public async Task<bool> AdicionaGenero(Genero genero)
        {
            await _generoRepository.AdicionaGenero(genero);
            return await AlteracoesSalvas();
        }

        public Genero AtualizaGenero(GeneroDTO generoDTO)
        {
            return _generoRepository.AtualizaGenero(generoDTO);            
        }

        public async Task<bool> ExcluiGenero(Genero genero)
        {
            _generoRepository.ExcluiGenero(genero);
            return await AlteracoesSalvas();
        }

        public async Task<Genero> RetornaGeneroExistente(string slugGenero)
        {
            return await _generoRepository.RetornaGeneroExistente(slugGenero);
        }

        public async Task<bool> AlteracoesSalvas()
        {
            return await _generoRepository.AlteracoesSalvas();
        }
    }
}