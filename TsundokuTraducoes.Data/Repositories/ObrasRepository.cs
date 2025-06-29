using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.Validacao;

namespace TsundokuTraducoes.Data.Repositories
{
    public class ObrasRepository : IObrasRepository
    {
        protected readonly ContextBase _context;

        public ObrasRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<List<Novel>> ObterListaNovels(RequestObras requestObras)
        {
            var listaNovels = new List<Novel>();

            if (!string.IsNullOrEmpty(requestObras.Pesquisar))
            {                
                listaNovels = await _context.Novels
                    .AsNoTracking()
                    .Include(n => n.GenerosNovel)
                    .ThenInclude(x => x.Genero)
                    .Where(w => EF.Functions.Like(w.Titulo.ToUpper(), $"%{requestObras.Pesquisar.ToUpper()}%")).ToListAsync();
            }
            else
            {
                var parametrosVerificados = ValidacaoRequest.VerificaParametrosObras(requestObras);
                if (parametrosVerificados)
                {
                    var sql = RetornaSqlListaNovelsPorParametros(requestObras.Nacionalidade, requestObras.Status, requestObras.Tipo, requestObras.Genero);
                    listaNovels = await _context.Novels
                        .FromSqlRaw(sql)
                        .Include(n => n.GenerosNovel)
                        .ThenInclude(x => x.Genero)
                        .ToListAsync();
                }
                else
                {
                    listaNovels = await _context.Novels
                        .AsNoTracking()
                        .Include(n => n.GenerosNovel)
                        .ThenInclude(x => x.Genero)
                        .ToListAsync();
                }
            }

            return listaNovels;
        }

        public async Task<List<Comic>> ObterListaComics(RequestObras requestObras)
        {
            var listaComics = new List<Comic>();

            if (!string.IsNullOrEmpty(requestObras.Pesquisar))
            {
                listaComics = await _context.Comics.AsNoTracking().Where(w => EF.Functions.Like(w.Titulo.ToUpper(), $"%{requestObras.Pesquisar.ToUpper()}%")).ToListAsync();
            }
            else
            {
                var parametrosVerificados = ValidacaoRequest.VerificaParametrosObras(requestObras);
                if (parametrosVerificados)
                {
                    var sql = RetornaSqlListaComicsPorParametros(requestObras.Nacionalidade, requestObras.Status, requestObras.Tipo, requestObras.Genero);
                    listaComics = await _context.Comics
                        .FromSqlRaw(sql)
                        .Include(x => x.GenerosComic)
                        .ThenInclude(x => x.Genero)
                        .ToListAsync();
                }
                else
                {
                    listaComics = await _context.Comics
                        .Include(x => x.GenerosComic)
                        .ThenInclude(x => x.Genero)
                        .ToListAsync();
                }
            }

            return listaComics;
        }
        
        
        public async Task<List<Novel>> ObterListaNovelsRecomendadas()
        {
            return await _context.Novels.AsNoTracking().Where(w => w.EhRecomendacao == true).ToListAsync();
        }

        public async Task<List<Comic>> ObterListaComicsRecomendadas()
        {
            return await _context.Comics.AsNoTracking().Where(w => w.EhRecomendacao == true).ToListAsync();
        }

        
        public async Task<List<Novel>> ObterListaNovelsRecentes()
        {
            return await _context
                .Novels
                .AsNoTracking()
                .Where(x => x.Volumes.Where(x => x.ListaCapitulo.Count != 0).Any())
                .OrderByDescending(o => o.DataAtualizacaoUltimoCapitulo)
                .ToListAsync();
        }
        
        public async Task<List<Comic>> ObterListaComicsRecentes()
        {   
            return await _context
                .Comics
                .AsNoTracking()
                .Where(x => x.Volumes.Where(x => x.ListaCapitulo.Count != 0).Any())
                .OrderByDescending(o => o.DataAtualizacaoUltimoCapitulo)
                .ToListAsync();
        }


