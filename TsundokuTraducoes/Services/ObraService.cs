using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Recomendacao.Comic;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.Services
{
    public class ObraService : IObraService
    {
        private readonly IObraRepository _repository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ObraService(IObraRepository repository, IGeneroRepository generoRepository, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _generoRepository = generoRepository;   
            _mapper = mapper;
            _webHostEnvironment = hostEnvironment;
        }

        public async Task<Result<List<RetornoObra>>> RetornaListaObras()
        {
            var listaRetornoObras = new List<RetornoObra>();
            var listaObras = await _repository.RetornaListaObras();            

            if (listaObras.Count > 0)
            {
                foreach (var obra in listaObras)
                {
                    listaRetornoObras.Add(await TrataRetornoObra(obra));
                }
            }

            return Result.Ok(listaRetornoObras);
        }

        public async Task<Result<RetornoObra>> RetornaObraPorId(int id)
        {
            var obra = await _repository.RetornaObraPorId(id);
            if (obra == null)
                return Result.Fail("Obra não encontrada!");

            var retornoObra = await TrataRetornoObra(obra);
            return Result.Ok().ToResult(retornoObra);
        }

        public async Task<Result<RetornoObra>> AdicionaObra(ObraDTO obraDTO)
        {
            var obra = _mapper.Map<Novel>(obraDTO);
            var obraExistente = await _repository.RetornaObraExistente(obraDTO.Titulo);

            if (obraExistente != null)
                return Result.Fail("Obra já postada!");

            if (!TratamentoDeStrings.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra)) 
                return Result.Fail("Erro ao adicionar a Obra, código hexadecimal informada fora do padrão!");

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

            await _repository.AdicionaObra(obra);
            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao adicionar a Obra!");

            await _repository.InsereGenerosObra(obraDTO, obra, true);

            var retornoObra = await TrataRetornoObra(obra);
            return Result.Ok().ToResult(retornoObra);
        }

        public async Task<Result<RetornoObra>> AtualizarObra(ObraDTO obraDTO)
        {
            var obraEncontrada = await _repository.RetornaObraPorId(obraDTO.Id);
            if (obraEncontrada == null)
                return Result.Fail("Obra não encontrada!");

            if (!TratamentoDeStrings.ValidaCorHexaDecimal(obraDTO.CodigoCorHexaObra))
                return Result.Fail("Erro ao atualizar a Obra, código hexadecimal informada fora do padrão!");

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

            obraEncontrada = await _repository.AtualizaObra(obraDTO);

            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao atualizar a obra!");

            await _repository.InsereGenerosObra(obraDTO, obraEncontrada, false);

            var retornoObra = await TrataRetornoObra(obraEncontrada);
            return Result.Ok().ToResult(retornoObra);
        }

        public async Task<Result<bool>> ExcluirObra(int idObra)
        {
            var obraEncontrada = await _repository.RetornaObraPorId(idObra);
            if (obraEncontrada == null)
                return Result.Fail("Obra não encontrada!");
            
            _repository.ExcluiObra(obraEncontrada);
            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao excluir a obra!");

            new Imagens().ExcluiDiretorioImagens(obraEncontrada.DiretorioImagemObra);
            return Result.Ok().WithSuccess("Obra excluída com sucesso!");
        }

        public Result<ComicRecomendada> AdicionaObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO)
        {
            var obraRecomendadaExistente = _repository.RetornaObraRecomendadaPorObraId(obraRecomendadaDTO.IdObra);
            if (obraRecomendadaExistente != null)
                return Result.Fail("Obra recomendada já cadastrada!");

            var retornoCargaListaMensagem = CarregaListaMensagemObraRecomendada(obraRecomendadaDTO);
            if (retornoCargaListaMensagem.IsFailed)
                return Result.Fail(retornoCargaListaMensagem.Errors[0].Message);

            var obraRecomendada = _mapper.Map<ComicRecomendada>(obraRecomendadaDTO);
            _repository.AdicionaObraRecomendada(obraRecomendada);
            _repository.InsereListaComentariosObraRecomendada(obraRecomendadaDTO, obraRecomendada);

            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao adicionar a Obra Recomendada!");

            return Result.Ok().ToResult(obraRecomendada);
        }

        public async Task<Result<InformacaoObraDTO>> RetornaInformacaoObraDTO(int? idObra = null)
        {
            var informacaoObraDTO = new InformacaoObraDTO();
            var generos = await _repository.RetornaListaGeneros();

            var retornoOk = generos != null;
            if (!retornoOk)
                return Result.Fail("Erro ao carregar os gêneros!");

            informacaoObraDTO.ListaGeneros.AddRange(generos);

            if (idObra != null)
            {
                var obra = await _repository.RetornaObraPorId(idObra.Value);
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

        public Result<ComentarioComicRecomendada> AdicionaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var comentarioObraRecomendada = _mapper.Map<ComentarioComicRecomendada>(comentarioObraRecomendadaDTO);

            _repository.AdicionaComentarioObraRecomendada(comentarioObraRecomendada);
            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao adicionar a Comentario Obra Recomendada!");

            return Result.Ok().ToResult(comentarioObraRecomendada);
        }

        public Result<ComentarioComicRecomendada> AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO)
        {
            var comentarioObraRecomendada = _repository.RetornaComentarioObraRecomendadaPorId(comentarioObraRecomendadaDTO.Id);
            if (comentarioObraRecomendada == null)
                return Result.Fail("Obra não encontrada!");

            comentarioObraRecomendada = _repository.AtualizaComentarioObraRecomendada(comentarioObraRecomendadaDTO);

            if (!_repository.AlteracoesSalvas().Result)
                return Result.Fail("Erro ao adicionar a Comentario Obra Recomendada!");

            return Result.Ok().ToResult(comentarioObraRecomendada);
        }

        public Result<List<ComicRecomendada>> RetornaListaObraRecomendada()
        {
            var listaObraRecomendada = _repository.RetornaListaObraRecomendada();
            if (listaObraRecomendada == null)
                return Result.Fail("Erro ao retornar todas as Obras!");

            return Result.Ok().ToResult(listaObraRecomendada);
        }

        public Result<ComicRecomendada> RetornaObraRecomendadaPorId(int id)
        {
            var obraRecomendada = _repository.RetornaObraRecomendadaPorId(id);
            if (obraRecomendada == null)
                return Result.Fail("Obra Recomendada não encontrada!");

            return obraRecomendada;
        }

        public Result<ComentarioComicRecomendada> RetornaComentarioObraRecomendadaPorId(int id)
        {
            var comentarioObraRecomendada = _repository.RetornaComentarioObraRecomendadaPorId(id);
            if (comentarioObraRecomendada == null)
                return Result.Fail("Comentário da Obra Recomendada não encontrado!");

            return comentarioObraRecomendada;
        }

        private async Task<RetornoObra> TrataRetornoObra(Novel obra)
        {
            var retornoObra = _mapper.Map<RetornoObra>(obra);
            retornoObra.DataInclusao = obra.DataInclusao.ToString("dd/MM/yyyy HH:mm:ss");
            retornoObra.UsuarioAlteracao = !string.IsNullOrEmpty(obra.UsuarioAlteracao) ? obra.UsuarioAlteracao : "";
            retornoObra.DataAlteracao = obra.DataAlteracao != null ? obra.DataAlteracao?.ToString("dd/MM/yyyy HH:mm:ss") : "";
            retornoObra.Generos = await _generoRepository.CarregaListaGeneros(obra.GenerosObra);
            return retornoObra;
        }
    }
}
