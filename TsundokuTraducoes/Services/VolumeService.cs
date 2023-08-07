using AutoMapper;
using FluentResults;
using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.Services
{
    public class VolumeService : IVolumeService
    {
        private readonly IMapper _mapper;
        private readonly IVolumeRepository _repository;

        public VolumeService(IMapper mapper, IVolumeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Result<List<Volume>> RetornaListaVolume(int? idObra)
        {
            var listaVolumes = _repository.RetornaListaVolumes(idObra);
            if (listaVolumes == null)
            {
                return Result.Fail("Erro ao retornar todos os volumes!");
            }

            return Result.Ok(listaVolumes);
        }

        public Result<Volume> RetornaVolumePorId(int id)
        {
            var volume = _repository.RetornaVolumePorId(id);
            if (volume == null)
            {
                return Result.Fail("Volume não encontrado!");
            }

            return Result.Ok(volume);
        }

        public Result<Volume> AdicionaVolume(VolumeDTO volumeDTO)
        {
            var volume = _mapper.Map<Volume>(volumeDTO);
            var obra = _repository.RetornaObraPorId(volumeDTO.ObraId);
            var volumeExistente = _repository.RetornaVolumeExistente(volumeDTO.ObraId, volumeDTO.Numero);

            if (obra == null)
            {
                return Result.Fail("Não foi encontrada a obra informada");
            }

            if (volumeExistente != null)
            {
                return Result.Fail("Voluma já postado.");
            }

            if (volumeDTO.ImagemCapaVolumeFile != null)
            {
                new Imagens().ProcessaUploadImagemCapaVolume(volumeDTO.ImagemCapaVolumeFile, volume, obra, volumeDTO);
            }
            else
            {
                return Result.Fail("Erro ao adicionar volume da obra, imagem capa do volume não enviada!");
            }

            _repository.Adiciona(volume);            
            if (_repository.AlteracoesSalvas())
            {
                _repository.AtualizaObraPorVolume(obra, volume);
                return Result.Ok(volume);
            }

            return Result.Fail("Erro ao adicionar volume da obra!");            
        }

        public Result<Volume> AtualizaVolume(VolumeDTO volumeDTO)
        {
            var volumeEncontrado = _repository.RetornaVolumePorId(volumeDTO.Id);
            if (volumeEncontrado == null)
            {
                return Result.Fail("Volume não encontrado!");
            }

            if (volumeDTO.ImagemCapaVolumeFile != null)
            {
                var obra = _repository.RetornaObraPorId(volumeDTO.ObraId);

                if (obra == null)
                    return Result.Fail("Não foi encontrada a obra informada");

                new Imagens().ProcessaUploadImagemCapaVolume(volumeDTO.ImagemCapaVolumeFile, volumeEncontrado, obra, volumeDTO);
            }

            volumeEncontrado = _repository.AtualizaVolume(volumeDTO);
            if (!_repository.AlteracoesSalvas())
            {
                return Result.Fail("Erro ao atualizar o volume!");
            }
                
            return Result.Ok(volumeEncontrado);
        }

        public Result<bool> ExcluirVolume(int id)
        {
            var volumeEncontrado = _repository.RetornaVolumePorId(id);
            if (volumeEncontrado == null)
            {
                return Result.Fail("Volume não encontrado!");
            }

            _repository.Exclui(volumeEncontrado);
            if (_repository.AlteracoesSalvas())
            {
                return Result.Ok().WithSuccess("Volume excluído com sucesso!");
            }

            return Result.Fail("Erro ao excluir o volume!");
        }
    }
}
