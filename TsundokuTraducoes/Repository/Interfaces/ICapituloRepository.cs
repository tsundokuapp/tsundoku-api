using System;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models;
using TsundokuTraducoes.Api.DTOs.Admin;
using System.Threading.Tasks;

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

        void AtualizaObraPorCapitulo(Obra obra, string descritivoCapitulo, string slug, DateTime dataInclusao);
        List<CapituloDTO> RetornaListaCapitulos();        
        Task<Obra> RetornaObraPorId(int obraId);
    }
}
