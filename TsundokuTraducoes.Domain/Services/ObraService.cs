using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Services
{
    public class ObraService : IObraService
    {
        private readonly IObraRepository _obraRepository;
        
        public ObraService(IObraRepository obraRepository)
        {
            _obraRepository = obraRepository;
        }


        public async Task<List<Novel>> RetornaListaNovels()
        {
            return await _obraRepository.RetornaListaNovels();
        }
        
        public async Task<List<Comic>> RetornaListaComics()
        {
            return await _obraRepository.RetornaListaComics();
        }


        public async Task<Novel> RetornaNovelPorId(Guid id)
        {
            return await _obraRepository.RetornaNovelPorId(id);
        }
        
        public async Task<Comic> RetornaComicPorId(Guid id)
        {
            return await _obraRepository.RetornaComicPorId(id);
        }


        public async Task<bool> AdicionaNovel(Novel novel)
        {
            await _obraRepository.AdicionaNovel(novel);
            return await _obraRepository.AlteracoesSalvas();
        }

        public async Task<bool> AdicionaComic(Comic comic)
        {
            await _obraRepository.AdicionaComic(comic); 
            return await _obraRepository.AlteracoesSalvas();
        }


        public Novel AtualizaNovel(ObraDTO obraDTO)
        {
            return _obraRepository.AtualizaNovel(obraDTO);
        }

        public Comic AtualizaComic(ObraDTO obraDTO)
        {
            return _obraRepository.AtualizaComic(obraDTO);
        }


        public async Task<bool> ExcluiNovel(Novel novel)
        {
            _obraRepository.ExcluiNovel(novel);
            return await _obraRepository.AlteracoesSalvas();
        }

        public async Task<bool> ExcluiComic(Comic comic)
        {
            _obraRepository.ExcluiComic(comic);
            return await _obraRepository.AlteracoesSalvas();
        }


        public async Task InsereGenerosNovel(Novel novel, List<string> ListaGeneros, bool inclusao)
        {
            await _obraRepository.InsereGenerosNovel(novel, ListaGeneros, inclusao);
        }

        public async Task InsereGenerosComic(Comic comic, List<string> ListaGeneros, bool inclusao)
        {
            await _obraRepository.InsereGenerosComic(comic, ListaGeneros, inclusao);
        }


        public async Task<Novel> RetornaNovelExistente(string titulo)
        {
            return await _obraRepository.RetornaNovelExistente(titulo);
        }

        public async Task<Comic> RetornaComicExistente(string titulo)
        {
            return await _obraRepository.RetornaComicExistente(titulo);
        }

        public async Task<bool> AlteracoesSalvas()
        {
            return await  _obraRepository.AlteracoesSalvas();
        }
    }
}
