﻿using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;

namespace TsundokuTraducoes.Data.Repositories
{
    public class ObrasRepository : IObrasRepository
    {
        protected readonly ContextBase _context;

        public ObrasRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<List<RetornoObras>> ObterListaNovels(RequestObras requestObras)
        {
            var listaNovels = new List<Novel>();

            if (!string.IsNullOrEmpty(requestObras.Pesquisar))
            {                
                listaNovels = await _context.Novels.AsNoTracking().Where(w => EF.Functions.Like(w.Titulo.ToUpper(), $"%{requestObras.Pesquisar.ToUpper()}%")).ToListAsync();
            }
            else
            {
                var sql = RetornaSqlListaNovelsPorParametros(requestObras.Nacionalidade, requestObras.Status, requestObras.Tipo, requestObras.Genero);
                listaNovels = await _context.Novels.FromSqlRaw(sql).ToListAsync();
            }

            return TrataListaRetornoNovel(listaNovels);
        }

        public async Task<List<RetornoObras>> ObterListaComics(RequestObras requestObras)
        {
            var listaComics = new List<Comic>();

            if (!string.IsNullOrEmpty(requestObras.Pesquisar))
            {
                listaComics = await _context.Comics.AsNoTracking().Where(w => EF.Functions.Like(w.Titulo.ToUpper(), $"%{requestObras.Pesquisar.ToUpper()}%")).ToListAsync();
            }
            else
            {
                var sql = RetornaSqlListaComicsPorParametros(requestObras.Nacionalidade, requestObras.Status, requestObras.Tipo, requestObras.Genero);
                listaComics = await _context.Comics.FromSqlRaw(sql).ToListAsync();
            }

            return TrataListaRetornoComic(listaComics);
        }
        
        
        public async Task<List<RetornoObras>> ObterListaNovelsRecentes()
        {
            var listaNovels = await _context.Novels.AsNoTracking().OrderByDescending(o => o.DataInclusao).ToListAsync();
            return TrataListaRetornoNovel(listaNovels);
        }
        
        public async Task<List<RetornoObras>> ObterListaComicsRecentes()
        {
            var listaComics = await _context.Comics.AsNoTracking().OrderByDescending(o => o.DataInclusao).ToListAsync();
            return TrataListaRetornoComic(listaComics);
        }

        
        public async Task<RetornoObras> ObterNovelPorId(RequestObras requestObras)
        {
            var novel = await _context.Novels.AsNoTracking().FirstOrDefaultAsync(w => w.Id.ToString() == requestObras.IdObra);

            if (novel != null)            
                return TrataRetornoNovel(novel);

            return null;
        }

        public async Task<RetornoObras> ObterComicPorId(RequestObras requestObras)
        {
            var comic = await _context.Comics.AsNoTracking().FirstOrDefaultAsync(w => w.Id.ToString() == requestObras.IdObra);

            if (comic != null)
                return TrataRetornoComic(comic);

            return null;
        }
        
        
        public async Task<List<RetornoCapitulosHome>> ObterCapitulosHome()
        {
            var query = (from capitulosComic in _context.CapitulosComic.AsNoTracking()
                               join volumesComic in _context.VolumesComic.AsNoTracking()
                                   on capitulosComic.VolumeId equals volumesComic.Id
                               join comics in _context.Comics.AsNoTracking()
                                   on volumesComic.ComicId equals comics.Id
                               select new
                               {
                                   NumeroCapitulo = capitulosComic.Numero,
                                   ParteCapitulo = capitulosComic.Parte,
                                   SlugCapitulo = capitulosComic.Slug,
                                   capitulosComic.DataInclusao,
                                   NumeroVolume = volumesComic.Numero,
                                   UrlCapaVolume = volumesComic.ImagemVolume,
                                   UrlCapaPrincipal = comics.ImagemCapaPrincipal,
                                   AliasObra = comics.Alias,
                                   AutorObra = comics.Autor,
                               })
                        .Union(from capitulosNovel in _context.CapitulosNovel.AsNoTracking()
                               join volumesNovel in _context.VolumesNovel.AsNoTracking()
                                   on capitulosNovel.VolumeId equals volumesNovel.Id
                               join novels in _context.Novels.AsNoTracking()
                                   on volumesNovel.NovelId equals novels.Id
                               select new
                               {
                                   NumeroCapitulo = capitulosNovel.Numero,
                                   ParteCapitulo = capitulosNovel.Parte,
                                   SlugCapitulo = capitulosNovel.Slug,
                                   capitulosNovel.DataInclusao,
                                   NumeroVolume = volumesNovel.Numero,
                                   UrlCapaVolume = volumesNovel.ImagemVolume,
                                   UrlCapaPrincipal = novels.ImagemCapaPrincipal,
                                   AliasObra = novels.Alias,
                                   AutorObra = novels.Autor,
                               }
                        );

            var listaRetornoCapitulos = await query
                .Select(rc => new RetornoCapitulosHome
                    {
                        NumeroCapitulo = rc.NumeroCapitulo,
                        ParteCapitulo = rc.ParteCapitulo,
                        SlugCapitulo = rc.SlugCapitulo,
                        DataInclusao = rc.DataInclusao,
                        NumeroVolume = rc.NumeroVolume,
                        UrlCapaVolume = rc.UrlCapaVolume,
                        UrlCapaPrincipal = rc.UrlCapaPrincipal,
                        AliasObra = rc.AliasObra,
                        AutorObra = rc.AutorObra
                    })
                .OrderByDescending(o => o.DataInclusao)
                .ToListAsync();

            TrataListaRetornoCapitulo(listaRetornoCapitulos);
            return listaRetornoCapitulos;
        }

