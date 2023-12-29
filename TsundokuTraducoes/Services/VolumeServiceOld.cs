using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Api.Services
{
    public class VolumeServiceOld : IVolumeServiceOld
    {
        private readonly IMapper _mapper;
        private readonly IVolumeRepositoryOld _volumeRepository;
        private readonly IObraRepositoryOld _obraRepository;
        private readonly IImagemServiceOld _imagemService;

        public VolumeServiceOld(IMapper mapper, IVolumeRepositoryOld repository, IObraRepositoryOld obraRepository, IImagemServiceOld imagemService)
        {
            _mapper = mapper;
            _volumeRepository = repository;
            _obraRepository = obraRepository;
            _imagemService = imagemService;
        }

        public async Task<Result<List<RetornoVolume>>> RetornaListaVolumes(Guid? idObra)
        {
            var listaVolumes = new List<RetornoVolume>();
            var listaVolumesNovel = await _volumeRepository.RetornaListaVolumesNovel(idObra);
            var listaVolumesComic = await _volumeRepository.RetornaListaVolumesComic(idObra);

            if (listaVolumesNovel.Count > 0)
            {
                foreach (var volume in listaVolumesNovel)
                {
                    listaVolumes.Add(TrataRetornoVolumeNovel(volume));
                }
            }

            if (listaVolumesComic.Count > 0)
            {
                foreach (var volume in listaVolumesComic)
                {
                    listaVolumes.Add(TrataRetornoVolumeComic(volume));
                }
            }

            return Result.Ok(listaVolumes);
        }


        public async Task<Result<RetornoVolume>> RetornaVolumeNovelPorId(Guid id)
        {
            var volume = await _volumeRepository.RetornaVolumeNovelPorId(id);
            if (volume == null)
                return Result.Fail("Volume não encontrado!");

            var retornoVolume = TrataRetornoVolumeNovel(volume);
            return Result.Ok(retornoVolume);
        }

        public async Task<Result<RetornoVolume>> RetornaVolumeComicPorId(Guid id)
        {
            var volume = await _volumeRepository.RetornaVolumeComicPorId(id);
            if (volume == null)
                return Result.Fail("Volume não encontrado!");

            var retornoVolume = TrataRetornoVolumeComic(volume);
            return Result.Ok(retornoVolume);
        }


        public async Task<Result<RetornoVolume>> AdicionaVolumeNovel(VolumeDTO volumeDTO)
        {
            var volume = _mapper.Map<VolumeNovel>(volumeDTO);
            var novel = await _obraRepository.RetornaNovelPorId(volumeDTO.ObraId);
            var volumeExistente = await _volumeRepository.RetornaVolumeNovelExistente(volumeDTO);

            if (novel == null)
                return Result.Fail("Não foi encontrada a obra informada");

            if (volumeExistente != null)
                return Result.Fail("Volume já postado!");

            if (volumeDTO.ImagemVolumeFile != null)
            {
                var result = _imagemService.ProcessaUploadCapaVolume(volumeDTO, volumeDTO.Numero, novel.DiretorioImagemObra);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                volume.DiretorioImagemVolume = volumeDTO.DiretorioImagemVolume;
                volume.ImagemVolume = volumeDTO.ImagemVolume;
            }
            else
            {
                return Result.Fail("Erro ao adicionar volume da obra, capa do volume não enviada!");
            }

            volume.DataInclusao = DateTime.Now;
            volume.DataAlteracao = volume.DataInclusao;
            await _volumeRepository.AdicionaVolumeNovel(volume);
            if (_volumeRepository.AlteracoesSalvas().Result)
            {
                _volumeRepository.AtualizaNovelPorVolume(novel, volume);
                var retornoVolume = TrataRetornoVolumeNovel(volume);
                return Result.Ok(retornoVolume);
            }

            return Result.Fail("Erro ao adicionar volume da obra!");
        }

        public async Task<Result<RetornoVolume>> AdicionaVolumeComic(VolumeDTO volumeDTO)
        {
            var volume = _mapper.Map<VolumeComic>(volumeDTO);
            var comic = await _obraRepository.RetornaComicPorId(volumeDTO.ObraId);
            var volumeExistente = await _volumeRepository.RetornaVolumeComicExistente(volumeDTO);

            if (comic == null)
                return Result.Fail("Não foi encontrada a obra informada");

            if (volumeExistente != null)
                return Result.Fail("Volume já postado!");

            if (volumeDTO.ImagemVolumeFile != null)
            {
                var result = _imagemService.ProcessaUploadCapaVolume(volumeDTO, volumeDTO.Numero, comic.DiretorioImagemObra);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                volume.DiretorioImagemVolume = volumeDTO.DiretorioImagemVolume;
                volume.ImagemVolume = volumeDTO.ImagemVolume;
            }
            else
            {
                return Result.Fail("Erro ao adicionar volume da obra, capa do volume não enviada!");
            }

            volume.DataInclusao = DateTime.Now;
            volume.DataAlteracao = volume.DataInclusao;
            await _volumeRepository.AdicionaVolumeComic(volume);
            if (_volumeRepository.AlteracoesSalvas().Result)
            {
                _volumeRepository.AtualizaComicPorVolume(comic, volume);
                var retornoVolume = TrataRetornoVolumeComic(volume);
                return Result.Ok(retornoVolume);
            }

            return Result.Fail("Erro ao adicionar volume da obra!");
        }


        public async Task<Result<RetornoVolume>> AtualizaVolumeNovel(VolumeDTO volumeDTO)
        {
            var volumeEncontrado = await _volumeRepository.RetornaVolumeNovelPorId(volumeDTO.Id);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            if (volumeDTO.ImagemVolumeFile != null)
            {
                var novel = await _obraRepository.RetornaNovelPorId(volumeDTO.ObraId);
                if (novel == null)
                    return Result.Fail("Não foi encontrada a obra informada");                

                var result = _imagemService.ProcessaUploadCapaVolume(volumeDTO, volumeDTO.Numero, novel.DiretorioImagemObra);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                volumeEncontrado.DiretorioImagemVolume = volumeDTO.DiretorioImagemVolume;
                volumeEncontrado.ImagemVolume = volumeDTO.ImagemVolume;
            }

            volumeEncontrado = _volumeRepository.AtualizaVolumeNovel(volumeDTO);
            if (!_volumeRepository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao atualizar o volume!");

            var retornoVolume = TrataRetornoVolumeNovel(volumeEncontrado);
            return Result.Ok(retornoVolume);
        }

        public async Task<Result<RetornoVolume>> AtualizaVolumeComic(VolumeDTO volumeDTO)
        {
            var volumeEncontrado = await _volumeRepository.RetornaVolumeComicPorId(volumeDTO.Id);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            if (volumeDTO.ImagemVolumeFile != null)
            {
                var comic = await _obraRepository.RetornaComicPorId(volumeDTO.ObraId);
                if (comic == null)
                    return Result.Fail("Não foi encontrada a obra informada");

                var result = _imagemService.ProcessaUploadCapaVolume(volumeDTO, volumeDTO.Numero, comic.DiretorioImagemObra);
                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                volumeEncontrado.DiretorioImagemVolume = volumeDTO.DiretorioImagemVolume;
                volumeEncontrado.ImagemVolume = volumeDTO.ImagemVolume;
            }

            volumeEncontrado = _volumeRepository.AtualizaVolumeComic(volumeDTO);
            if (!_volumeRepository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao atualizar o volume!");

            var retornoVolume = TrataRetornoVolumeComic(volumeEncontrado);
            return Result.Ok(retornoVolume);
        }


        public async Task<Result<bool>> ExcluiVolumeNovel(Guid novelId)
        {
            var volumeEncontrado = await _volumeRepository.RetornaVolumeNovelPorId(novelId);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            _volumeRepository.ExcluiVolumeNovel(volumeEncontrado);
            _imagemService.ExcluiDiretorioImagens(volumeEncontrado.DiretorioImagemVolume);            

            if (!_volumeRepository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao excluir o volume!");


            return Result.Ok().WithSuccess("Volume excluído com sucesso!");
        }

        public async Task<Result<bool>> ExcluiVolumeComic(Guid comicId)
        {
            var volumeEncontrado = await _volumeRepository.RetornaVolumeComicPorId(comicId);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            _volumeRepository.ExcluiVolumeComic(volumeEncontrado);
            _imagemService.ExcluiDiretorioImagens(volumeEncontrado.DiretorioImagemVolume);

            if (!_volumeRepository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao excluir o volume!");

            return Result.Ok().WithSuccess("Volume excluído com sucesso!");
        }


        private RetornoVolume TrataRetornoVolumeNovel(VolumeNovel volumeNovel)
        {
            var retornoVolume = _mapper.Map<RetornoVolume>(volumeNovel);
            retornoVolume.DataInclusao = volumeNovel.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.DataAlteracao = volumeNovel.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.UsuarioAlteracao = !string.IsNullOrEmpty(volumeNovel.UsuarioAlteracao) ? volumeNovel.UsuarioAlteracao : null;
            return retornoVolume;
        }

        private RetornoVolume TrataRetornoVolumeComic(VolumeComic volumeComic)
        {
            var retornoVolume = _mapper.Map<RetornoVolume>(volumeComic);
            retornoVolume.DataInclusao = volumeComic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.DataAlteracao = volumeComic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.UsuarioAlteracao = !string.IsNullOrEmpty(volumeComic.UsuarioAlteracao) ? volumeComic.UsuarioAlteracao : null;
            return retornoVolume;
        }
    }
}
