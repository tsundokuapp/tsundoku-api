using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Domain.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly IVolumeRepository _volumeRepository;
        
        public VolumeService(IVolumeRepository volumeRepository)
        {
            _volumeRepository = volumeRepository;
        }

        public List<VolumeNovel> RetornaListaVolumesNovel(Guid? idObra)
        {
            return _volumeRepository.RetornaListaVolumesNovel(idObra);
        }

        public List<VolumeComic> RetornaListaVolumesComic(Guid? idObra)
        {
            return _volumeRepository.RetornaListaVolumesComic(idObra);
        }

        
        public VolumeNovel RetornaVolumeNovelPorId(Guid id)
        {
            return _volumeRepository.RetornaVolumeNovelPorId(id);
        }

        public VolumeComic RetornaVolumeComicPorId(Guid id)
        {
            return _volumeRepository.RetornaVolumeComicPorId(id);
        }


        public async Task<bool> AdicionaVolumeNovel(VolumeNovel volumeNovel)
        {
            _volumeRepository.AdicionaVolumeNovel(volumeNovel);
            return await _volumeRepository.AlteracoesSalvas();
        }

        public async Task<bool> AdicionaVolumeComic(VolumeComic volumeComic)
        {
            _volumeRepository.AdicionaVolumeComic(volumeComic);
            return await _volumeRepository.AlteracoesSalvas();
        }


        public VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO)
        {
            return _volumeRepository.AtualizaVolumeNovel(volumeDTO);
        }

        public VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO)
        {            
            return _volumeRepository.AtualizaVolumeComic(volumeDTO);
        }


        public async Task<bool> ExcluiVolumeNovel(VolumeNovel volumeNovel)
        {
            _volumeRepository.ExcluiVolumeNovel(volumeNovel);
            return await _volumeRepository.AlteracoesSalvas();
        }

        public async Task<bool> ExcluiVolumeComic(VolumeComic volumeComic)
        {
            _volumeRepository.ExcluiVolumeComic(volumeComic);
            return await _volumeRepository.AlteracoesSalvas();
        }

        public VolumeNovel RetornaVolumeNovelExistente(VolumeDTO VolumeDTO)
        {
            return _volumeRepository.RetornaVolumeNovelExistente(VolumeDTO);
        }

        public VolumeComic RetornaVolumeComicExistente(VolumeDTO VolumeDTO)
        {
            return _volumeRepository.RetornaVolumeComicExistente(VolumeDTO);
        }        

        public async Task<bool> AlteracoesSalvas()
        {
            return await _volumeRepository.AlteracoesSalvas();
        }
    }
}