        public async Task<List<RetornoObrasRecomendadas>> ObterObrasRecomendadas()
        {
            var query = (from comics in _context.Comics.AsNoTracking()
                         where comics.EhRecomendacao == true
                         select new
                         {
                             Titulo = comics.Alias,
                             Capa = !string.IsNullOrEmpty(comics.ImagemCapaUltimoVolume) ? comics.ImagemCapaUltimoVolume : comics.ImagemCapaPrincipal,
                             SlugObra = comics.Slug,
                             Sinopse = comics.Sinopse
                         })
                        .Union(from novels in _context.Novels.AsNoTracking()
                               where novels.EhRecomendacao == true
                               select new
                               {
                                   Titulo = novels.Alias,
                                   Capa = !string.IsNullOrEmpty(novels.ImagemCapaUltimoVolume) ? novels.ImagemCapaUltimoVolume : novels.ImagemCapaPrincipal,
                                   SlugObra = novels.Slug,
                                   Sinopse = novels.Sinopse
                               }
                        );

            var listaRetornoObrasRecomendadas = await query
                .Select(ror => new RetornoObrasRecomendadas
                {
                    Titulo = ror.Titulo,
                    Capa = ror.Capa,
                    SlugObra = ror.SlugObra,
                    Sinopse = ror.Sinopse
                })
                .ToListAsync();

            return listaRetornoObrasRecomendadas;
        }


        private static string RetornaSqlListaNovelsPorParametros(string nacionalidade, string status, string tipo, string genero)
        {
            var condicaoConsulta = string.Empty;
            var joinsGeneros = string.Empty;

            var listaParametroConsulta = new List<string>();

            if (!string.IsNullOrEmpty(nacionalidade))
            {
                listaParametroConsulta.Add($"N.NacionalidadeSlug = '{nacionalidade}' ");
            }

            if (!string.IsNullOrEmpty(status))
            {
                listaParametroConsulta.Add($"N.StatusObraSlug = '{status}' ");
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                listaParametroConsulta.Add($"N.TipoObraSlug = '{tipo}' ");
            }

            if (!string.IsNullOrEmpty(genero))
            {
                listaParametroConsulta.Add($"G.Slug = '{genero}' ");
                joinsGeneros = @"INNER JOIN GenerosNovel GN ON GN.NovelId = N.Id
                                 INNER JOIN Generos G ON G.Id = GN.GeneroId ";
            }

            for (int indice = 0; indice < listaParametroConsulta.Count; indice++)
            {
                if (indice == 0)
                {
                    condicaoConsulta = $"WHERE {listaParametroConsulta[indice]} ";
                }
                else
                {
                    condicaoConsulta += $"AND {listaParametroConsulta[indice]} ";
                }
            }

            return @$"SELECT N.*
                        FROM Novels N
                        {joinsGeneros} 
                        {condicaoConsulta} ";
        }
        
        private static string RetornaSqlListaComicsPorParametros(string nacionalidade, string status, string tipo, string genero)
        {
            var condicaoConsulta = string.Empty;
            var joinsGeneros = string.Empty;

            var listaParametroConsulta = new List<string>();

            if (!string.IsNullOrEmpty(nacionalidade))
            {
                listaParametroConsulta.Add($"C.NacionalidadeSlug = '{nacionalidade}' ");
            }

            if (!string.IsNullOrEmpty(status))
            {
                listaParametroConsulta.Add($"C.StatusObraSlug = '{status}' ");
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                listaParametroConsulta.Add($"C.TipoObraSlug = '{tipo}' ");
            }

            if (!string.IsNullOrEmpty(genero))
            {
                listaParametroConsulta.Add($"G.Slug = '{genero}' ");
                joinsGeneros = @"INNER JOIN GenerosComic GC ON GC.ComicId = C.Id
                                 INNER JOIN Generos G ON G.Id = GC.GeneroId ";
            }

            for (int indice = 0; indice < listaParametroConsulta.Count; indice++)
            {
                if (indice == 0)
                {
                    condicaoConsulta = $"WHERE {listaParametroConsulta[indice]} ";
                }
                else
                {
                    condicaoConsulta += $"AND {listaParametroConsulta[indice]} ";
                }
            }

            return @$"SELECT C.*
                        FROM Comics C 
                        {joinsGeneros} 
                        {condicaoConsulta} ";
        }
        

        private static List<RetornoObras> TrataListaRetornoNovel(List<Novel> listaNovels)
        {
            var listaRetornoObra = new List<RetornoObras>();

            foreach (var obra in listaNovels)
            {
                listaRetornoObra.Add(TrataRetornoNovel(obra));
            }

            return listaRetornoObra;
        }
        
