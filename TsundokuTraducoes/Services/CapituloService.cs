using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Utilidades;
using TsundokuTraducoes.Models;

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

        public Result<List<CapituloNovel>> RetornaListaCapitulos()
        {
            var capitulos = _repository.RetornaListaCapitulos();
            if (capitulos == null)
            {
                return Result.Fail("Erro ao retornar todos os capitulos!");
            }

            return Result.Ok(capitulos);
        }

        public Result<CapituloNovel> RetornaCapituloPorId(int capituloId)
        {
            var capitulo = _repository.RetornaCapituloPorId(capituloId);
            if (capitulo == null)
            {
                return Result.Fail("Erro ao retornar o capitulo!");
            }

            return Result.Ok(capitulo);
        }

        public Result<CapituloNovel> AdicionaCapitulo(CapituloDTO capituloDTO)
        {
            var capitulo = _mapper.Map<CapituloNovel>(capituloDTO);
            var volume = _repository.RetornaVolumePorId(capituloDTO.VolumeId);
            var obra = _repository.RetornaObraPorId(volume.ObraId);
            _repository.Adiciona(capitulo);

            var ehComic = VerificaEhComic(volume.ObraId);
            if (ehComic)
            {
                if (capituloDTO.ListaImagensCapitulo != null && capituloDTO.ListaImagensCapitulo.Count > 0)
                {
                    
                    var uploadImagemCapa = new Imagens(_webHostEnvironment);
                    var resultadoMensagemDTO = uploadImagemCapa.ProcessaListaUploadImagemPaginaCapitulo(capituloDTO.ListaImagensCapitulo, obra, volume, capitulo);

                    if (resultadoMensagemDTO.Erro)
                    {
                        return Result.Fail(resultadoMensagemDTO.MensagemErro);
                    }
                }
                else
                {
                    return Result.Fail("Não foi encontrada a lista de imagens para o capítulo");
                }
            }

            //capitulo.ConteudoNovel = JsonConvert.SerializeObject(capitulo.ListaImagensManga);

            if (_repository.AlteracoesSalvas())
            {
                _repository.AtualizaObraPorCapitulo(obra, capitulo);
                if (ehComic)
                {
                    foreach (var urlImagemManga in capitulo.ListaImagensManga)
                    {
                        urlImagemManga.CapituloId = capitulo.Id;
                    }
                }

                return Result.Ok(capitulo);
            }

            return Result.Fail("Erro ao adicionar o capítulo!");
        }

        public Result<CapituloNovel> AtualizaCapitulo(CapituloDTO capituloDTO)
        {
            var capituloEncontrado = _repository.RetornaCapituloPorId(capituloDTO.Id);
            if (capituloEncontrado == null)
            {
                return Result.Fail("Capítulo não encontrado!");
            }

            var volume = _repository.RetornaVolumePorId(capituloDTO.VolumeId);

            if (capituloDTO.ListaImagensCapitulo != null && capituloDTO.ListaImagensCapitulo.Count > 0)
            {

                var obra = _repository.RetornaObraPorId(volume.ObraId);
                new Imagens().ExcluiDiretorioImagens(capituloEncontrado.DiretorioCapitulo);
                _repository.ExcluiTabelaUrlImagensManga(capituloEncontrado);

                var resultadoMensagemDTO = new Imagens().ProcessaListaUploadImagemPaginaCapitulo(capituloDTO.ListaImagensCapitulo, obra, volume, capituloEncontrado);

                if (resultadoMensagemDTO.Erro)
                {
                    return Result.Fail(resultadoMensagemDTO.MensagemErro);
                }
            }

            capituloEncontrado = _repository.AtualizaCapitulo(capituloDTO);

            if (!_repository.AlteracoesSalvas())
            {
                return Result.Fail("Erro ao atualizar o capítulo!");
            }

            return Result.Ok(capituloEncontrado);

        }

        public Result<bool> ExcluiCapitulo(int capituloId)
        {
            var capituloEncontrado = _repository.RetornaCapituloPorId(capituloId);
            if (capituloEncontrado == null)
            {
                return Result.Fail("Capítulo não encontrado!");
            }

            var volume = _repository.RetornaVolumePorId(capituloEncontrado.VolumeId);
            var obra = _repository.RetornaObraPorId(volume.ObraId);

            if (VerificaEhComic(obra.Id))
            {
                var imagens = new Imagens();
                imagens.ExcluiDiretorioImagens(capituloEncontrado.DiretorioCapitulo);
            }

            _repository.Exclui(capituloEncontrado);
            if (!_repository.AlteracoesSalvas())
            {
                return Result.Fail("Erro ao excluir o capítulo!");
            }

            return Result.Ok().WithSuccess("Capítulo excluído com sucesso!");
        }

        public Result<CapituloDTO> RetornaDadosObra(int obraId)
        {
            var capituloDTO = new CapituloDTO();
            var obra = _repository.RetornaObraPorId(obraId);

            if (obra == null)
            {
                return Result.Fail("Obra consultada não encontrada!");
            }

            capituloDTO.Obra = obra;
            //capituloDTO.EhComic = VerificaEhComic(obra.Id);

            return Result.Ok(capituloDTO);
        }

        public Result<CapituloDTO> RetornaDadosCapitulo(int capituloId)
        {
            var capituloDTO = new CapituloDTO();
            var capitulo = _repository.RetornaCapituloPorId(capituloId);
            if (capitulo == null)
            {
                return Result.Fail("Capítulo consultado não encontrada!");
            }

            var volume = _repository.RetornaVolumePorId(capitulo.VolumeId);
            if (volume == null)
            {
                return Result.Fail("Volume consultado não encontrada!");
            }

            var obra = _repository.RetornaObraPorId(volume.ObraId);
            if (obra == null)
            {
                return Result.Fail("Obra consultada não encontrada!");
            }

            AtualizaCapituloDTO(capituloDTO, capitulo, obra);
            return Result.Ok(capituloDTO);
        }

        private void AtualizaCapituloDTO(CapituloDTO capituloDTO, CapituloNovel capitulo, Obra obra)
        {
            capituloDTO.Id = capitulo.Id;
            capituloDTO.Numero = capitulo.Numero;
            capituloDTO.Parte = !string.IsNullOrEmpty(capitulo.Parte) ? capitulo.Parte : string.Empty;
            capituloDTO.Titulo = !string.IsNullOrEmpty(capitulo.Titulo) ? capitulo.Titulo : string.Empty;
            capituloDTO.VolumeId = capitulo.VolumeId;
            capituloDTO.ConteudoNovel = !string.IsNullOrEmpty(capitulo.ConteudoNovel) ? capitulo.ConteudoNovel : string.Empty;
            capituloDTO.TituloObra = obra.Titulo;
            capituloDTO.TipoObraSlug = obra.TipoObraSlug;
            capituloDTO.ObraId = obra.Id;
            capituloDTO.UsuarioAlteracao = capitulo.UsuarioAlteracao;
            capituloDTO.UsuarioCadastro = capitulo.UsuarioCadastro;
            capituloDTO.Obra = obra;
            //capituloDTO.EhComic = VerificaEhComic(obra.Id);
            capituloDTO.EhIlustracoesNovel = capitulo.EhIlustracoesNovel;
        }

        public bool VerificaEhComic(int obraId)
        {
            var obra = _repository.RetornaObraPorId(obraId);
            return obra.TipoObraSlug == "manga" || obra.TipoObraSlug == "manhua" || obra.TipoObraSlug == "manhwa";
        }
    }
}
