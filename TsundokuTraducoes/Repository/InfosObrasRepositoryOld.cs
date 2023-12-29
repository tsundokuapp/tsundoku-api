using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.DTOs.Admin.Request;
using TsundokuTraducoes.Api.DTOs.Public.Retorno;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Data.Context;
using static Dapper.SqlMapper;

namespace TsundokuTraducoes.Api.Repository
{
    public class InfosObrasRepositoryOld : RepositoryOld, IInfosObrasRepositoryOld
    {
        public InfosObrasRepositoryOld(ContextBase context) : base(context) { }

        public async Task<List<RetornoObra>> ObterListaNovels(RequestObras requestObras)
        {
            var listaRetornoObra = new List<RetornoObra>();
            var sql = string.Empty;
            var dynamicParameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(requestObras.Pesquisar))
            {
                sql = RetornaSqlListaNovelsComPesquisar();                
                dynamicParameters.Add("@pesquisar", "%" + requestObras.Pesquisar + "%");                
            }
            else
            {
                dynamicParameters.Add("@nacionalidade", requestObras.Nacionalidade);
                dynamicParameters.Add("@status", requestObras.Status);
                dynamicParameters.Add("@tipo", requestObras.Tipo);
                dynamicParameters.Add("@genero", requestObras.Genero);

                sql = RetornaSqlListaNovelsPorParametros(requestObras.Nacionalidade, requestObras.Status, requestObras.Tipo, requestObras.Genero);
            }

            var retornoConsulta = await _contextDapper.QueryAsync<RetornoObra>(sql, dynamicParameters);
            listaRetornoObra.AddRange(retornoConsulta.ToList());

            TrataListaRetornoObra(listaRetornoObra);
            return listaRetornoObra;
        }

        public async Task<List<RetornoObra>> ObterListaNovelsRecentes()
        {
            var listaRetornoObra = new List<RetornoObra>();

            var sql = @"SELECT ImagemCapaPrincipal UrlCapaPrincipal,
	                           ImagemCapaUltimoVolume UrlCapaVolume,
	                           Alias,
	                           Autor,
	                           Slug,
	                           NumeroUltimoVolume DescritivoVolume,
	                           Id
                          FROM Novels
                         ORDER BY DataInclusao DESC;"
            ;

            var retornoConsulta = await _contextDapper.QueryAsync<RetornoObra>(sql);
            listaRetornoObra.AddRange(retornoConsulta.ToList());

            TrataListaRetornoObra(listaRetornoObra);
            return listaRetornoObra;
        }

        public async Task<RetornoObra> ObterNovelsPorId(RequestObras requestObras)
        {
            var listaRetornoObra = new List<RetornoObra>();

            var sql = @"SELECT ImagemCapaPrincipal UrlCapaPrincipal,
                        	   ImagemCapaUltimoVolume UrlCapaVolume,
                        	   Alias,
                        	   Autor,
                        	   Slug,
                        	   NumeroUltimoVolume DescritivoVolume,
                        	   Id
                          FROM Novels
                         WHERE Id = @IdObra;"
            ;

            var retornoConsulta = await _contextDapper.QueryAsync<RetornoObra>(sql, new { IdObra = requestObras.IdObra });
            listaRetornoObra.AddRange(retornoConsulta.ToList());

            TrataListaRetornoObra(listaRetornoObra);
            return listaRetornoObra.FirstOrDefault();
        }

        
        public async Task<List<RetornoObra>> ObterListaComics(RequestObras requestObras)
        {
            var listaRetornoObra = new List<RetornoObra>();
            var sql = string.Empty;
            var dynamicParameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(requestObras.Pesquisar))
            {
                sql = RetornaSqlListaComicsComPesquisar();
                dynamicParameters.Add("@pesquisar", "%" + requestObras.Pesquisar + "%");
            }
            else
            {
                dynamicParameters.Add("@nacionalidade", requestObras.Nacionalidade);
                dynamicParameters.Add("@status", requestObras.Status);
                dynamicParameters.Add("@tipo", requestObras.Tipo);
                dynamicParameters.Add("@genero", requestObras.Genero);

                sql = RetornaSqlListaComicsPorParametros(requestObras.Nacionalidade, requestObras.Status, requestObras.Tipo, requestObras.Genero);
            }

            var retornoConsulta = await _contextDapper.QueryAsync<RetornoObra>(sql, dynamicParameters);
            listaRetornoObra.AddRange(retornoConsulta.ToList());

