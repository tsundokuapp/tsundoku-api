using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;
using static Dapper.SqlMapper;

namespace TsundokuTraducoes.Api.Repository
{
    public class ObraRepositoryOld : RepositoryOld, IObraRepositoryOld
    {   
        private readonly IGeneroRepositoryOld _generoRepository;

        public ObraRepositoryOld(ContextBase context, IGeneroRepositoryOld generoRepository) : base(context)
        {   
            _generoRepository = generoRepository;
        }

        public async Task<List<Novel>> RetornaListaNovels()
        {
            var listaNovels = new List<Novel>();
            await _contextDapper.QueryAsync(RetornaQueryListaNovel(),
                new[]
                {
                    typeof(Novel),
                    typeof(GeneroNovel)
                },
                (objects) =>
                {
                    var novel = (Novel)objects[0];
                    var generoNovel = (GeneroNovel)objects[1];

                    if (listaNovels.SingleOrDefault(o => o.Id == novel.Id) == null)
                    {
                        listaNovels.Add(novel);
                    }
                    else
                    {
                        novel = listaNovels.SingleOrDefault(o => o.Id == novel.Id);
                    }

                    AdicionaGeneroNovel(novel, generoNovel);

                    return novel;
                }, splitOn: "Id, NovelId");

            return listaNovels;
        }
        
        public async Task<Novel> RetornaNovelPorId(Guid novelId)
        {
            var listaNovels = await RetornaListaNovels();
            return listaNovels.SingleOrDefault(f => f.Id == novelId);
        }


        public async Task<List<Comic>> RetornaListaComics()
        {
            var listaComics = new List<Comic>();
            await _contextDapper.QueryAsync(RetornaQueryListaComic(),
                new[]
                {
                    typeof(Comic),
                    typeof(GeneroComic)
                },
                (objects) =>
                {
                    var comic = (Comic)objects[0];
                    var generoComic = (GeneroComic)objects[1];

                    if (listaComics.SingleOrDefault(o => o.Id == comic.Id) == null)
                    {
                        listaComics.Add(comic);
                    }
                    else
                    {
                        comic = listaComics.SingleOrDefault(o => o.Id == comic.Id);
                    }

                    AdicionaGeneroComic(comic, generoComic);

                    return comic;
                }, splitOn: "Id, ComicId");

            return listaComics;
        }

        public async Task<Comic> RetornaComicPorId(Guid comicId)
        {
            var listaComics = await RetornaListaComics();
            return listaComics.SingleOrDefault(f => f.Id == comicId);
        }


        public async Task AdicionaNovel(Novel novel)
        {
            await AdicionaEntidadeBancoDados(novel);
        }        
        
        public async Task AdicionaComic(Comic comic)
        {
            await AdicionaEntidadeBancoDados(comic);
        }


        public Novel AtualizaNovel(ObraDTO obraDTO)
        {
            var novelEncontrada = _context.Novels.Include(n => n.GenerosNovel).SingleOrDefault(n => n.Id == obraDTO.Id);
            obraDTO.DiretorioImagemObra = novelEncontrada.DiretorioImagemObra;
            _context.Entry(novelEncontrada).CurrentValues.SetValues(obraDTO);
            novelEncontrada.DataAlteracao = DateTime.Now;
                        
            return novelEncontrada;
        }

        public Comic AtualizaComic(ObraDTO obraDTO)
        {
            var comicEncontrada = _context.Comics.Include(o => o.GenerosComic).SingleOrDefault(o => o.Id == obraDTO.Id);
            obraDTO.DiretorioImagemObra = comicEncontrada.DiretorioImagemObra;
            _context.Entry(comicEncontrada).CurrentValues.SetValues(obraDTO);
            comicEncontrada.DataAlteracao = DateTime.Now;

            return comicEncontrada;
        }


        public void ExcluiNovel(Novel novel)
        {
            ExcluiEntidadeBancoDados(novel);
        }

        public void ExcluiComic(Comic comic)
        {
            ExcluiEntidadeBancoDados(comic);
        }


        public async Task<Novel> RetornaNovelExistente(string titulo)
        {
            titulo = titulo.Trim();
            var sql = $@"SELECT * 
                           FROM Novels 
                          WHERE Titulo LIKE @Titulo";

            var novelExistente = await _contextDapper.QueryAsync<Novel>(sql, new { Titulo = "%" + titulo + "%" });
            return novelExistente.FirstOrDefault();
        }

        public async Task<Comic> RetornaComicExistente(string titulo)
        {
            titulo = titulo.Trim();
            var sql = $@"SELECT * 
                           FROM Comics 
                          WHERE Titulo LIKE @Titulo";

            var comicExistente = await _contextDapper.QueryAsync<Comic>(sql, new { Titulo = "%" + titulo + "%" });
            return comicExistente.FirstOrDefault();
        }


        public async Task InsereGenerosNovel(ObraDTO obraDTO, Novel novelEncontrada, bool inclusao)
        {
            if (inclusao)
            {
                novelEncontrada.GenerosNovel = new List<GeneroNovel>();
            }
            else
            {
                foreach (var generosNovel in novelEncontrada.GenerosNovel)
                {
                    _generoRepository.ExcluiGeneroNovel(generosNovel);
                }
            }

            var arrayGenero = obraDTO.ListaGeneros[0]?.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (arrayGenero != null && arrayGenero.Length > 0)
            {
                foreach (var genero in arrayGenero)
                {
                    var generoEncontrado = _context.Generos.Single(s => s.Slug == genero);
                    await _generoRepository.AdicionaGeneroNovel(new GeneroNovel
                    {
                        GeneroId = generoEncontrado.Id,
                        NovelId = novelEncontrada.Id
                    });

                    await AlteracoesSalvas();
                }
            }
        }

        public async Task InsereGenerosComic(ObraDTO obraDTO, Comic comicEncontrada, bool inclusao)
        {
            if (inclusao)
            {
                comicEncontrada.GenerosComic = new List<GeneroComic>();
            }
            else
            {
                foreach (var generosComic in comicEncontrada.GenerosComic)
                {
                    _generoRepository.ExcluiGeneroComic(generosComic);
                }
            }

            var arrayGenero = obraDTO.ListaGeneros[0]?.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (arrayGenero != null && arrayGenero.Length > 0)
            {
                foreach (var genero in arrayGenero)
                {
                    var generoEncontrado = _context.Generos.Single(s => s.Slug == genero);
                    await _generoRepository.AdicionaGeneroComic(new GeneroComic
                    {
                        GeneroId = generoEncontrado.Id,
                        ComicId = comicEncontrada.Id
                    });

                    await AlteracoesSalvas();
                }
            }
        }
              

        private void AdicionaGeneroNovel(Novel novel, GeneroNovel generoNovel)
        {
            if (generoNovel != null)
                novel.GenerosNovel.Add(generoNovel);
        }

        private void AdicionaGeneroComic(Comic comic, GeneroComic generoComic)
        {
            if (generoComic != null)
                comic.GenerosComic.Add(generoComic);
        }


        private string RetornaQueryListaNovel()
        {
            return @"SELECT N.*, GN.*
                       FROM Novels N
                      INNER JOIN GenerosNovel GN ON GN.NovelId = N.Id
                      ORDER BY N.Titulo;";
        }

        private string RetornaQueryListaComic()
        {
            return @"SELECT C.*, GC.*
                       FROM Comics C
                      INNER JOIN GenerosComic GC ON GC.ComicId = C.Id
                      ORDER BY C.Titulo;";
        }       
    }
}