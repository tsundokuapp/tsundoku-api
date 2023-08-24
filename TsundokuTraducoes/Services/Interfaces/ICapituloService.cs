using FluentResults;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.DTOs.Admin;
using System.Threading.Tasks;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface ICapituloService
    {
        #region Comic        

        Result<CapituloComic> RetornaCapituloComicPorId(int capituloId);
        Task<Result<CapituloComic>> AdicionaCapituloComic(CapituloDTO capituloDTO);
        Task<Result<CapituloComic>> AtualizaCapituloComic(CapituloDTO capituloDTO);
        Result<bool> ExcluiCapituloComic(int capituloId);

        #endregion

        #region Novel        

        Result<CapituloNovel> RetornaCapituloNovelPorId(int capituloId);
        Task<Result<CapituloNovel>> AdicionaCapituloNovel(CapituloDTO capituloDTO);
        Task<Result<CapituloNovel>> AtualizaCapituloNovel(CapituloDTO capituloDTO);
        Result<bool> ExcluiCapituloNovel(int capituloId);

        #endregion

        Result<List<CapituloDTO>> RetornaListaCapitulos();
        Task<Result<CapituloDTO>> RetornaDadosObra(int obraId);
        Task<Result<CapituloDTO>> RetornaDadosCapitulo(int capituloId);        
        Task<bool> VerificaEhComic(int obraId);
    }
}