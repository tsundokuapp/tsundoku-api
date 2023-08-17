using AutoMapper;
using FluentResults;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Utilidades;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Repository.Interfaces;

namespace TsundokuTraducoes.Api.Services
{
    public class CapituloService : ICapituloService
    {
        private readonly IMapper _mapper;
        private readonly ICapituloRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CapituloService(ICapituloRepository repository, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _mapper = mapper;
            _repository = repository;
            _webHostEnvironment = hostEnvironment;
        }

        #region Comics

        public Result<CapituloComic> RetornaCapituloComicPorId(int capituloId)
        {
            var capitulo = _repository.RetornaCapituloComicPorId(capituloId);
            if (capitulo == null)
                return Result.Fail("Erro ao retornar o capitulo!");

            return Result.Ok(capitulo);
        }

        public Result<CapituloComic> AdicionaCapituloComic(CapituloDTO capituloDTO)
        {
            var volume = _repository.RetornaVolumePorId(capituloDTO.VolumeId);
            var obra = _repository.RetornaObraPorId(volume.ObraId);

            var capituloExistente = _repository.RetornaCapituloComicExistente(volume.Id, capituloDTO);
            if (capituloExistente == null)
            {
                var capitulo = _mapper.Map<CapituloComic>(capituloDTO);
                _repository.Adiciona(capitulo);


                var ehComic = VerificaEhComic(volume.ObraId);
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

                if (_repository.AlteracoesSalvas())
                {

                    _repository.AtualizaObraPorCapitulo(obra, capitulo.DescritivoCapitulo, capitulo.Slug, capitulo.DataInclusao);
                    return Result.Ok(capitulo);
                }

                return Result.Fail("Erro ao adicionar o capítulo!");
            }
            else
            {
                return Result.Fail("Capítulo já postado!");
            }
        }

        public Result<CapituloComic> AtualizaCapituloComic(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _repository.RetornaCapituloComicPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var volume = _repository.RetornaVolumePorId(capituloDTO.VolumeId);

            if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
            {
                var obra = _repository.RetornaObraPorId(volume.ObraId);
                new Imagens().ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
                var result = new Imagens().ProcessaListaUploadImagemPaginaCapitulo(capituloDTO, obra, volume, capituloEncontrado.Id);

                if (result.IsFailed)
                    return Result.Fail(result.Errors[0].Message);

                capituloEncontrado.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                capituloEncontrado.ListaImagens = capituloDTO.ListaImagemCapitulo;
            }

            capituloEncontrado = _repository.AtualizaCapituloComic(capituloDTO);

            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao atualizar o capítulo!");

            return Result.Ok(capituloEncontrado);

        }

        public Result<bool> ExcluiCapituloComic(int capituloId)
        {
            var capituloEncontrado = _repository.RetornaCapituloNovelPorId(capituloId);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            if (capituloEncontrado.EhIlustracoesNovel)
            {
                var imagens = new Imagens();
                imagens.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
            }

            _repository.Exclui(capituloEncontrado);
            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao excluir o capítulo!");

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }

        #endregion

        #region Novels

        public Result<CapituloNovel> RetornaCapituloNovelPorId(int capituloId)
        {
            var capitulo = _repository.RetornaCapituloNovelPorId(capituloId);
            if (capitulo == null)
                return Result.Fail("Erro ao retornar o capitulo!");

            return Result.Ok(capitulo);
        }

        public Result<CapituloNovel> AdicionaCapituloNovel(CapituloDTO capituloDTO)
        {
            var volume = _repository.RetornaVolumePorId(capituloDTO.VolumeId);
            var obra = _repository.RetornaObraPorId(volume.ObraId);

            var capituloExistente = _repository.RetornaCapituloNovelExistente(volume.Id, capituloDTO);
            if (capituloExistente == null)
            {
                var capitulo = _mapper.Map<CapituloNovel>(capituloDTO);
                _repository.Adiciona(capitulo);

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

                if (_repository.AlteracoesSalvas())
                {
                    _repository.AtualizaObraPorCapitulo(obra, capitulo.DescritivoCapitulo, capitulo.Slug, capitulo.DataInclusao);
                    return Result.Ok(capitulo);
                }

                return Result.Fail("Erro ao adicionar o capítulo!");
            }
            else
            {
                return Result.Fail("Capítulo já postado!");
            }
        }

        public Result<CapituloNovel> AtualizaCapituloNovel(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _repository.RetornaCapituloNovelPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            var volume = _repository.RetornaVolumePorId(capituloDTO.VolumeId);

            if (capituloDTO.EhIlustracoesNovel)
            {
                if (capituloDTO.ListaImagensForm != null && capituloDTO.ListaImagensForm.Count > 0)
                {

                    var obra = _repository.RetornaObraPorId(volume.ObraId);
                    new Imagens().ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
                    var result = new Imagens().ProcessaListaUploadImagemPaginaCapitulo(capituloDTO, obra, volume, capituloEncontrado.Id);

                    if (result.IsFailed)
                        return Result.Fail(result.Errors[0].Message);

                    capituloEncontrado.DiretorioImagemCapitulo = capituloDTO.DiretorioImagemCapitulo;
                    capituloEncontrado.ConteudoNovel = capituloDTO.ConteudoNovel;
                }
            }

            capituloEncontrado = _repository.AtualizaCapituloNovel(capituloDTO);

            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao atualizar o capítulo!");

            return Result.Ok(capituloEncontrado);
        }

        public Result<bool> ExcluiCapituloNovel(int capituloId)
        {
            var capituloEncontrado = _repository.RetornaCapituloNovelPorId(capituloId);
            if (capituloEncontrado == null)
                return Result.Fail("Capítulo não encontrado!");

            if (capituloEncontrado.EhIlustracoesNovel)
            {
                var imagens = new Imagens();
                imagens.ExcluiDiretorioImagens(capituloEncontrado.DiretorioImagemCapitulo);
            }

            _repository.Exclui(capituloEncontrado);
            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao excluir o capítulo!");

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }

        #endregion

        public Result<List<CapituloDTO>> RetornaListaCapitulos()
        {
            var capitulos = _repository.RetornaListaCapitulos();
            if (capitulos == null) 
                return Result.Fail("Erro ao retornar todos os capitulos!");

            return Result.Ok(capitulos);
        }

        public Result<CapituloDTO> RetornaDadosObra(int obraId)
        {
            var capituloDTO = new CapituloDTO();
            var obra = _repository.RetornaObraPorId(obraId);

            if (obra == null)
                return Result.Fail("Obra consultada não encontrada!");

            capituloDTO.Obra = obra;
            return Result.Ok(capituloDTO);
        }

        public Result<CapituloDTO> RetornaDadosCapitulo(int capituloId)
        {
            var capituloDTO = new CapituloDTO();
            var capituloNovel = _repository.RetornaCapituloNovelPorId(capituloId);
            var capituloComic = _repository.RetornaCapituloComicPorId(capituloId);

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

            var volume = _repository.RetornaVolumePorId(volumeId);
            if (volume == null)
                return Result.Fail("Volume consultado não encontrada!");

            var obra = _repository.RetornaObraPorId(volume.ObraId);
            if (obra == null)
                return Result.Fail("Obra consultada não encontrada!");

            AtualizaCapituloDTO(capituloDTO, obra, capituloNovel, capituloComic);
            return Result.Ok(capituloDTO);
        }

        public bool VerificaEhComic(int obraId)
        {
            var obra = _repository.RetornaObraPorId(obraId);
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
