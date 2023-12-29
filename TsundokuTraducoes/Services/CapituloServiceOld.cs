using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Api.Services
{
    public class CapituloServiceOld : ICapituloServiceOld
    {
        private readonly IMapper _mapper;
        private readonly IObraRepositoryOld _obraRepository;
        private readonly ICapituloRepositoryOld _capituloRepository;
        private readonly IVolumeRepositoryOld _volumeRepository;
        private readonly IImagemServiceOld _imagemService;

        public CapituloServiceOld(ICapituloRepositoryOld repository, IMapper mapper, IVolumeRepositoryOld volumeRepository, IObraRepositoryOld obraRepository, IImagemServiceOld imagemService)
        {
            _mapper = mapper;
            _capituloRepository = repository;
            _volumeRepository = volumeRepository;
            _obraRepository = obraRepository;
            _imagemService = imagemService;
        }

        public async Task<Result<List<RetornoCapitulo>>> RetornaListaCapitulos(Guid? volumeId = null)
        {
            var listaCapitulos = new List<RetornoCapitulo>();
            var capitulosNovel = await _capituloRepository.RetornaListaCapitulosNovel(volumeId);
            var capitulosComic = await _capituloRepository.RetornaListaCapitulosComic(volumeId);
            
            if (capitulosNovel.Count > 0)
            {
                foreach (var capitulo in capitulosNovel)
                {
                    listaCapitulos.Add(TrataRetornoCapituloNovel(capitulo));
                }
            }

            if (capitulosComic.Count > 0)
            {
                foreach (var capitulo in capitulosComic)
                {
                    listaCapitulos.Add(TrataRetornoCapituloComic(capitulo));
                }
            }

            return Result.Ok(listaCapitulos);
        }
        
        
        public async Task<Result<CapituloNovel>> RetornaCapituloNovelPorId(Guid capituloId)
        {
            var capitulo = await _capituloRepository.RetornaCapituloNovelPorId(capituloId);
            if (capitulo == null)
                return Result.Fail("Capitulo não encontrado!");

            return Result.Ok(capitulo);
        }
        
        public async Task<Result<CapituloComic>> RetornaCapituloComicPorId(Guid capituloId)
        {
            var capitulo = await _capituloRepository.RetornaCapituloComicPorId(capituloId);
            if (capitulo == null)
                return Result.Fail("Capitulo não encontrado!");

            return Result.Ok(capitulo);
        }

        
        public async Task<Result<RetornoCapitulo>> AdicionaCapituloNovel(CapituloDTO capituloDTO)
        {
            var volume = await _volumeRepository.RetornaVolumeNovelPorId(capituloDTO.VolumeId);
            var obra = await _obraRepository.RetornaNovelPorId(volume.NovelId);

            var capituloExistente = await _capituloRepository.RetornaCapituloNovelExistente(capituloDTO);
            if (capituloExistente == null)
            {
                var capitulo = _mapper.Map<CapituloNovel>(capituloDTO);
                capitulo.DataInclusao = DateTime.Now;
                capitulo.DataAlteracao = capitulo.DataInclusao;
                await _capituloRepository.AdicionaCapituloNovel(capitulo);

                if (capituloDTO.EhIlustracoesNovel)
                {
                    if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                    {                        
                        var result = _imagemService.ProcessaUploadListaImagensCapituloNovel(capituloDTO, volume);
                        if (result.IsFailed)
                            return Result.Fail(result.Errors[0].Message);

                        capitulo.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                        capitulo.ConteudoNovel = capituloDTO.ConteudoNovel;
                    }
                    else
                    {
                        return Result.Fail("Não foi encontrada a lista de imagens para o capítulo");
                    }
                }

                if (!_capituloRepository.AlteracoesSalvas().Result)
                {
                    return Result.Fail("Erro ao adicionar o capítulo!");                    
                }

                _capituloRepository.AtualizaNovelPorCapitulo(obra, capitulo);

                var retornoCapitulo = TrataRetornoCapituloNovel(capitulo);
                return Result.Ok(retornoCapitulo);
            }
            else
            {
                return Result.Fail("Capítulo já postado!");
            }
        }
        
        public async Task<Result<RetornoCapitulo>> AdicionaCapituloComic(CapituloDTO capituloDTO)
        {
            var volume = await _volumeRepository.RetornaVolumeComicPorId(capituloDTO.VolumeId);
            var comic = await _obraRepository.RetornaComicPorId(volume.ComicId);

            var capituloExistente = await _capituloRepository.RetornaCapituloComicExistente(capituloDTO);
            if (capituloExistente == null)
            {
                var capitulo = _mapper.Map<CapituloComic>(capituloDTO);
                capitulo.DataInclusao = DateTime.Now;
                capitulo.DataAlteracao = capitulo.DataInclusao;
                await _capituloRepository.AdicionaCapituloComic(capitulo);

                if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                {                    
                    var result = _imagemService.ProcessaUploadListaImagensCapituloManga(capituloDTO, volume);
                    if (result.IsFailed)
                        return Result.Fail(result.Errors[0].Message);

                    capitulo.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                    capitulo.ListaImagens = capituloDTO.ListaImagemCapitulo;
                }
                else
                {
                    return Result.Fail("Não foi encontrada a lista de imagens para o capítulo.");
                }

                if (!_capituloRepository.AlteracoesSalvas().Result)
                {
                    return Result.Fail("Erro ao adicionar o capítulo!");
                }

                _capituloRepository.AtualizaComicPorCapitulo(comic, capitulo);

                var retornoCapitulo = TrataRetornoCapituloComic(capitulo);
                return Result.Ok(retornoCapitulo);
            }
            else
            {
                return Result.Fail("Capítulo já postado!");
            }
        }

        
        public async Task<Result<RetornoCapitulo>> AtualizaCapituloNovel(CapituloDTO capituloDTO)
        {            
            var capituloEncontrado = await _capituloRepository.RetornaCapituloNovelPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var volume = await _volumeRepository.RetornaVolumeNovelPorId(capituloDTO.VolumeId);

            if (capituloDTO.EhIlustracoesNovel)
            {
                if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                {
                    _imagemService.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
                    var result = _imagemService.ProcessaUploadListaImagensCapituloNovel(capituloDTO, volume);

                    if (result.IsFailed)
                        return Result.Fail(result.Errors[0].Message);

                    capituloEncontrado.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                    capituloEncontrado.ConteudoNovel = capituloDTO.ConteudoNovel;
                }
            }

            capituloEncontrado = await _capituloRepository.AtualizaCapituloNovel(capituloDTO);

            if (!_capituloRepository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao atualizar o capítulo!");

            var retornoCapitulo = TrataRetornoCapituloNovel(capituloEncontrado);
            return Result.Ok(retornoCapitulo);
        }
        
        public async Task<Result<RetornoCapitulo>> AtualizaCapituloComic(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = await _capituloRepository.RetornaCapituloComicPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var volume = await _volumeRepository.RetornaVolumeComicPorId(capituloDTO.VolumeId);

            if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
            {
                _imagemService.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
                var result = _imagemService.ProcessaUploadListaImagensCapituloManga(capituloDTO,volume);

                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                capituloEncontrado.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                capituloEncontrado.ListaImagens = capituloDTO.ListaImagemCapitulo;
            }

            capituloEncontrado = await _capituloRepository.AtualizaCapituloComic(capituloDTO);

            if (!_capituloRepository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao atualizar o capítulo!");

            var retornoCapitulo = TrataRetornoCapituloComic(capituloEncontrado);
            return Result.Ok(retornoCapitulo);
        }

        
        public async Task<Result> ExcluiCapituloNovel(Guid capituloId)
        {
            var capituloEncontrado = await _capituloRepository.RetornaCapituloNovelPorId(capituloId);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            _capituloRepository.ExcluiCapituloNovel(capituloEncontrado);
            
            if (capituloEncontrado.EhIlustracoesNovel)
            {
                _imagemService.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
            }

            if (!_capituloRepository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao excluir o capítulo!");

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }
        
        public async Task<Result> ExcluiCapituloComic(Guid capituloId)
        {
            var capituloEncontrado = await _capituloRepository.RetornaCapituloComicPorId(capituloId);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            _capituloRepository.ExcluiCapituloComic(capituloEncontrado);
            _imagemService.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);

            if (!_capituloRepository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao excluir o capítulo!");

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }


        private RetornoCapitulo TrataRetornoCapituloNovel(CapituloNovel capituloNovel)
        {            
            var retornoCapitulo = _mapper.Map<RetornoCapitulo>(capituloNovel);
            retornoCapitulo.DataInclusao = capituloNovel.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = capituloNovel.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(capituloNovel.UsuarioAlteracao) ? capituloNovel.UsuarioAlteracao : null;
            return retornoCapitulo;
        }

        private RetornoCapitulo TrataRetornoCapituloComic(CapituloComic CapituloComic)
        {
            var retornoCapitulo = _mapper.Map<RetornoCapitulo>(CapituloComic);
            retornoCapitulo.DataInclusao = CapituloComic.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.DataAlteracao = CapituloComic.DataAlteracao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoCapitulo.UsuarioAlteracao = !string.IsNullOrEmpty(CapituloComic.UsuarioAlteracao) ? CapituloComic.UsuarioAlteracao : null;
            return retornoCapitulo;
        }
    }
}