using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Public;
using TsundokuTraducoes.Api.DTOs.Public.Retorno;
using TsundokuTraducoes.Api.Repository.Interfaces;
using static Dapper.SqlMapper;

namespace TsundokuTraducoes.Api.Repository
{
    public class InfosObrasRepository : Repository, IInfosObrasRepository
    {
        public InfosObrasRepository(TsundokuContext context) : base(context) { }


        public async Task<List<RetornoObra>> ObterListaNovels(string pesquisar, string nacionalidade, string status, string tipo, string genero)
        {
            var listaRetornoObra = new List<RetornoObra>();
            var sql = string.Empty;
            var dynamicParameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(pesquisar))
            {
                sql = RetornaSqlListaNovelsComPesquisar();                
                dynamicParameters.Add("@pesquisar", "%" + pesquisar + "%");                
            }
            else
            {
                dynamicParameters.Add("@nacionalidade", nacionalidade);
                dynamicParameters.Add("@status", status);
                dynamicParameters.Add("@tipo", tipo);
                dynamicParameters.Add("@genero", genero);

                sql = RetornaSqlListaNovelsPorParametros(nacionalidade, status, tipo, genero);
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
                         ORDER BY DataAlteracao DESC;"
            ;

            var retornoConsulta = await _contextDapper.QueryAsync<RetornoObra>(sql);
            listaRetornoObra.AddRange(retornoConsulta.ToList());

            TrataListaRetornoObra(listaRetornoObra);
            return listaRetornoObra;
        }

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

        // TODO - Será verificado se vai ser reaproveitado enquanto é trabalhado nos backlogs
        public List<DadosCapitulosDTO> ObterCapitulos()
        {
            return _contextDapper.Query<DadosCapitulosDTO>(RetornaSqlListaCapitulos()).ToList();
        }

        public List<DadosCapitulosDTO> ObterCapitulos(string pesquisar, bool ehNovel)
        {
            var condicaoConsulta = @$"WHERE O.Titulo LIKE '%{pesquisar}%' {RetornaCondicaoEhNovel(ehNovel)}";
            return _contextDapper.Query<DadosCapitulosDTO>(RetornaSqlListaCapitulos(condicaoConsulta)).ToList();
        }

        private string RetornaSqlListaCapitulos(string condicaoConsulta = "")
        {
            return @$"SELECT O.Titulo TituloObra,
	                         O.NumeroUltimoVolume DescritivoVolume,
	                         O.NumeroUltimoCapitulo DescritivoCapitulo,
	                         O.Slug SlugObra,
	                         O.SlugUltimoVolume SlugVolume,
	                         O.SlugUltimoCapitulo SlugCapitulo,
	                         O.ImagemCapaUltimoVolume UrlCapaVolume,
	                         O.Alias AliasObra,
	                         O.Autor AutorObra 
                        FROM Obra O 
                        {condicaoConsulta}
                       ORDER BY O.DataAtualizacaoUltimoCapitulo DESC";
        }

