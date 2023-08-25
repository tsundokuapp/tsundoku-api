using FluentResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Recomendacao.Comic;

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
        Result<ComicRecomendada> AdicionaObraRecomendada(ObraRecomendadaDTO obraRecomendadaDTO);
        Result<ComentarioComicRecomendada> AdicionaComentarioObraRecomendada(ComentarioObraRecomendadaDTO obraRecomendadaDTO);
        Result<ComentarioComicRecomendada> AtualizaComentarioObraRecomendada(ComentarioObraRecomendadaDTO comentarioObraRecomendadaDTO);
        Result<List<ComicRecomendada>> RetornaListaObraRecomendada();
        Result<ComicRecomendada> RetornaObraRecomendadaPorId(int id);
        Result<ComentarioComicRecomendada> RetornaComentarioObraRecomendadaPorId(int id);
    }
}
