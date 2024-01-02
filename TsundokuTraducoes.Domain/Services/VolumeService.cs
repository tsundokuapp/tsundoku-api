using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Obra;
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

        public async Task<List<VolumeNovel>> RetornaListaVolumesNovel(Guid? idObra)
        {
            return await _volumeRepository.RetornaListaVolumesNovel(idObra);
        }

        public async Task<List<VolumeComic>> RetornaListaVolumesComic(Guid? idObra)
        {
            return await _volumeRepository.RetornaListaVolumesComic(idObra);
        }

        
        public async Task<VolumeNovel> RetornaVolumeNovelPorId(Guid id)
        {
            return await _volumeRepository.RetornaVolumeNovelPorId(id);
        }

        public async Task<VolumeComic> RetornaVolumeComicPorId(Guid id)
        {
            return await _volumeRepository.RetornaVolumeComicPorId(id);
        }


        public async Task<bool> AdicionaVolumeNovel(VolumeNovel volumeNovel)
        {
            await _volumeRepository.AdicionaVolumeNovel(volumeNovel);
            return await _volumeRepository.AlteracoesSalvas();
        }

        public async Task<bool> AdicionaVolumeComic(VolumeComic volumeComic)
        {
            await _volumeRepository.AdicionaVolumeComic(volumeComic);
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

        public async Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeDTO VolumeDTO)
        {
            return await _volumeRepository.RetornaVolumeNovelExistente(VolumeDTO);
        }

        public async Task<VolumeComic> RetornaVolumeComicExistente(VolumeDTO VolumeDTO)
        {
            return await _volumeRepository.RetornaVolumeComicExistente(VolumeDTO);
        }        

        public async Task<bool> AlteracoesSalvas()
        {
            return await _volumeRepository.AlteracoesSalvas();
        }

        public void AtualizaNovelPorVolume(Novel novel, VolumeNovel volumeNovel)
        {
            _volumeRepository.AtualizaNovelPorVolume(novel, volumeNovel);
        }

        public void AtualizaComicPorVolume(Comic comic, VolumeComic volumeComic)
        {
            _volumeRepository.AtualizaComicPorVolume(comic, volumeComic);
        }
    }
}
