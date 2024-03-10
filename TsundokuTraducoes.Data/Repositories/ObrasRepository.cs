using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Data.Repositories
{
    public class ObrasRepository : IObrasRepository
    {
        protected readonly ContextBase _context;

        public ObrasRepository(ContextBase context)
        {
            _context = context;
        }

        public async Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras)
        {
            var listaNovels = new List<Novel>();

            if (!string.IsNullOrEmpty(requestObras.Pesquisar))
            {                
                listaNovels = await _context.Novels.Where(w => EF.Functions.Like(w.Titulo.ToUpper(), $"%{requestObras.Pesquisar.ToUpper()}%")).ToListAsync();
            }
            else
            {
                var sql = RetornaSqlListaNovelsPorParametros(requestObras.Nacionalidade, requestObras.Status, requestObras.Tipo, requestObras.Genero);
                listaNovels = await _context.Novels.FromSqlRaw(sql).ToListAsync();
            }

            return TrataListaRetornoNovel(listaNovels);
        }

        public async Task<List<RetornoObra>> ObterListaNovelsRecentes()
        {
            var listaNovels = await _context.Novels.OrderByDescending(o => o.DataInclusao).ToListAsync();
            return TrataListaRetornoNovel(listaNovels);
        }

        public async Task<RetornoObra> ObterNovelPorId(RequestObras requestObras)
        {
            var novel = await _context.Novels.FirstOrDefaultAsync(w => w.Id.ToString() == requestObras.IdObra);

            if (novel != null)            
                return TrataRetornoNovel(novel);

            return null;
        }

        /***********************************************************************************************/

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
                listaParametroConsulta.Add($"C.NacionalidadeSlug = @nacionalidade ");
            }

            if (!string.IsNullOrEmpty(status))
            {
                listaParametroConsulta.Add($"C.StatusObraSlug = @status ");
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                listaParametroConsulta.Add($"C.TipoObraSlug = @tipo ");
            }

            if (!string.IsNullOrEmpty(genero))
            {
                listaParametroConsulta.Add($"G.Slug = @genero ");
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

            return @$"SELECT C.ImagemCapaPrincipal UrlCapaPrincipal,
	                         C.ImagemCapaUltimoVolume UrlCapaVolume,
	                         C.Alias,
	                         C.Autor,
	                         C.Slug,
	                         C.NumeroUltimoVolume DescritivoVolume,
	                         C.Id
                        FROM Comics C 
                        {joinsGeneros} 
                        {condicaoConsulta} ";
        }


        private static List<RetornoObra> TrataListaRetornoNovel(List<Novel> listaNovels)
        {
            var listaRetornoObra = new List<RetornoObra>();

            foreach (var obra in listaNovels)
            {
                listaRetornoObra.Add(TrataRetornoNovel(obra));
            }

            return listaRetornoObra;
        }

        private static RetornoObra TrataRetornoNovel(Novel obra)
        {
            return new RetornoObra
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

        private static void TrataListaRetornoCapitulo(List<RetornoCapitulos> listaRetornoCapitulo)
        {
            foreach (var retornoCapitulo in listaRetornoCapitulo)
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
    }
}
