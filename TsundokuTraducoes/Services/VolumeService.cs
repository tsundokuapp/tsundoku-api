using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.Repository;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly IMapper _mapper;
        private readonly IVolumeRepository _volumeRepository;
        private readonly IObraRepository _obraRepository;

        public VolumeService(IMapper mapper, IVolumeRepository repository, IObraRepository obraRepository)
        {
            _mapper = mapper;
            _volumeRepository = repository;
            _obraRepository = obraRepository;
        }

        public async Task<Result<List<RetornoVolume>>> RetornaListaVolume(int? idObra)
        {
            var listaRetornoVolumes = new List<RetornoVolume>();
            var listaVolumes = await _volumeRepository.RetornaListaVolumes(idObra);

            if (listaVolumes.Count > 0)
            {
                foreach (var volume in listaVolumes)
                {
                    listaRetornoVolumes.Add(TrataRetornoVolume(volume));
                }
            }

            return Result.Ok(listaRetornoVolumes);
        }

        public async Task<Result<RetornoVolume>> RetornaVolumePorId(int id)
        {
            var volume = await _volumeRepository.RetornaVolumePorId(id);
            if (volume == null)
                return Result.Fail("Volume não encontrado!");

            var retornoVolume = TrataRetornoVolume(volume);
            return Result.Ok(retornoVolume);
        }

        public async Task<Result<RetornoVolume>> AdicionaVolume(VolumeDTO volumeDTO)
        {
            var volume = _mapper.Map<Volume>(volumeDTO);
            var obra = await _obraRepository.RetornaObraPorId(volumeDTO.ObraId);
            var volumeExistente = await _volumeRepository.RetornaVolumeExistente(volumeDTO.ObraId, volumeDTO.Numero);

            if (obra == null)
                return Result.Fail("Não foi encontrada a obra informada");

            if (volumeExistente != null)
                return Result.Fail("Volume já postado!");

            if (volumeDTO.ImagemCapaVolumeFile != null)
            {
                var retornoProcessoImagem = new Imagens().ProcessaUploadImagemCapaVolume(volumeDTO.ImagemCapaVolumeFile, volume, obra, volumeDTO);
                if (retornoProcessoImagem.IsFailed)
                    return Result.Fail(retornoProcessoImagem.Errors[0].Message);
            }
            else
            {
                return Result.Fail("Erro ao adicionar volume da obra, imagem capa do volume não enviada!");
            }

            _volumeRepository.AdicionaVolume(volume);
            if (_volumeRepository.AlteracoesSalvas())
            {
                _volumeRepository.AtualizaObraPorVolume(obra, volume);
                var retornoVolume = TrataRetornoVolume(volume);
                return Result.Ok(retornoVolume);
            }

            return Result.Fail("Erro ao adicionar volume da obra!");
        }

        public async Task<Result<RetornoVolume>> AtualizaVolume(VolumeDTO volumeDTO)
        {
            var volumeEncontrado = await _volumeRepository.RetornaVolumePorId(volumeDTO.Id);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            if (volumeDTO.ImagemCapaVolumeFile != null)
            {
                var obra = await _obraRepository.RetornaObraPorId(volumeDTO.ObraId);
                if (obra == null)
                    return Result.Fail("Não foi encontrada a obra informada");

                var retornoProcessoImagem = new Imagens().ProcessaUploadImagemCapaVolume(volumeDTO.ImagemCapaVolumeFile, volumeEncontrado, obra, volumeDTO);
                if (retornoProcessoImagem.IsFailed)
                    return Result.Fail(retornoProcessoImagem.Errors[0].Message);
            }

            volumeEncontrado = _volumeRepository.AtualizaVolume(volumeDTO);
            if (!_volumeRepository.AlteracoesSalvas())
                return Result.Fail("Erro ao atualizar o volume!");

            var retornoVolume = TrataRetornoVolume(volumeEncontrado);
            return Result.Ok(retornoVolume);
        }

        public async Task<Result<bool>> ExcluiVolume(int id)
        {
            var volumeEncontrado = await _volumeRepository.RetornaVolumePorId(id);
            if (volumeEncontrado == null)
                return Result.Fail("Volume não encontrado!");

            _volumeRepository.ExcluiVolume(volumeEncontrado);
            if (_volumeRepository.AlteracoesSalvas())
                return Result.Ok().WithSuccess("Volume excluído com sucesso!");

            return Result.Fail("Erro ao excluir o volume!");
        }

        private RetornoVolume TrataRetornoVolume(Volume volume)
        {
            var retornoVolume = _mapper.Map<RetornoVolume>(volume);
            retornoVolume.DataCadastro = volume.DataCadastro.ToString("dd/MM/yyyy HH:mm:ss");
            retornoVolume.DataAlteracao = volume.DataAlteracao != null ? volume.DataAlteracao?.ToString("dd/MM/yyyy HH:mm:ss") : null;
            retornoVolume.UsuarioAlteracao = !string.IsNullOrEmpty(volume.UsuarioAlteracao) ? volume.UsuarioAlteracao : null;

            if (volume.ListaCapituloNovel.Count > 0)
            {
                retornoVolume.ListaCapituloNovel = new List<RetornoCapituloNovel>();
                foreach (var capituloNovel in volume.ListaCapituloNovel)
                {
                    retornoVolume.ListaCapituloNovel.Add(_mapper.Map<RetornoCapituloNovel>(capituloNovel));
                }
            }

            if (volume.ListaCapituloComic.Count > 0)
            {
                retornoVolume.ListaCapituloComic = new List<RetornoCapituloComic>();
                foreach (var capituloComic in volume.ListaCapituloComic)
                {
                    retornoVolume.ListaCapituloComic.Add(_mapper.Map<RetornoCapituloComic>(capituloComic));
                }
            }

            return retornoVolume;
        }
    }
}
