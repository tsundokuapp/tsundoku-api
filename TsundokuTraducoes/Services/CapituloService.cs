using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Capitulo;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Api.Services
{
    public class CapituloService : ICapituloService
    {
        private readonly IMapper _mapper;
        private readonly IObraRepository _obraRepository;
        private readonly ICapituloRepository _capituloRepository;
        private readonly IVolumeRepository _volumeRepository;
        private readonly IImagemService _imagemService;

        public CapituloService(ICapituloRepository repository, IMapper mapper, IVolumeRepository volumeRepository, IObraRepository obraRepository, IImagemService imagemService)
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


        // TODO - Talvez seja descontinuado
        public async Task<Result<CapituloDTO>> RetornaDadosObra(Guid obraId)
        {
            var capituloDTO = new CapituloDTO();
            var obra = await _obraRepository.RetornaNovelPorId(obraId);

            if (obra == null)
                return Result.Fail("Obra consultada não encontrada!");

            capituloDTO.Obra = obra;
            return Result.Ok(capituloDTO);
        }

        public async Task<Result<CapituloDTO>> RetornaDadosCapitulo(Guid capituloId)
        {
            var capituloDTO = new CapituloDTO();
            var capituloNovel = await _capituloRepository.RetornaCapituloNovelPorId(capituloId);
            var capituloComic = await _capituloRepository.RetornaCapituloComicPorId(capituloId);

            Guid volumeId;
            if (capituloNovel != null)
            {
                volumeId = capituloNovel.VolumeId;

            }
            else
            {
                if (capituloComic != null)
                {
                    volumeId = capituloComic.VolumeId;
                }
                else
                {
                    return Result.Fail("Capítulo consultado não encontrada!");
                }
            }

            var volume = await _volumeRepository.RetornaVolumeNovelPorId(capituloDTO.VolumeId);
            if (volume == null)
                return Result.Fail("Volume consultado não encontrada!");

            var obra = await _obraRepository.RetornaNovelPorId(volume.NovelId);
            if (obra == null)
                return Result.Fail("Obra consultada não encontrada!");

            AtualizaCapituloDTO(capituloDTO, obra, capituloNovel, capituloComic);
            return Result.Ok(capituloDTO);
        }

        private void AtualizaCapituloDTO(CapituloDTO capituloDTO, Novel obra, CapituloNovel capituloNovel, CapituloComic capituloComic)
        {
            if (capituloNovel != null)
            {
                capituloDTO.Id = capituloNovel.Id;
                capituloDTO.Numero = capituloNovel.Numero;
                //capituloDTO.Parte = !string.IsNullOrEmpty(capituloNovel.Parte) ? capituloNovel.Parte : string.Empty;
                capituloDTO.Titulo = !string.IsNullOrEmpty(capituloNovel.Titulo) ? capituloNovel.Titulo : string.Empty;
                capituloDTO.VolumeId = capituloNovel.VolumeId;
                capituloDTO.ConteudoNovel = !string.IsNullOrEmpty(capituloNovel.ConteudoNovel) ? capituloNovel.ConteudoNovel : string.Empty;
                capituloDTO.TituloObra = obra.Titulo;
                capituloDTO.TipoObraSlug = obra.TipoObraSlug;
                capituloDTO.ObraId = obra.Id;
                capituloDTO.UsuarioAlteracao = capituloNovel.UsuarioAlteracao;
                capituloDTO.UsuarioInclusao = capituloNovel.UsuarioInclusao;
                capituloDTO.Obra = obra;
                capituloDTO.EhIlustracoesNovel = capituloNovel.EhIlustracoesNovel;
            }
            else
            {
                capituloDTO.Id = capituloComic.Id;
                capituloDTO.Numero = capituloComic.Numero;
                //capituloDTO.Parte = !string.IsNullOrEmpty(capituloComic.Parte) ? capituloComic.Parte : string.Empty;
                capituloDTO.Titulo = !string.IsNullOrEmpty(capituloComic.Titulo) ? capituloComic.Titulo : string.Empty;
                capituloDTO.VolumeId = capituloComic.VolumeId;
                capituloDTO.ListaImagemCapitulo = !string.IsNullOrEmpty(capituloComic.ListaImagens) ? capituloComic.ListaImagens : string.Empty;
                capituloDTO.TituloObra = obra.Titulo;
                capituloDTO.TipoObraSlug = obra.TipoObraSlug;
                capituloDTO.ObraId = obra.Id;
                capituloDTO.UsuarioAlteracao = capituloComic.UsuarioAlteracao;
                capituloDTO.UsuarioInclusao = capituloComic.UsuarioInclusao;
                capituloDTO.Obra = obra;
            }
        }
    }
}