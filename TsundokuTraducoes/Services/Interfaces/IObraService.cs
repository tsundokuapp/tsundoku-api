using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IObraService
    {
        Task<Result<List<RetornoObra>>> RetornaListaObras();
        Task<Result<RetornoObra>> RetornaObraPorId(int id);
        Task<Result<RetornoObra>> AdicionaObra(ObraDTO obraDTO);
        Task<Result<RetornoObra>> AtualizarObra(ObraDTO obraDTO);
        Task<Result<InformacaoObraDTO>> RetornaInformacaoObraDTO(int? idObra = null);
        Task<Result<bool>> ExcluirObra(int idObra);
        Result<ObraRecomendada> AdicionaObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO);
        Result<ComentarioObraRecomendada> AdicionaComentarioObraRecomendada(ComentarioObraRecomendadaDTO obraRecomendadaDTO);
        Result<ComentarioObraRecomendada> AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        Result<List<ObraRecomendada>> RetornaListaObraRecomendada();
        Result<ObraRecomendada> RetornaObraRecomendadaPorId(int id);
        Result<ComentarioObraRecomendada> RetornaComentarioObraRecomendadaPorId(int id);
    }
}