        private static List<RetornoObras> TrataListaRetornoComic(List<Comic> listaNovels)
        {
            var listaRetornoObra = new List<RetornoObras>();

            foreach (var obra in listaNovels)
            {
                listaRetornoObra.Add(TrataRetornoComic(obra));
            }

            return listaRetornoObra;
        }
        
        
        private static RetornoObras TrataRetornoNovel(Novel obra)
        {
            return new RetornoObras
            {
                UrlCapa = !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                ? obra.ImagemCapaUltimoVolume
                : obra.ImagemCapaPrincipal,

                Alias = obra.Alias,
                Autor = obra.Autor,
                DescritivoVolume = obra.NumeroUltimoVolume,
                Slug = obra.Slug,
                Id = obra.Id
            };
        }

        private static RetornoObras TrataRetornoComic(Comic obra)
        {
            return new RetornoObras
            {
                UrlCapa = !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                ? obra.ImagemCapaUltimoVolume
                : obra.ImagemCapaPrincipal,

                Alias = obra.Alias,
                Autor = obra.Autor,
                DescritivoVolume = obra.NumeroUltimoVolume,
                Slug = obra.Slug,
                Id = obra.Id
            };
        }

        private static void TrataListaRetornoCapitulo(List<RetornoCapitulosHome> listaRetornoCapitulos)
        {
            foreach (var retornoCapitulo in listaRetornoCapitulos)
            {
                retornoCapitulo.UrlCapa = !string.IsNullOrEmpty(retornoCapitulo.UrlCapaVolume)
                    ? retornoCapitulo.UrlCapaVolume
                    : retornoCapitulo.UrlCapaPrincipal;

                if (string.IsNullOrEmpty(retornoCapitulo.ParteCapitulo))
                    retornoCapitulo.ParteCapitulo = string.Empty;

                retornoCapitulo.UrlCapaVolume = null;
                retornoCapitulo.UrlCapaPrincipal = null;
            }
        }

        public List<RetornoVolumes> ObterListaVolumeCapitulos(string idObra)
        {
            var listaRetornoVolume = (from volumesComic in _context.VolumesComic.AsNoTracking()
                                             select new RetornoVolumes()
                                             {
                                                 Id = volumesComic.Id,
                                                 IdObra = volumesComic.ComicId,
                                                 NumeroVolume = volumesComic.Numero,
                                                 SlugVolume = volumesComic.Slug,
                                                 UrlCapaVolume = volumesComic.ImagemVolume,
                                                 DataInclusao = volumesComic.DataInclusao,
                                                 Sinopse = volumesComic.Sinopse,

                                                 ListaCapitulos =
                                                 (
                                                     from capitulo in _context.CapitulosComic.AsNoTracking()
                                                     orderby capitulo.DataInclusao
                                                     where capitulo.VolumeId == volumesComic.Id
                                                     select new RetornoCapitulos()
                                                     {
                                                         Id = capitulo.Id,
                                                         IdVolume = capitulo.VolumeId,
                                                         DataInclusao = capitulo.DataInclusao,
                                                         NumeroCapitulo = capitulo.Numero,
                                                         ParteCapitulo = capitulo.Parte,
                                                         SlugCapitulo = capitulo.Slug,
                                                         TituloCapitulo = capitulo.Titulo
                                                     }
                                                 )
                                                 .OrderByDescending(o => o.DataInclusao)
                                                 .ToList()
                                             })
                                      .AsEnumerable()
                                      .Union(from volumesNovel in _context.VolumesNovel.AsNoTracking()
                                             select new RetornoVolumes()
                                             {
                                                 Id = volumesNovel.Id,
                                                 IdObra = volumesNovel.NovelId,
                                                 NumeroVolume = volumesNovel.Numero,
                                                 SlugVolume = volumesNovel.Slug,
                                                 UrlCapaVolume = volumesNovel.ImagemVolume,
                                                 DataInclusao = volumesNovel.DataInclusao,
                                                 Sinopse = volumesNovel.Sinopse,

                                                 ListaCapitulos =
                                                 (
                                                     from capitulo in _context.CapitulosNovel.AsNoTracking()
                                                     orderby capitulo.DataInclusao
                                                     where capitulo.VolumeId == volumesNovel.Id
                                                     select new RetornoCapitulos()
                                                     {
                                                         Id = capitulo.Id,
                                                         IdVolume = capitulo.VolumeId,
                                                         DataInclusao = capitulo.DataInclusao,
                                                         NumeroCapitulo = capitulo.Numero,
                                                         ParteCapitulo = capitulo.Parte,
                                                         SlugCapitulo = capitulo.Slug,
                                                         TituloCapitulo = capitulo.Titulo
                                                     }
                                                 )
                                                 .OrderByDescending(o => o.DataInclusao)
                                                 .ToList()
                                             })
                                          .Where(w => w.IdObra.ToString() == idObra)
                                          .AsEnumerable()
                                          .OrderByDescending(o => o.DataInclusao);

            return listaRetornoVolume.ToList();
        }
    }
}