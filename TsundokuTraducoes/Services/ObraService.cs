using AutoMapper;
using FluentResults;
using Newtonsoft.Json;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;
using Microsoft.AspNetCore.Hosting;
using TsundokuTraducoes.Api.Utilidades;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Repository.Interfaces;

namespace TsundokuTraducoes.Api.Services
{
    public class ObraService : IObraService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IObraRepository _repository;
        private readonly IMapper _mapper;

        public ObraService(IObraRepository repository, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = hostEnvironment;
        }

        public Result<List<Obra>> RetornaListaObras()
        {
            var listaObras = _repository.RetornaListaObras();
            if (listaObras == null)
                return Result.Fail("Erro ao retornar todas as Obras!");

            return Result.Ok(listaObras);
        }

        public Result<Obra> RetornaObraPorId(int id)
        {
            var obra = _repository.RetornaObraPorId(id);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            return Result.Ok(obra);
        }

        public Result<Obra> AdicionaObra(ObraDTO obraDTO)
        {
            var obra = _mapper.Map<Obra>(obraDTO);
            var obraExistente = _repository.RetornaObraExistente(obraDTO.Titulo);

            if (obraExistente != null)
                return Result.Fail("Obra já postada!");

            if (obraDTO.ImagemCapaPrincipalFile != null)
            {
                var uploadImagemCapa = new Imagens(_webHostEnvironment);
                var retornoProcessoImagem = uploadImagemCapa.ProcessaImagemObra(obraDTO.ImagemCapaPrincipalFile, obraDTO.Titulo, obra, obraDTO);
                if (retornoProcessoImagem.IsFailed)
                    return Result.Fail(retornoProcessoImagem.Errors[0].Message);
            }
            else
            {
                return Result.Fail("Erro ao adicionar a Obra, imagem da capa não enviada!");
            }

            if (obraDTO.ImagemBannerFile != null)
            {
                var uploadImagemCapa = new Imagens(_webHostEnvironment);
                var retornoProcessoImagem = uploadImagemCapa.ProcessaImagemObra(obraDTO.ImagemBannerFile, obraDTO.Titulo, obra, obraDTO, true);
                if (retornoProcessoImagem.IsFailed)
                    return Result.Fail(retornoProcessoImagem.Errors[0].Message);
            }

            _repository.Adiciona(obra);
            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao adicionar a Obra!");

            _repository.InsereGenerosObra(obraDTO, obra, true);
            return Result.Ok().ToResult(obra);
        }

        public Result<Obra> AtualizarObra(ObraDTO obraDTO)
        {
            var obraEncontrada = _repository.RetornaObraPorId(obraDTO.Id);
            if (obraEncontrada == null)
                return Result.Fail("Obra não encontrada!");

            if (obraDTO.ImagemCapaPrincipalFile != null)
            {
                var retornoProcessoImagem = new Imagens(_webHostEnvironment).ProcessaImagemObra(obraDTO.ImagemCapaPrincipalFile, obraDTO.Titulo, obraEncontrada, obraDTO);
                if (retornoProcessoImagem.IsFailed)
                    return Result.Fail(retornoProcessoImagem.Errors[0].Message);
            }
            else
            {
                obraDTO.ImagemCapaPrincipal = obraEncontrada.ImagemCapaPrincipal;
            }

            if (obraDTO.ImagemBannerFile != null)
            {
                var retornoProcessoImagem = new Imagens(_webHostEnvironment).ProcessaImagemObra(obraDTO.ImagemBannerFile, obraDTO.Titulo, obraEncontrada, obraDTO, true);
                if (retornoProcessoImagem.IsFailed)
                    return Result.Fail(retornoProcessoImagem.Errors[0].Message);
            }
            else
            {
                obraDTO.ImagemBanner = obraEncontrada.ImagemBanner;
            }

            obraEncontrada = _repository.AtualizaObra(obraDTO);

            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao atualizar a obra!");

            _repository.InsereGenerosObra(obraDTO, obraEncontrada, false);
            return Result.Ok().ToResult(obraEncontrada);
        }

        public Result<bool> ExcluirObra(int idObra)
        {
            var obraEncontrada = _repository.RetornaObraPorId(idObra);
            if (obraEncontrada == null)
                return Result.Fail("Obra não encontrada!");

            _repository.Exclui(obraEncontrada);
            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao excluir a obra!");

            return Result.Ok().WithSuccess("Obra excluída com sucesso!");
        }

