﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin;
using TsundokuTraducoes.Api.Models.Obra;

namespace TsundokuTraducoes.Api.Repository.Interfaces
{
    public interface IObraRepository
    {
        Task<List<Novel>> RetornaListaNovels();
        Task<List<Comic>> RetornaListaComics();
        
        Task<Novel> RetornaNovelPorId(Guid obraId);
        Task<Comic> RetornaComicPorId(Guid obraId);

        Task AdicionaNovel(Novel novel);
        Task AdicionaComic(Comic comic);
               
        Novel AtualizaNovel(ObraDTO obraDTO);
        Comic AtualizaComic(ObraDTO obraDTO);

        void ExcluiNovel(Novel novel);
        void ExcluiComic(Comic comic);

        Task InsereGenerosNovel(ObraDTO obraDTO, Novel obra, bool inclusao);        
        Task InsereGenerosComic(ObraDTO obraDTO, Comic comic, bool inclusao);        
        
        Task<Novel> RetornaNovelExistente(string titulo);
        Task<Comic> RetornaComicExistente(string titulo);
        
        
        Task<bool> AlteracoesSalvas();
    }
}