            TrataListaRetornoObra(listaRetornoObra);
            return listaRetornoObra;
        }
        
        public async Task<List<RetornoObra>> ObterListaComicsRecentes()
        {
            var listaRetornoObra = new List<RetornoObra>();

            var sql = @"SELECT ImagemCapaPrincipal UrlCapaPrincipal,
	                           ImagemCapaUltimoVolume UrlCapaVolume,
	                           Alias,
	                           Autor,
	                           Slug,
	                           NumeroUltimoVolume DescritivoVolume,
	                           Id
                          FROM Comics
                         ORDER BY DataInclusao DESC;"
            ;

            var retornoConsulta = await _contextDapper.QueryAsync<RetornoObra>(sql);
            listaRetornoObra.AddRange(retornoConsulta.ToList());

            TrataListaRetornoObra(listaRetornoObra);
            return listaRetornoObra;
        }

        public async Task<RetornoObra> ObterComicPorId(RequestObras requestObras)
        {
            var listaRetornoObra = new List<RetornoObra>();

            var sql = @"SELECT ImagemCapaPrincipal UrlCapaPrincipal,
                        	   ImagemCapaUltimoVolume UrlCapaVolume,
                        	   Alias,
                        	   Autor,
                        	   Slug,
                        	   NumeroUltimoVolume DescritivoVolume,
                        	   Id
                          FROM Comics
                         WHERE Id = @IdObra;"
            ;

            var retornoConsulta = await _contextDapper.QueryAsync<RetornoObra>(sql, new { IdObra = requestObras.IdObra });
            listaRetornoObra.AddRange(retornoConsulta.ToList());

            TrataListaRetornoObra(listaRetornoObra);
            return listaRetornoObra.FirstOrDefault();
        }

        
        public async Task<List<RetornoCapitulos>> ObterCapitulosHome()
        {
            var listaRetornoCapitulo = new List<RetornoCapitulos>();

            var sql = @"SELECT CC.Id,
                               CC.Numero NumeroCapitulo,
                               CC.Parte ParteCapitulo,
                               CC.Slug SlugCapitulo,
                               CC.DataInclusao DataInclusao,
                               VC.Numero NumeroVolume,
                               VC.ImagemVolume UrlCapaVolume,
                               C.ImagemCapaPrincipal UrlCapaPrincipal,
                               C.Alias AliasObra,
                               C.Autor AutorObra
                          FROM CapitulosComic CC
                         INNER JOIN VolumesComic VC ON VC.Id = CC.VolumeId
                         INNER JOIN Comics C ON C.Id = VC.ComicId
                         UNION
                        SELECT CN.Id,
                    	       CN.Numero NumeroCapitulo,
                               CN.Parte ParteCapitulo,
                               CN.Slug SlugCapitulo,
                               CN.DataInclusao DataInclusao,
                               VN.Numero NumeroVolume,
                               VN.ImagemVolume UrlCapaVolume,
                               N.ImagemCapaPrincipal UrlCapaPrincipal,
                               N.Alias AliasObra,
                               N.Autor AutorObra
                          FROM CapitulosNovel CN
                         INNER JOIN VolumesNovel VN ON VN.Id = CN.VolumeId
                         INNER JOIN Novels N ON N.Id = VN.NovelId
                         ORDER BY DataInclusao DESC;"
            ;

            var retornoConsulta = await _contextDapper.QueryAsync<RetornoCapitulos>(sql);
            listaRetornoCapitulo.AddRange(retornoConsulta.ToList());

            TrataListaRetornoCapitulo(listaRetornoCapitulo);
            return listaRetornoCapitulo;
        }

        
        #region Métodos Auxiliares

        private static string RetornaSqlListaNovelsComPesquisar()
        {
            return @"SELECT ImagemCapaPrincipal UrlCapaPrincipal,
                            ImagemCapaUltimoVolume UrlCapaVolume,
                            Alias,
                            Autor,
                            Slug,
                            NumeroUltimoVolume DescritivoVolume,
                            Id
                       FROM Novels
                      WHERE Titulo LIKE upper(@pesquisar)";
        }

        private static string RetornaSqlListaNovelsPorParametros(string nacionalidade, string status, string tipo, string genero)
        {
            var condicaoConsulta = string.Empty;
            var joinsGeneros = string.Empty;

            var listaParametroConsulta = new List<string>();

            if (!string.IsNullOrEmpty(nacionalidade))
            {
                listaParametroConsulta.Add($"N.NacionalidadeSlug = @nacionalidade ");
            }

            if (!string.IsNullOrEmpty(status))
            {
                listaParametroConsulta.Add($"N.StatusObraSlug = @status ");
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                listaParametroConsulta.Add($"N.TipoObraSlug = @tipo ");
            }

            if (!string.IsNullOrEmpty(genero))
            {                
                listaParametroConsulta.Add($"G.Slug = @genero ");
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

            return @$"SELECT N.ImagemCapaPrincipal UrlCapaPrincipal,
	                         N.ImagemCapaUltimoVolume UrlCapaVolume,
	                         N.Alias,
	                         N.Autor,
	                         N.Slug,
	                         N.NumeroUltimoVolume DescritivoVolume,
	                         N.Id
                        FROM Novels N 
                        {joinsGeneros} 
                        {condicaoConsulta} ";
        }

        private static string RetornaSqlListaComicsComPesquisar()
        {
            return @"SELECT ImagemCapaPrincipal UrlCapaPrincipal,
                            ImagemCapaUltimoVolume UrlCapaVolume,
                            Alias,
                            Autor,
                            Slug,
                            NumeroUltimoVolume DescritivoVolume,
                            Id
                       FROM Comics
                      WHERE Titulo LIKE upper(@pesquisar)";
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
                
        private static void TrataListaRetornoObra(List<RetornoObra> listaRetornoObra)
        {
            foreach (var retornoObra in listaRetornoObra)
            {
                retornoObra.UrlCapa = !string.IsNullOrEmpty(retornoObra.UrlCapaVolume)
                    ? retornoObra.UrlCapaVolume
                    : retornoObra.UrlCapaPrincipal;

                if (string.IsNullOrEmpty(retornoObra.DescritivoVolume))
                    retornoObra.DescritivoVolume = string.Empty;

                retornoObra.UrlCapaVolume = null;
                retornoObra.UrlCapaPrincipal = null;
            }
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

        #endregion
    }
}
