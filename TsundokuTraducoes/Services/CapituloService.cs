using AutoMapper;
using FluentResults;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Utilidades;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Repository.Interfaces;
using System.Threading.Tasks;

namespace TsundokuTraducoes.Api.Services
{
    public class CapituloService : ICapituloService
    {
        private readonly IMapper _mapper;
        private readonly ICapituloRepository _capituloRepository;
        private readonly IVolumeRepository _volumeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CapituloService(ICapituloRepository repository, IMapper mapper, IWebHostEnvironment hostEnvironment, IVolumeRepository volumeRepository)
        {
            _mapper = mapper;
            _capituloRepository = repository;
            _webHostEnvironment = hostEnvironment;
            _volumeRepository = volumeRepository;
        }

        #region Comics

        public Result<CapituloComic> RetornaCapituloComicPorId(int capituloId)
        {
            var capitulo = _capituloRepository.RetornaCapituloComicPorId(capituloId);
            if (capitulo == null)
                return Result.Fail("Erro ao retornar o capitulo!");

            return Result.Ok(capitulo);
        }

        public async Task<Result<CapituloComic>> AdicionaCapituloComic(CapituloDTO capituloDTO)
        {
            var volume = await _volumeRepository.RetornaVolumePorId(capituloDTO.VolumeId);
            var obra = await _capituloRepository.RetornaObraPorId(volume.ObraId);

            var capituloExistente = _capituloRepository.RetornaCapituloComicExistente(volume.Id, capituloDTO);
            if (capituloExistente == null)
            {
                var capitulo = _mapper.Map<CapituloComic>(capituloDTO);
                _capituloRepository.Adiciona(capitulo);


                var ehComic = await VerificaEhComic(volume.ObraId);
                if (ehComic)
                {
                    if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                    {
                        var uploadImagemCapa = new Imagens(_webHostEnvironment);
                        var result = uploadImagemCapa.ProcessaListaUploadImagemPaginaCapitulo(capituloDTO, obra, volume, capitulo.Id);

                        if (result.IsFailed)
                            return Result.Fail(result.Errors[0].Message);

                        capitulo.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                        capitulo.ListaImagens = capituloDTO.ListaImagemCapitulo;
                    }
                    else
                    {
                        return Result.Fail("Não foi encontrada a lista de imagens para o capítulo.");
                    }
                }

                if (_capituloRepository.AlteracoesSalvas())
                {

                    _capituloRepository.AtualizaObraPorCapitulo(obra, capitulo.DescritivoCapitulo, capitulo.Slug, capitulo.DataInclusao);
                    return Result.Ok(capitulo);
                }

                return Result.Fail("Erro ao adicionar o capítulo!");
            }
            else
            {
                return Result.Fail("Capítulo já postado!");
            }
        }

        public async Task <Result<CapituloComic>> AtualizaCapituloComic(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _capituloRepository.RetornaCapituloComicPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var volume = await _volumeRepository.RetornaVolumePorId(capituloDTO.VolumeId);

            if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
            {
                var obra = await _capituloRepository.RetornaObraPorId(volume.ObraId);
                new Imagens().ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
                var result = new Imagens().ProcessaListaUploadImagemPaginaCapitulo(capituloDTO, obra, volume, capituloEncontrado.Id);

                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                capituloEncontrado.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                capituloEncontrado.ListaImagens = capituloDTO.ListaImagemCapitulo;
            }

            capituloEncontrado = _capituloRepository.AtualizaCapituloComic(capituloDTO);

            if (!_capituloRepository.AlteracoesSalvas())
                return Result.Fail("Erro ao atualizar o capítulo!");

            return Result.Ok(capituloEncontrado);

        }

        public Result<bool> ExcluiCapituloComic(int capituloId)
        {
            var capituloEncontrado = _capituloRepository.RetornaCapituloNovelPorId(capituloId);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            if (capituloEncontrado.EhIlustracoesNovel)
            {
                var imagens = new Imagens();
                imagens.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
            }

            _capituloRepository.Exclui(capituloEncontrado);
            if (!_capituloRepository.AlteracoesSalvas())
                return Result.Fail("Erro ao excluir o capítulo!");

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }

