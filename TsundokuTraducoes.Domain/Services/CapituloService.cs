using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Services
{
    public class CapituloService : ICapituloService
    {
        private readonly ICapituloRepository _capituloRepository;

        public CapituloService(ICapituloRepository capituloRepository)
        {
            _capituloRepository = capituloRepository;
        }

        public List<CapituloNovel> RetornaListaCapitulosNovel(Guid? volumeId)
        {
            return _capituloRepository.RetornaListaCapitulosNovel(volumeId);
        }

        public List<CapituloComic> RetornaListaCapitulosComic(Guid? volumeId)
        {
            return _capituloRepository.RetornaListaCapitulosComic(volumeId);
        }


        public CapituloNovel RetornaCapituloNovelPorId(Guid capituloId)
        {
            return _capituloRepository.RetornaCapituloNovelPorId(capituloId);
        }

        public CapituloComic RetornaCapituloComicPorId(Guid capituloId)
        {
            return _capituloRepository.RetornaCapituloComicPorId(capituloId);
        }


        public async Task<bool> AdicionaCapituloNovel(CapituloNovel capituloNovel)
        {
            _capituloRepository.AdicionaCapituloNovel(capituloNovel);
            return await _capituloRepository.AlteracoesSalvas();
        }

        public async Task<bool> AdicionaCapituloComic(CapituloComic capituloComic)
        {
            _capituloRepository.AdicionaCapituloComic(capituloComic);
            return await _capituloRepository.AlteracoesSalvas();
        }

        
        public CapituloComic AtualizaCapituloComic(CapituloDTO capituloDTO)
        {
            return _capituloRepository.AtualizaCapituloComic(capituloDTO);
        }

        public CapituloNovel AtualizaCapituloNovel(CapituloDTO capituloDTO)
        {
            return _capituloRepository.AtualizaCapituloNovel(capituloDTO);
        }


        public async Task<bool> ExcluiCapituloNovel(CapituloNovel capituloNovel)
        {
            _capituloRepository.ExcluiCapituloNovel(capituloNovel);
            return await _capituloRepository.AlteracoesSalvas();
        }

        public async Task<bool> ExcluiCapituloComic(CapituloComic capituloComic)
        {
            _capituloRepository.ExcluiCapituloComic(capituloComic);
            return await _capituloRepository.AlteracoesSalvas();
        }


        public CapituloNovel RetornaCapituloNovelExistente(CapituloDTO capituloDTO)
        {
            return _capituloRepository.RetornaCapituloNovelExistente(capituloDTO);
        }

        public CapituloComic RetornaCapituloComicExistente(CapituloDTO capituloDTO)
        {
            return _capituloRepository.RetornaCapituloComicExistente(capituloDTO);
        }


        public async Task<bool> AlteracoesSalvas()
        {
            return await _capituloRepository.AlteracoesSalvas();
        }
    }
}