        public async Task<Novel> ObterNovelPorId(Guid id)
        {
            return await _context
                .Novels
                .AsNoTracking()
                .Include(n => n.GenerosNovel)
                .ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Novel> ObterNovelPorSlug(string slug)
        {
            return await _context
                .Novels
                .AsNoTracking()
                .Include(n => n.GenerosNovel)
                .ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(w => w.Slug == slug);
        }

        public async Task<Comic> ObterComicPorId(Guid id)
        {
            return await _context
                .Comics
                .AsNoTracking()
                .Include(x => x.GenerosComic)
                .ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<Comic> ObterComicPorSlug(string slug)
        {
            return await _context
                .Comics
                .AsNoTracking()
                .Include(x => x.GenerosComic)
                .ThenInclude(x => x.Genero)
                .FirstOrDefaultAsync(x => x.Slug == slug);
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
                                   IdObra = comics.Id,
                                   NumeroCapitulo = capitulosComic.Numero,
                                   ParteCapitulo = capitulosComic.Parte,
                                   SlugCapitulo = capitulosComic.Slug,
                                   capitulosComic.DataInclusao,
                                   NumeroVolume = volumesComic.Numero,
                                   UrlCapaVolume = volumesComic.ImagemVolume,
                                   UrlCapaPrincipal = comics.ImagemCapaPrincipal,
                                   AliasObra = comics.Alias,
                                   AutorObra = comics.Autor,
                                   TipoObra = comics.TipoObra,
                                   SlugObra = comics.Slug
                                   
                               })
                        .Union(from capitulosNovel in _context.CapitulosNovel.AsNoTracking()
                               join volumesNovel in _context.VolumesNovel.AsNoTracking()
                                   on capitulosNovel.VolumeId equals volumesNovel.Id
                               join novels in _context.Novels.AsNoTracking()
                                   on volumesNovel.NovelId equals novels.Id
                               select new
                               {
                                   IdObra = novels.Id,
                                   NumeroCapitulo = capitulosNovel.Numero,
                                   ParteCapitulo = capitulosNovel.Parte,
                                   SlugCapitulo = capitulosNovel.Slug,
                                   capitulosNovel.DataInclusao,
                                   NumeroVolume = volumesNovel.Numero,
                                   UrlCapaVolume = volumesNovel.ImagemVolume,
                                   UrlCapaPrincipal = novels.ImagemCapaPrincipal,
                                   AliasObra = novels.Alias,
                                   AutorObra = novels.Autor,
                                   TipoObra = novels.TipoObra,
                                   SlugObra = novels.Slug
                               }
                        );

            var listaRetornoCapitulos = await query
                .Select(rc => new RetornoCapitulosHome
                    {
                        IdObra = rc.IdObra,
                        NumeroCapitulo = rc.NumeroCapitulo,
                        ParteCapitulo = rc.ParteCapitulo,
                        SlugCapitulo = rc.SlugCapitulo,
                        DataInclusao = rc.DataInclusao,
                        NumeroVolume = rc.NumeroVolume,
                        UrlCapaVolume = rc.UrlCapaVolume,
                        UrlCapaPrincipal = rc.UrlCapaPrincipal,
                        AliasObra = rc.AliasObra,
                        AutorObra = rc.AutorObra,
                        TipoObra = rc.TipoObra,
                        SlugObra = rc.SlugObra
                    })
                .OrderByDescending(o => o.DataInclusao)
                .ToListAsync();

            var listaTratada = TrataListaRetornoCapitulo(listaRetornoCapitulos);
            var retornoListaCapitulosHome = listaTratada.Take(20);
            return [.. retornoListaCapitulosHome];
        }

