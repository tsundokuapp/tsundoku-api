using FluentResults;
using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models;

namespace TsundokuTraducoes.Api.Services.Interfaces
{
    public interface ICapituloService
    {
        Result<List<CapituloNovel>> RetornaListaCapitulos();
        Result<CapituloNovel> RetornaCapituloPorId(int capituloId);
        Result<CapituloNovel> AdicionaCapitulo(CapituloDTO capituloDTO);
        Result<CapituloNovel> AtualizaCapitulo(CapituloDTO capituloDTO);
        Result<CapituloDTO> RetornaDadosObra(int obraId);
        Result<CapituloDTO> RetornaDadosCapitulo(int capituloId);
        Result<bool> ExcluiCapitulo(int capituloId);
        bool VerificaEhComic(int obraId);
    }
}
