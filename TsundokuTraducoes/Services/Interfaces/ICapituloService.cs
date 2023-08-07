using FluentResults;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.DTOs.Admin;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface ICapituloService
    {
        #region Comic        

        Result<CapituloComic> RetornaCapituloComicPorId(int capituloId);
        Result<CapituloComic> AdicionaCapituloComic(CapituloDTO capituloDTO);
        Result<CapituloComic> AtualizaCapituloComic(CapituloDTO capituloDTO);
        Result<bool> ExcluiCapituloComic(int capituloId);

        #endregion

        #region Novel        

        Result<CapituloNovel> RetornaCapituloNovelPorId(int capituloId);
        Result<CapituloNovel> AdicionaCapituloNovel(CapituloDTO capituloDTO);
        Result<CapituloNovel> AtualizaCapituloNovel(CapituloDTO capituloDTO);
        Result<bool> ExcluiCapituloNovel(int capituloId);

        #endregion

        Result<List<CapituloDTO>> RetornaListaCapitulos();
        Result<CapituloDTO> RetornaDadosObra(int obraId);
        Result<CapituloDTO> RetornaDadosCapitulo(int capituloId);        
        bool VerificaEhComic(int obraId);
    }
}