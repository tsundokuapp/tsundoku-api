using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Utilidades;
using TsundokuTraducoes.Models;

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
            {   
                return Result.Fail("Erro ao retornar todas as Obras!");
            }

            return Result.Ok(listaObras);
        }

        public Result<Obra> RetornaObraPorId(int id)
        {
            var obra = _repository.RetornaObraPorId(id);
            if (obra == null)
            {
                return Result.Fail("Obra não encontrada!");
            }

            return Result.Ok(obra);
        }

        public Result<Obra> AdicionaObra(ObraDTO obraDTO)
        {
            var obra = _mapper.Map<Obra>(obraDTO);
            _repository.CarregaListaGeneros(obraDTO, obra, true);
            _repository.InsereGenerosObra(obraDTO);

            if (obraDTO.ImagemCapa != null)
            {
                var uploadImagemCapa = new Imagens(_webHostEnvironment);
                uploadImagemCapa.ProcessaUploadImagemCapaObra(obraDTO.ImagemCapa, obraDTO.Titulo, obra, obraDTO);
            }
            else
            {
                return Result.Fail("Erro ao adicionar a Obra, imagem da capa não enviada!");
            }

            _repository.Adiciona(obra);
            if (!_repository.AlteracoesSalvas())
            {
                return Result.Fail("Erro ao adicionar a Obra!");
            }
                
            return Result.Ok().ToResult(obra);
        }

        public Result<Obra> AtualizarObra(ObraDTO obraDTO)
        {   
            var obraEncontrada = _repository.RetornaObraPorId(obraDTO.Id);
            if (obraEncontrada == null)
            {
                return Result.Fail("Obra não encontrada!");
            }
            

            if (obraDTO.ImagemCapa != null)
            {
                new Imagens(_webHostEnvironment).ProcessaUploadImagemCapaObra(obraDTO.ImagemCapa, obraDTO.Titulo, obraEncontrada, obraDTO);
            }

            obraEncontrada = _repository.AtualizaObra(obraDTO);
            if (!_repository.AlteracoesSalvas())
            {
                return Result.Fail("Erro ao atualizar a obra!");
            }

            return Result.Ok().ToResult(obraEncontrada);
        }   

        public Result<bool> ExcluirObra(int idObra)
        {
            var obraEncontrada = _repository.RetornaObraPorId(idObra);
            if (obraEncontrada == null)
            {
                return Result.Fail("Obra não encontrada!");
            }

            //new Imagens().ExcluiDiretorioImagens(obraEncontrada.DiretorioObra);
            _repository.Exclui(obraEncontrada);
            if (!_repository.AlteracoesSalvas())
            {
                return Result.Fail("Obra não encontrada!");
                
            }

            return Result.Ok().WithSuccess("Obra excluída com sucesso!");
        }

        public Result<InformacaoObraDTO> RetornaInformacaoObraDTO(int? idObra = null)
        {
            var informacaoObraDTO = new InformacaoObraDTO();
            var generos = _repository.RetornaListaGeneros();

            var retornoOk = generos != null;
            if (!retornoOk)
            {
                return Result.Fail("Erro ao retornar as informações das obras!");
            }
            else
            {
                informacaoObraDTO.ListaGeneros.AddRange(generos);

                if (idObra != null)
                {
                    var obra = _repository.RetornaObraPorId(idObra.Value);
                    if (obra == null)
                    {
                        return Result.Fail("Obra não encontrada!");
                    }

                    informacaoObraDTO.Obra = obra;
                }

                return Result.Ok(informacaoObraDTO);
            }
        }       
    }
}