        private static string RetornaSqlListaNovelsPorParametros(string nacionalidade, string status, string tipo, string genero)
        {
            var condicaoConsulta = string.Empty;
            var joinsGeneros = string.Empty;

            var listaParametroConsulta = new List<string>();

            if (!string.IsNullOrEmpty(nacionalidade))
            {
                listaParametroConsulta.Add($"N.Nacionalidade = '{nacionalidade}' ");
            }

            if (!string.IsNullOrEmpty(status))
            {
                listaParametroConsulta.Add($"N.StatusObra = '{status}' ");
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                listaParametroConsulta.Add($"N.TipoObra = '{tipo}' ");
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
                listaParametroConsulta.Add($"C.Nacionalidade = '{nacionalidade}' ");
            }

            if (!string.IsNullOrEmpty(status))
            {
                listaParametroConsulta.Add($"C.StatusObra = '{status}' ");
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                listaParametroConsulta.Add($"C.TipoObra = '{tipo}' ");
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
        
        public static RetornoNovel TrataRetornoNovelUnica(Novel obra)
        {
            return new RetornoNovel()
            {
                UrlCapa = !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                    ? obra.ImagemCapaUltimoVolume
                    : obra.ImagemCapaPrincipal,
                
                Titulo = obra.Titulo,
                TituloAlternativo = obra.TituloAlternativo,
                TipoObra = obra.TipoObra,
                Alias = obra.Alias,
                Autor = obra.Autor,
                Artista = obra.Artista,
                Ano = obra.Ano,
                Visualizacoes = obra.Visualizacoes,
                Sinopse = obra.Sinopse,
                EhRecomdacao = obra.EhRecomendacao,
                EhObraMaiorIdade = obra.EhObraMaiorIdade,
                DescritivoVolume = obra.NumeroUltimoVolume,
                Slug = obra.Slug,
                Id = obra.Id,
                Nacionalidade = obra.Nacionalidade,
                StatusObra = obra.StatusObra,
                Observacao = obra.Observacao,
                ListaGeneros = TrataRetornoListaGeneros(obra.GenerosNovel),
            };
        }

        public static RetornoComic TrataRetornoComicUnica(Comic obra)
        {
            return new RetornoComic()
            {
                UrlCapa = !string.IsNullOrEmpty(obra.ImagemCapaUltimoVolume)
                    ? obra.ImagemCapaUltimoVolume
                    : obra.ImagemCapaPrincipal,
                
                Titulo = obra.Titulo,
                TituloAlternativo = obra.TituloAlternativo,
                TipoObra = obra.TipoObra,
                Alias = obra.Alias,
                Autor = obra.Autor,
                Artista = obra.Artista,
                Ano = obra.Ano,
                Visualizacoes = obra.Visualizacoes,
                Sinopse = obra.Sinopse,
                EhRecomdacao = obra.EhRecomendacao,
                EhObraMaiorIdade = obra.EhObraMaiorIdade,
                DescritivoVolume = obra.NumeroUltimoVolume,
                Slug = obra.Slug,
                Id = obra.Id,
                Nacionalidade = obra.Nacionalidade,
                StatusObra = obra.StatusObra,
                Observacao = obra.Observacao,
            };
        }

        private static List<RetornoCapitulosHome> TrataListaRetornoCapitulo(List<RetornoCapitulosHome> listaRetornoCapitulos)
        {
            var listaTratada = listaRetornoCapitulos
                .GroupBy(g => new { g.IdObra })
                .Select(s => s.OrderByDescending(o => o.DataInclusao).First())
                .ToList();
            
            foreach (var retornoCapitulo in listaTratada)
            {
                retornoCapitulo.UrlCapa = !string.IsNullOrEmpty(retornoCapitulo.UrlCapaVolume)
                    ? retornoCapitulo.UrlCapaVolume
                    : retornoCapitulo.UrlCapaPrincipal;

                if (string.IsNullOrEmpty(retornoCapitulo.ParteCapitulo))
                    retornoCapitulo.ParteCapitulo = string.Empty;

                retornoCapitulo.UrlCapaVolume = null;
                retornoCapitulo.UrlCapaPrincipal = null;
            }

            return listaTratada;
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

        public async Task<CapituloComic> ObterCapituloComicPorId(Guid id)
        {
            var capitulo = await _context.CapitulosComic
                                    .AsNoTracking()
                                    .Where(w => w.Id == id).FirstOrDefaultAsync();

            return capitulo;
        }

        public async Task<CapituloNovel> ObterCapituloNovelPorId(Guid id)
        {
            var capitulo = await _context.CapitulosNovel
                                    .AsNoTracking()
                                    .Where(w => w.Id == id).FirstOrDefaultAsync();

            return capitulo;
        }

        public static List<string> TrataRetornoListaGeneros(List<GeneroNovel> generosNovel)
        {
            var listaGeneros = new List<string>();

            generosNovel.ForEach((genero) =>
            {
                listaGeneros.Add(genero.Genero.Descricao);
            });

            return listaGeneros;
        }

        public async Task<List<RetornoObrasPesquisa>> ObterObrasPesquisa(string obra)
        {
            var query = (from comics in _context.Comics.AsNoTracking()
                         where EF.Functions.Like(comics.Titulo.ToUpper(), $"%{obra.ToUpper()}%")
                         || EF.Functions.Like(comics.TituloAlternativo.ToUpper(), $"%{obra.ToUpper()}%")
                         select new
                         {
                             comics.Id,
                             comics.Slug,
                             comics.Titulo,
                             comics.Alias,
                             Capa = comics.ImagemCapaUltimoVolume ?? comics.ImagemCapaPrincipal,
                             Tipo = comics.TipoObra,
                             comics.Sinopse
                         })
                        .Union(from novels in _context.Novels.AsNoTracking()
                               where EF.Functions.Like(novels.Titulo.ToUpper(), $"%{obra.ToUpper()}%")
                               || EF.Functions.Like(novels.TituloAlternativo.ToUpper(), $"%{obra.ToUpper()}%")
                               select new
                               {
                                   novels.Id,
                                   novels.Slug,
                                   novels.Titulo,
                                   novels.Alias,
                                   Capa = novels.ImagemCapaUltimoVolume ?? novels.ImagemCapaPrincipal,
                                   Tipo = novels.TipoObra,
                                   novels.Sinopse
                               }
                        );

            var listaRetornoObrasPesquisa = await query
                .Select(rc => new RetornoObrasPesquisa
                {
                    Id = rc.Id,
                    Slug = rc.Slug,
                    Titulo = rc.Titulo,
                    Alias = rc.Alias,
                    Capa = rc.Capa,
                    Tipo = rc.Tipo,
                    Sinopse = rc.Sinopse
                })
                .ToListAsync();

            return listaRetornoObrasPesquisa;
        }
    }
}