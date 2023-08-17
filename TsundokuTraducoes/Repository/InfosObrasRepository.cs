using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Public;
using TsundokuTraducoes.Api.Repository.Interfaces;

namespace TsundokuTraducoes.Api.Repository
{
    public class InfosObrasRepository : IInfosObrasRepository
    {
        private readonly IDbConnection _contextDapper;

        public InfosObrasRepository()
        {
            _contextDapper = new TsundokuContextDapper().RetornaSqlConnetionDapper();
        }

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
