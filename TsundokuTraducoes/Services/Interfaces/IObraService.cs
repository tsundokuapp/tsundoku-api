using FluentResults;
using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IObraService
    {
        Result<List<Obra>> RetornaListaObras();
        Result<Obra> RetornaObraPorId(int id);
        Result<Obra> AdicionaObra(ObraDTO obraDTO);
        Result<Obra> AtualizarObra(ObraDTO obraDTO);
        Result<InformacaoObraDTO> RetornaInformacaoObraDTO(int? idObra = null);
        Result<bool> ExcluirObra(int idObra);
        Result<ObraRecomendada> AdicionaObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO);
        Result<ComentarioObraRecomendada> AdicionaComentarioObraRecomendada(ComentarioObraRecomendadaDTO obraRecomendadaDTO);
        Result<ComentarioObraRecomendada> AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        Result<List<ObraRecomendada>> RetornaListaObraRecomendada();
        Result<ObraRecomendada> RetornaObraRecomendadaPorId(int id);
        Result<ComentarioObraRecomendada> RetornaComentarioObraRecomendadaPorId(int id);
    }
}
