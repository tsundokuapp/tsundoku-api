using FluentResults;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.DTOs.Admin.Retorno;
using TsundokuTraducoes.Api.Models.Capitulo;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface ICapituloService
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
        
        Task<Result<CapituloDTO>> RetornaDadosObra(Guid obraId);
        Task<Result<CapituloDTO>> RetornaDadosCapitulo(Guid capituloId);
    }
}