        #endregion

        #region Novels

        public Result<CapituloNovel> RetornaCapituloNovelPorId(int capituloId)
        {
            var capitulo = _capituloRepository.RetornaCapituloNovelPorId(capituloId);
            if (capitulo == null)
                return Result.Fail("Erro ao retornar o capitulo!");

            return Result.Ok(capitulo);
        }

        public async Task<Result<CapituloNovel>> AdicionaCapituloNovel(CapituloDTO capituloDTO)
        {
            var volume = await _volumeRepository.RetornaVolumePorId(capituloDTO.VolumeId);
            var obra = await _capituloRepository.RetornaObraPorId(volume.ObraId);

            var capituloExistente = _capituloRepository.RetornaCapituloNovelExistente(volume.Id, capituloDTO);
            if (capituloExistente == null)
            {
                var capitulo = _mapper.Map<CapituloNovel>(capituloDTO);
                _capituloRepository.Adiciona(capitulo);

                if (capituloDTO.EhIlustracoesNovel)
                {
                    if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                    {
                        var uploadImagemCapa = new Imagens(_webHostEnvironment);
                        var result = uploadImagemCapa.ProcessaListaUploadImagemPaginaCapitulo(capituloDTO, obra, volume, capitulo.Id);

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

                if (_capituloRepository.AlteracoesSalvas())
                {
                    _capituloRepository.AtualizaObraPorCapitulo(obra, capitulo.DescritivoCapitulo, capitulo.Slug, capitulo.DataInclusao);
                    return Result.Ok(capitulo);
                }

                return Result.Fail("Erro ao adicionar o capítulo!");
            }
            else
            {
                return Result.Fail("Capítulo já postado!");
            }
        }

        public async Task<Result<CapituloNovel>> AtualizaCapituloNovel(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _capituloRepository.RetornaCapituloNovelPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var volume = await _volumeRepository.RetornaVolumePorId(capituloDTO.VolumeId);

            if (capituloDTO.EhIlustracoesNovel)
            {
                if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                {

                    var obra = await _capituloRepository.RetornaObraPorId(volume.ObraId);
                    new Imagens().ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
                    var result = new Imagens().ProcessaListaUploadImagemPaginaCapitulo(capituloDTO, obra, volume, capituloEncontrado.Id);

                    if (result.IsFailed)
                        return Result.Fail(result.Errors[0].Message);

                    capituloEncontrado.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                    capituloEncontrado.ConteudoNovel = capituloDTO.ConteudoNovel;
                }
            }

            capituloEncontrado = _capituloRepository.AtualizaCapituloNovel(capituloDTO);

            if (!_capituloRepository.AlteracoesSalvas())
                return Result.Fail("Erro ao atualizar o capítulo!");

            return Result.Ok(capituloEncontrado);
        }

        public Result<bool> ExcluiCapituloNovel(int capituloId)
        {
            var capituloEncontrado = _capituloRepository.RetornaCapituloNovelPorId(capituloId);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            if (capituloEncontrado.EhIlustracoesNovel)
            {
                var imagens = new Imagens();
                imagens.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
            }

            _capituloRepository.Exclui(capituloEncontrado);
            if (!_capituloRepository.AlteracoesSalvas())
                return Result.Fail("Erro ao excluir o capítulo!");

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }

        #endregion

        public Result<List<CapituloDTO>> RetornaListaCapitulos()
        {
            var capitulos = _capituloRepository.RetornaListaCapitulos();
            if (capitulos == null) 
                return Result.Fail("Erro ao retornar todos os capitulos!");

            return Result.Ok(capitulos);
        }

        public async Task<Result<CapituloDTO>> RetornaDadosObra(int obraId)
        {
            var capituloDTO = new CapituloDTO();
            var obra = await _capituloRepository.RetornaObraPorId(obraId);

            if (obra == null)
                return Result.Fail("Obra consultada não encontrada!");

            capituloDTO.Obra = obra;
            return Result.Ok(capituloDTO);
        }

        public async Task<Result<CapituloDTO>> RetornaDadosCapitulo(int capituloId)
        {
            var capituloDTO = new CapituloDTO();
            var capituloNovel = _capituloRepository.RetornaCapituloNovelPorId(capituloId);
            var capituloComic = _capituloRepository.RetornaCapituloComicPorId(capituloId);

            int volumeId;
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

            var volume = await _volumeRepository.RetornaVolumePorId(capituloDTO.VolumeId);
            if (volume == null)
                return Result.Fail("Volume consultado não encontrada!");

            var obra = await _capituloRepository.RetornaObraPorId(volume.ObraId);
            if (obra == null)
                return Result.Fail("Obra consultada não encontrada!");

            AtualizaCapituloDTO(capituloDTO, obra, capituloNovel, capituloComic);
            return Result.Ok(capituloDTO);
        }

        public async Task<bool> VerificaEhComic(int obraId)
        {
            var obra = await _capituloRepository.RetornaObraPorId(obraId);
            return obra.TipoObraSlug == "manga" || obra.TipoObraSlug == "manhua" || obra.TipoObraSlug == "manhwa";
        }

        private void AtualizaCapituloDTO(CapituloDTO capituloDTO, Obra obra, CapituloNovel capituloNovel, CapituloComic capituloComic)
        {
            if (capituloNovel != null)
            {
                capituloDTO.Id = capituloNovel.Id;
                capituloDTO.Numero = capituloNovel.Numero;
                capituloDTO.Parte = !string.IsNullOrEmpty(capituloNovel.Parte) ? capituloNovel.Parte : string.Empty;
                capituloDTO.Titulo = !string.IsNullOrEmpty(capituloNovel.Titulo) ? capituloNovel.Titulo : string.Empty;
                capituloDTO.VolumeId = capituloNovel.VolumeId;
                capituloDTO.ConteudoNovel = !string.IsNullOrEmpty(capituloNovel.ConteudoNovel) ? capituloNovel.ConteudoNovel : string.Empty;
                capituloDTO.TituloObra = obra.Titulo;
                capituloDTO.TipoObraSlug = obra.TipoObraSlug;
                capituloDTO.ObraId = obra.Id;
                capituloDTO.UsuarioAlteracao = capituloNovel.UsuarioAlteracao;
                capituloDTO.UsuarioCadastro = capituloNovel.UsuarioCadastro;
                capituloDTO.Obra = obra;
                capituloDTO.EhIlustracoesNovel = capituloNovel.EhIlustracoesNovel;
            }
            else
            {
                capituloDTO.Id = capituloComic.Id;
                capituloDTO.Numero = capituloComic.Numero;
                capituloDTO.Parte = !string.IsNullOrEmpty(capituloComic.Parte) ? capituloComic.Parte : string.Empty;
                capituloDTO.Titulo = !string.IsNullOrEmpty(capituloComic.Titulo) ? capituloComic.Titulo : string.Empty;
                capituloDTO.VolumeId = capituloComic.VolumeId;
                capituloDTO.ListaImagemCapitulo = !string.IsNullOrEmpty(capituloComic.ListaImagens) ? capituloComic.ListaImagens : string.Empty;
                capituloDTO.TituloObra = obra.Titulo;
                capituloDTO.TipoObraSlug = obra.TipoObraSlug;
                capituloDTO.ObraId = obra.Id;
                capituloDTO.UsuarioAlteracao = capituloComic.UsuarioAlteracao;
                capituloDTO.UsuarioCadastro = capituloComic.UsuarioCadastro;
                capituloDTO.Obra = obra;
            }
        }
    }
}
