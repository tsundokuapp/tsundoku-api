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


        public bool AdicionaVolumeNovel(VolumeNovel volumeNovel)
        {
            _volumeRepository.AdicionaVolumeNovel(volumeNovel);
            return _volumeRepository.AlteracoesSalvass();
        }

        public bool AdicionaVolumeComic(VolumeComic volumeComic)
        {
            _volumeRepository.AdicionaVolumeComic(volumeComic);
            return _volumeRepository.AlteracoesSalvass();
        }


        public VolumeNovel AtualizaVolumeNovel(VolumeDTO volumeDTO)
        {
            return _volumeRepository.AtualizaVolumeNovel(volumeDTO);
        }

        public VolumeComic AtualizaVolumeComic(VolumeDTO volumeDTO)
        {            
            return _volumeRepository.AtualizaVolumeComic(volumeDTO);
        }


        public bool ExcluiVolumeNovel(VolumeNovel volumeNovel)
        {
            _volumeRepository.ExcluiVolumeNovel(volumeNovel);
            return _volumeRepository.AlteracoesSalvass();
        }

        public bool ExcluiVolumeComic(VolumeComic volumeComic)
        {
            _volumeRepository.ExcluiVolumeComic(volumeComic);
            return _volumeRepository.AlteracoesSalvass();
        }

        public async Task<VolumeNovel> RetornaVolumeNovelExistente(VolumeDTO VolumeDTO)
        {
            return await _volumeRepository.RetornaVolumeNovelExistente(VolumeDTO);
        }

        public async Task<VolumeComic> RetornaVolumeComicExistente(VolumeDTO VolumeDTO)
        {
            return await _volumeRepository.RetornaVolumeComicExistente(VolumeDTO);
        }        

        public bool AlteracoesSalvas()
        {
            return _volumeRepository.AlteracoesSalvass();
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