        public List<DadosCapitulosDTO> ObterCapitulos(string nacionalidade, string status, string tipo, string genero, bool ehNovel)
        {
            var condicaoConsulta = string.Empty;
            var contemGenero = false;
            var listaParametroConsulta = new List<string>();

            if (!string.IsNullOrEmpty(nacionalidade))
            {
                listaParametroConsulta.Add($"O.NacionalidadeSlug = '{nacionalidade}' ");
            }

            if (!string.IsNullOrEmpty(status))
            {
                listaParametroConsulta.Add($"O.StatusObraSlug = '{status}' ");
            }

            if (!string.IsNullOrEmpty(tipo))
            {
                listaParametroConsulta.Add($"O.TipoObraSlug = '{tipo}' ");
            }

            if (!string.IsNullOrEmpty(genero))
            {
                contemGenero = true;
                listaParametroConsulta.Add($"G.Slug = '{genero}' ");
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

            condicaoConsulta += string.IsNullOrEmpty(condicaoConsulta) ? "WHERE " + RetornaCondicaoEhNovel(ehNovel) : "AND " + RetornaCondicaoEhNovel(ehNovel);

            var capitulosRetornados = _contextDapper
                .Query<DadosCapitulosDTO>(RetornaSqlListaCapitulosPorParametros(contemGenero, condicaoConsulta))
                .ToList();

            return capitulosRetornados;
        }

        private string RetornaSqlListaCapitulosPorParametros(bool contemGenero, string condicaoConsulta)
        {
            string sql;

            if (contemGenero)
            {
                sql = @$"SELECT O.Titulo TituloObra,
                         	    O.NumeroUltimoVolume DescritivoVolume,
                                O.NumeroUltimoCapitulo DescritivoCapitulo,
                                O.Slug SlugObra,
                         	    O.SlugUltimoVolume SlugVolume,
                                O.SlugUltimoCapitulo SlugCapitulo,
                                O.ImagemCapaUltimoVolume UrlCapaVolume,
                                O.Alias AliasObra,
                                O.Autor AutorObra
                           FROM Obra O
                          INNER JOIN GeneroObra GO on GO.ObrasId = O.Id
                          INNER JOIN Generos G on G.Id = GO.GenerosId
                          {condicaoConsulta}
                          ORDER BY O.DataAtualizacaoUltimoCapitulo DESC;";
            }
            else
            {
                sql = @$"SELECT O.Titulo TituloObra,
	                            O.NumeroUltimoVolume DescritivoVolume,
                                O.NumeroUltimoCapitulo DescritivoCapitulo,
                                O.Slug SlugObra,
	                            O.SlugUltimoVolume SlugVolume,
                                O.SlugUltimoCapitulo SlugCapitulo,
                                O.ImagemCapaUltimoVolume UrlCapaVolume,
                                O.Alias AliasObra,
                                O.Autor AutorObra
                           FROM Obra O 
                          {condicaoConsulta} 
                          ORDER BY O.DataAtualizacaoUltimoCapitulo DESC;";
            }

            return sql;
        }

        private static string RetornaCondicaoEhNovel(bool ehNovel)
        {
            string condicaoEhNovel;

            if (ehNovel)
            {
                condicaoEhNovel = "O.TipoObraSlug not in ('manga', 'manhua', 'manhwa')";
            }
            else
            {
                condicaoEhNovel = "O.TipoObraSlug not in ('light-novel', 'web-novel')";
            }

            return condicaoEhNovel;
        }

        public ObraDTO ObterObraPorSlug(string slug)
        {
            var listaObraDTO = new List<ObraDTO>();

            _contextDapper.Query(RetornaSqlObraNovelPorSlug(),
                (Func<ObraDTO, GeneroDTO, VolumeDTO, CapituloDTO, ObraDTO>)((obraDTO, generoDTO, volumeDTO, capituloDTO) =>
                {
                    obraDTO = CarregaObra(obraDTO, listaObraDTO);
                    CarregaGeneros(obraDTO, generoDTO);
                    CarregaVolume(obraDTO, volumeDTO);
                    CarregaCapitulo(obraDTO, capituloDTO);

                    return obraDTO;
                }), new { slug }, splitOn: "ImagemCapaPrincipal, Genero, ImagemCapaVolume, CapituloNumero");

            _contextDapper.Query(RetornaSqlObraComicPorSlug(),
                (Func<ObraDTO, GeneroDTO, VolumeDTO, CapituloDTO, ObraDTO>)((obraDTO, generoDTO, volumeDTO, capituloDTO) =>
                {
                    obraDTO = CarregaObra(obraDTO, listaObraDTO);
                    CarregaGeneros(obraDTO, generoDTO);
                    CarregaVolume(obraDTO, volumeDTO);
                    CarregaCapitulo(obraDTO, capituloDTO);

                    return obraDTO;
                }), new { slug }, splitOn: "ImagemCapaPrincipal, Genero, ImagemCapaVolume, CapituloNumero");


            return listaObraDTO.FirstOrDefault();
        }

        private static ObraDTO CarregaObra(ObraDTO obraDTO, List<ObraDTO> listaObraDTO)
        {
            if (listaObraDTO.FirstOrDefault(w => w.SlugObra == obraDTO.SlugObra) == null)
            {
                listaObraDTO.Add(obraDTO);
            }
            else
            {
                obraDTO = listaObraDTO.SingleOrDefault(o => o.SlugObra == obraDTO.SlugObra);
            }

            return obraDTO;
        }

        private static void CarregaGeneros(ObraDTO obraDTO, GeneroDTO generoDTO)
        {
            if (obraDTO.Generos.FirstOrDefault(w => w.SlugGenero == generoDTO.SlugGenero) == null)
            {
                obraDTO.Generos.Add(generoDTO);
            }
        }

        private static void CarregaVolume(ObraDTO obraDTO, VolumeDTO volumeDTO)
        {
            if (volumeDTO != null)
            {
                if (obraDTO.Volumes.FirstOrDefault(w => w.SlugVolume == volumeDTO.SlugVolume) == null)
                {
                    obraDTO.Volumes.Add(volumeDTO);
                }
            }
        }

        private static void CarregaCapitulo(ObraDTO obraDTO, CapituloDTO capituloDTO)
        {
            if (obraDTO.Volumes.Count > 0)
            {
                if (capituloDTO != null)
                {
                    foreach (var volume in obraDTO.Volumes)
                    {
                        if (volume.CapitulosDTO.FirstOrDefault(w => w.SlugCapitulo == capituloDTO.SlugCapitulo) == null)
                        {
                            volume.CapitulosDTO.Add(capituloDTO);
                        }
                    }
                }
            }
        }

        private static string RetornaSqlObraNovelPorSlug()
        {
            return @"SELECT O.ImagemCapaPrincipal AS ImagemCapaPrincipal,
		                    O.Titulo, 
		                    O.TituloAlternativo,
		                    O.Sinopse AS SinopseObra,
		                    O.Autor AS AutorObra,
		                    O.Artista AS Ilustrador,
		                    O.Slug AS SlugObra,
		                    O.StatusObraSlug AS SlugStatus,       
		                    O.TipoObraSlug AS SlugTipoObra,
		                    G.Descricao AS Genero,
		                    G.Slug AS SlugGenero,
		                    V.ImagemVolume AS ImagemCapaVolume,
		                    V.Sinopse AS SinopseVolume,
		                    V.Slug AS SlugVolume,
		                    V.Numero AS VolumeNumero,
		                    C.Numero AS CapituloNumero,
		                    C.Titulo AS TituloCapitulo,
		                    C.Slug AS SlugCapitulo
                       FROM Obra O
                      INNER JOIN GeneroObra GO ON GO.ObraId = O.Id
                      INNER JOIN Genero G ON G.Id = GO.GeneroId
                       LEFT JOIN Volume V ON V.ObraId = O.Id
                       LEFT JOIN CapituloNovel C ON C.VolumeId = V.Id
                      WHERE O.Slug = @slug";
        }

        private static string RetornaSqlObraComicPorSlug()
        {
            return @"SELECT O.ImagemCapaPrincipal AS ImagemCapaPrincipal,
		                    O.Titulo, 
		                    O.TituloAlternativo,
		                    O.Sinopse AS SinopseObra,
		                    O.Autor AS AutorObra,
		                    O.Artista AS Ilustrador,
		                    O.Slug AS SlugObra,
		                    O.StatusObraSlug AS SlugStatus,       
		                    O.TipoObraSlug AS SlugTipoObra,
		                    G.Descricao AS Genero,
		                    G.Slug AS SlugGenero,
		                    V.ImagemVolume AS ImagemCapaVolume,
		                    V.Sinopse AS SinopseVolume,
		                    V.Slug AS SlugVolume,
		                    V.Numero AS VolumeNumero,
		                    C.Numero AS CapituloNumero,
		                    C.Titulo AS TituloCapitulo,
		                    C.Slug AS SlugCapitulo
                       FROM Obra O
                      INNER JOIN GeneroObra GO ON GO.ObraId = O.Id
                      INNER JOIN Genero G ON G.Id = GO.GeneroId
                       LEFT JOIN Volume V ON V.ObraId = O.Id
                       LEFT JOIN CapituloManga C ON C.VolumeId = V.Id
                      WHERE O.Slug = @slug";
        }

        public ConteudoCapituloNovelDTO ObterCapituloNovelPorSlug(string slug)
        {
            var sql = @"SELECT Titulo TituloCapitulo, 
                               ConteudoNovel ConteudoCapitulo,
                               Tradutor,
                               Revisor,
	                           QC,
                               Slug SlugCapitulo,
                               Id 
                         FROM CapituloNovel
                        WHERE Slug = @slug;";

            var retorno = _contextDapper.Query<ConteudoCapituloNovelDTO>(sql, new { slug });
            return retorno.FirstOrDefault();
        }

        public ConteudoCapituloComicDTO ObterCapituloComicPorSlug(string slug)
        {
            var sql = @"SELECT Id,
	                           Titulo TituloCapitulo,
                               ListaImagens	ListaImagensComic,
                               Slug SlugCapitulo
                          FROM CapituloManga
                         WHERE Slug = @slug";

            var retorno = _contextDapper.Query<ConteudoCapituloComicDTO>(sql, new { slug });
            return retorno.FirstOrDefault();
        }
    }
}
