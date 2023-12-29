using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface IObraServiceOld
    {
        Task<Result<List<RetornoObra>>> RetornaListaObras();
        
        Task<Result<RetornoObra>> RetornaNovelPorId(Guid id);
        Task<Result<RetornoObra>> RetornaComicPorId(Guid id);
                
        Task<Result<RetornoObra>> AdicionaNovel(ObraDTO obraDTO);
        Task<Result<RetornoObra>> AdicionaComic(ObraDTO obraDTO);

        Task<Result<RetornoObra>> AtualizaNovel(ObraDTO obraDTO);
        Task<Result<RetornoObra>> AtualizaComic(ObraDTO obraDTO);

        Task<Result<bool>> ExcluiNovel(Guid idObra);
        Task<Result<bool>> ExcluiComic(Guid idObra);

        Task<Result<InformacaoObraDTO>> RetornaInformacaoObraDTO(Guid? idObra = null);
    }
}
