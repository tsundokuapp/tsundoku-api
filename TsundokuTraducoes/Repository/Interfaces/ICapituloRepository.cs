using System;
using System.Collections.Generic;
using TsundokuTraducoes.Api.DTOs.Admin;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Models.Capitulo;
using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface ICapituloRepository
    {
        void Adiciona<T>(T entity) where T : class;
        void Atualiza<T>(T entity) where T : class;
        void Exclui<T>(T entity) where T : class;
        bool AlteracoesSalvas();

        #region Comic

        CapituloComic RetornaCapituloComicPorId(int capituloId);
        CapituloComic AtualizaCapituloComic(CapituloDTO capituloDTO);
        CapituloComic RetornaCapituloComicExistente(int idVolume, CapituloDTO capituloDTO);

        #endregion

        #region Novel

        CapituloNovel RetornaCapituloNovelPorId(int capituloId);
        CapituloNovel AtualizaCapituloNovel(CapituloDTO capituloDTO);
        CapituloNovel RetornaCapituloNovelExistente(int idVolume, CapituloDTO capituloDTO);

        #endregion

        void AtualizaObraPorCapitulo(Novel obra, string descritivoCapitulo, string slug, DateTime dataInclusao);
        List<CapituloDTO> RetornaListaCapitulos();        
        Task<Novel> RetornaObraPorId(int obraId);
    }
}
