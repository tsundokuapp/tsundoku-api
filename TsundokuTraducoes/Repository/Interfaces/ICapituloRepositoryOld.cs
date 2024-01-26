using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface ICapituloRepositoryOld
    {
        Task<List<CapituloNovel>> RetornaListaCapitulosNovel(Guid? volumeId);
        Task<List<CapituloComic>> RetornaListaCapitulosComic(Guid? volumeId);

        Task<CapituloNovel> RetornaCapituloNovelPorId(Guid capituloId);
        Task<CapituloComic> RetornaCapituloComicPorId(Guid capituloId);

        Task AdicionaCapituloNovel(CapituloNovel volumeNovel);
        Task AdicionaCapituloComic(CapituloComic volumeComic);

        Task<CapituloNovel> AtualizaCapituloNovel(CapituloDTO capituloDTO);
        Task<CapituloComic> AtualizaCapituloComic(CapituloDTO capituloDTO);

        void ExcluiCapituloNovel(CapituloNovel volumeNovel);
        void ExcluiCapituloComic(CapituloComic volumeComic);

        Task<CapituloNovel> RetornaCapituloNovelExistente(CapituloDTO capituloDTO);
        Task<CapituloComic> RetornaCapituloComicExistente(CapituloDTO capituloDTO);

        void AtualizaNovelPorCapitulo(Novel novel, CapituloNovel capituloNovel);
        void AtualizaComicPorCapitulo(Comic comic, CapituloComic capituloComic);

        Task<bool> AlteracoesSalvas();
    }
}
