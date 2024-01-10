using Dapper;
using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;

#nullable disable

namespace TsundokuTraducoes.Data.Repositories
{
    public class ObraRepository : IObraRepository
    {
        private readonly IGeneroDeParaRepository _generoRepository;
        protected readonly ContextBase _context;

        public ObraRepository(ContextBase context, IGeneroDeParaRepository generoRepository)
        {
            _generoRepository = generoRepository;
            _context = context;
        }

        public async Task<List<Novel>> RetornaListaNovels()
        {
            var listaNovels = new List<Novel>();
            await _context.Connection.QueryAsync(RetornaQueryListaNovel(),
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
        
        public async Task<List<Comic>> RetornaListaComics()
        {
            var listaComics = new List<Comic>();
            await _context.Connection.QueryAsync(RetornaQueryListaComic(),
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


        public async Task<Novel> RetornaNovelPorId(Guid novelId)
        {
            var listaNovels = await RetornaListaNovels();
            return listaNovels.SingleOrDefault(f => f.Id == novelId);
        }

        public async Task<Comic> RetornaComicPorId(Guid comicId)
        {
            var listaComics = await RetornaListaComics();
            return listaComics.SingleOrDefault(f => f.Id == comicId);
        }


        public void AdicionaNovel(Novel novel)
        {
            _context.Add(novel);
        }

        public void AdicionaComic(Comic comic)
        {
            _context.Add(comic);
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
            _context.Remove(novel);
        }

        public void ExcluiComic(Comic comic)
        {
            _context.Remove(comic);
        }


        public async Task<Novel> RetornaNovelExistente(string titulo)
        {
            titulo = titulo.Trim();
            var sql = $@"SELECT * 
                           FROM Novels 
                          WHERE Titulo LIKE @Titulo";

            var novelExistente = await _context.Connection.QueryAsync<Novel>(sql, new { Titulo = "%" + titulo + "%" });
            return novelExistente.FirstOrDefault();
        }

        public async Task<Comic> RetornaComicExistente(string titulo)
        {
            titulo = titulo.Trim();
            var sql = $@"SELECT * 
                           FROM Comics 
                          WHERE Titulo LIKE @Titulo";

            var comicExistente = await _context.Connection.QueryAsync<Comic>(sql, new { Titulo = "%" + titulo + "%" });
            return comicExistente.FirstOrDefault();
        }


        public async Task InsereGenerosNovel(Novel novel, List<string> listaGeneros, bool inclusao)
        {
            if (inclusao)
            {
                novel.GenerosNovel = new List<GeneroNovel>();
            }
            else
            {
                foreach (var generoNovel in novel.GenerosNovel)
                {                    
                    _generoRepository.ExcluiGeneroNovel(generoNovel);
                }
            }

            var arrayGenero = listaGeneros[0]?.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (arrayGenero != null && arrayGenero.Length > 0)
            {
                foreach (var genero in arrayGenero)
                {
                    var generoEncontrado = _context.Generos.Single(s => s.Slug == genero);
                    await _generoRepository.AdicionaGeneroNovel(new GeneroNovel { NovelId = novel.Id, GeneroId = generoEncontrado.Id });
                    AlteracoesSalvas();
                }
            }
        }

        public async Task InsereGenerosComic(Comic comic, List<string> listaGeneros, bool inclusao)
        {
            if (inclusao)
            {
                comic.GenerosComic = new List<GeneroComic>();
            }
            else
            {
                foreach (var generosComic in comic.GenerosComic)
                {
                    _generoRepository.ExcluiGeneroComic(generosComic);
                }
            }

            var arrayGenero = listaGeneros[0]?.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            if (arrayGenero != null && arrayGenero.Length > 0)
            {
                foreach (var genero in arrayGenero)
                {
                    var generoEncontrado = _context.Generos.Single(s => s.Slug == genero);
                    await _generoRepository.AdicionaGeneroComic(new GeneroComic { ComicId = comic.Id, GeneroId = generoEncontrado.Id });
                    AlteracoesSalvas();
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

        public bool AlteracoesSalvas()
        {
            return _context.SaveChanges() > 0;
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