        public Result<ObraRecomendada> AdicionaObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO)
        {
            var obraRecomendadaExistente = _repository.RetornaObraRecomendadaPorObraId(obraRecomendadaDTO.IdObra);
            if (obraRecomendadaExistente != null)
                return Result.Fail("Obra recomendada já cadastrada!");

            var retornoCargaListaMensagem = CarregaListaMensagemObraRecomendada(obraRecomendadaDTO);
            if (retornoCargaListaMensagem.IsFailed)
                return Result.Fail(retornoCargaListaMensagem.Errors[0].Message);

            var obraRecomendada = _mapper.Map<ObraRecomendada>(obraRecomendadaDTO);
            _repository.Adiciona(obraRecomendada);
            _repository.InsereListaComentariosObraRecomendada(obraRecomendadaDTO, obraRecomendada);

            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao adicionar a Obra Recomendada!");

            return Result.Ok().ToResult(obraRecomendada);
        }

        public Result<InformacaoObraDTO> RetornaInformacaoObraDTO(int? idObra = null)
        {
            var informacaoObraDTO = new InformacaoObraDTO();
            var generos = _repository.RetornaListaGeneros();

            var retornoOk = generos != null;
            if (!retornoOk)
                return Result.Fail("Erro ao carregar os gêneros!");

            informacaoObraDTO.ListaGeneros.AddRange(generos);

            if (idObra != null)
            {
                var obra = _repository.RetornaObraPorId(idObra.Value);
                if (obra == null)
                    return Result.Fail("Obra não encontrada!");

                informacaoObraDTO.Obra = obra;
            }

            return Result.Ok(informacaoObraDTO);
        }

        private Result CarregaListaMensagemObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO)
        {
            if (string.IsNullOrEmpty(obraRecomendadaDTO.ListaComentarioObraRecomendadaDTOJson))
                return Result.Fail("Não foi informado uma lista de comentário obra recomendada");

            obraRecomendadaDTO.ListaComentarioObraRecomendadaDTO =
                JsonConvert.DeserializeObject<List<ComentarioObraRecomendadaDTO>>(obraRecomendadaDTO.ListaComentarioObraRecomendadaDTOJson);

            return Result.Ok();
        }

        public Result<ComentarioObraRecomendada> AdicionaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var comentarioObraRecomendada = _mapper.Map<ComentarioObraRecomendada>(comentarioObraRecomendadaDTO);

            _repository.Adiciona(comentarioObraRecomendada);
            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao adicionar a Comentario Obra Recomendada!");

            return Result.Ok().ToResult(comentarioObraRecomendada);
        }

        public Result<ComentarioObraRecomendada> AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var comentarioObraRecomendada = _repository.RetornaComentarioObraRecomendadaPorId(comentarioObraRecomendadaDTO.Id);
            if (comentarioObraRecomendada == null)
                return Result.Fail("Obra não encontrada!");

            comentarioObraRecomendada = _repository.AtualizaComentarioObraRecomendada(comentarioObraRecomendadaDTO);

            if (!_repository.AlteracoesSalvas())
                return Result.Fail("Erro ao adicionar a Comentario Obra Recomendada!");

            return Result.Ok().ToResult(comentarioObraRecomendada);
        }

        public Result<List<ObraRecomendada>> RetornaListaObraRecomendada()
        {
            var listaObraRecomendada = _repository.RetornaListaObraRecomendada();
            if (listaObraRecomendada == null)
                return Result.Fail("Erro ao retornar todas as Obras!");

            return Result.Ok().ToResult(listaObraRecomendada);
        }

        public Result<ObraRecomendada> RetornaObraRecomendadaPorId(int id)
        {
            var obraRecomendada = _repository.RetornaObraRecomendadaPorId(id);
            if (obraRecomendada == null)
                return Result.Fail("Obra Recomendada não encontrada!");

            return obraRecomendada;
        }

        public Result<ComentarioObraRecomendada> RetornaComentarioObraRecomendadaPorId(int id)
        {
            var comentarioObraRecomendada = _repository.RetornaComentarioObraRecomendadaPorId(id);
            if (comentarioObraRecomendada == null)
                return Result.Fail("Comentário da Obra Recomendada não encontrado!");

            return comentarioObraRecomendada;
        }
    }
}
