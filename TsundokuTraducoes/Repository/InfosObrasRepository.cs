using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.DTOs.Public;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Utilidades;
using TsundokuTraducoes.Models;

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
                             O.ImagemUltimoVolume UrlCapaVolume,
                             O.Alias AliasObra,
                             O.AutorObra 
                        FROM Obras O 
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
                                O.ImagemUltimoVolume UrlCapaVolume,
                                O.Alias AliasObra,
                                O.AutorObra 
                           FROM Obras O
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
                                O.ImagemUltimoVolume UrlCapaVolume,
                                O.Alias AliasObra,
                                O.AutorObra 
                           FROM Obras O 
                          {condicaoConsulta} 
                          ORDER BY O.DataAtualizacaoUltimoCapitulo DESC;";
            }

            return sql;
        }

        private static string RetornaCondicaoEhNovel(bool ehNovel)
        {
            var condicaoEhNovel = string.Empty;

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

            _contextDapper.Query(RetornaSqlObraPorSlug(),
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
            if (obraDTO.GenerosDTO.FirstOrDefault(w => w.SlugGenero == generoDTO.SlugGenero) == null)
            {
                obraDTO.GenerosDTO.Add(generoDTO);
            }
        }

        private static void CarregaVolume(ObraDTO obraDTO, VolumeDTO volumeDTO)
        {
            if (volumeDTO != null)
            {
                if (obraDTO.VolumesDTO.FirstOrDefault(w => w.SlugVolume == volumeDTO.SlugVolume) == null)
                {
                    obraDTO.VolumesDTO.Add(volumeDTO);
                }
            }
        }

        private static void CarregaCapitulo(ObraDTO obraDTO, CapituloDTO capituloDTO)
        {
            if (obraDTO.VolumesDTO.Count > 0)
            {
                if (capituloDTO != null)
                {
                    foreach (var volume in obraDTO.VolumesDTO)
                    {
                        if (volume.CapitulosDTO.FirstOrDefault(w => w.SlugCapitulo == capituloDTO.SlugCapitulo) == null)
                        {
                            volume.CapitulosDTO.Add(capituloDTO);
                        }
                    }
                }
            }
        }

        private static string RetornaSqlObraPorSlug()
        {
            return @"SELECT O.EnderecoUrlCapa AS ImagemCapaPrincipal,
                       	    O.Titulo, 
                            O.TituloAlternativo,
                            O.Sinopse AS SinopseObra,
                            O.AutorObra,
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
                       FROM Obras O
                      INNER JOIN GeneroObra GO ON GO.ObrasId = O.Id
                      INNER JOIN Generos G ON G.Id = GO.GenerosId
                       LEFT JOIN Volumes V ON V.ObraId = O.Id
                       LEFT JOIN Capitulos C ON C.VolumeId = V.Id
                      WHERE O.Slug = @slug";
        }

        public ConteudoCapituloNovelDTO ObterCapituloPorSlug(string slug)
        {
            return new ConteudoCapituloNovelDTO();
        }
    }
}
