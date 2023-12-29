using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface ICapituloServiceOld
    {
        Task<Result<List<RetornoCapitulo>>> RetornaListaCapitulos(Guid? volumeId);
        
        Task<Result<CapituloNovel>> RetornaCapituloNovelPorId(Guid capituloId);
        Task<Result<CapituloComic>> RetornaCapituloComicPorId(Guid capituloId);

        Task<Result<RetornoCapitulo>> AdicionaCapituloNovel(CapituloDTO capituloDTO);
        Task<Result<RetornoCapitulo>> AdicionaCapituloComic(CapituloDTO capituloDTO);        
        
        Task<Result<RetornoCapitulo>> AtualizaCapituloComic(CapituloDTO capituloDTO);
        Task<Result<RetornoCapitulo>> AtualizaCapituloNovel(CapituloDTO capituloDTO);        
        
        Task<Result> ExcluiCapituloNovel(Guid capituloId);
        Task<Result> ExcluiCapituloComic(Guid capituloId);
    }
}