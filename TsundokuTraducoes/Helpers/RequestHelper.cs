using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin.Request;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno.Response;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno.Response;
using TsundokuTraducoes.Helpers.Validacao;

namespace TsundokuTraducoes.Api.Helpers
{
    public static class RequestHelper
    {
        public static object CriarObjetoRetono<T>(HttpContext httpContext, List<T> listaGenerica, int? skip, int? take)
        {
            var itensPorPagina = ValidacaoRequest.RetornaTakeTratado(take);
            var itensPulados = ValidacaoRequest.RetornaSkipTratado(skip, itensPorPagina);

            var dados = listaGenerica.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = listaGenerica.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        public static object CriarObjetoRetonoVolume(HttpContext httpContext, List<RetornoVolume> listaRetornoVolume, RequestVolume requestVolume)
        {
            var itensPorPagina = ValidacaoRequest.RetornaTakeTratado(requestVolume.Take);
            var itensPulados = ValidacaoRequest.RetornaSkipTratado(requestVolume.Skip, itensPorPagina);

            var dados = listaRetornoVolume.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = listaRetornoVolume.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        public static object CriarObjetoRetonoCapitulo(HttpContext httpContext, List<RetornoCapitulo> ListaRetornoCapitulo, RequestCapitulo requestCapitulo)
        {
            var itensPorPagina = ValidacaoRequest.RetornaTakeTratado(requestCapitulo.Take);
            var itensPulados = ValidacaoRequest.RetornaSkipTratado(requestCapitulo.Skip, itensPorPagina);

            var dados = ListaRetornoCapitulo.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = ListaRetornoCapitulo.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        internal static object CriarObjetoRetonoObras<T>(HttpContext httpContext, List<T> listaGenerica, RequestObras requestObras, bool ehHome = false)
        {
            var itensPorPagina = ValidacaoRequest.RetornaTakeTratado(requestObras.Take);
            var itensPulados = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip, itensPorPagina);

            var dados = listaGenerica.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = listaGenerica.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url);
            proxima = proxima != null ? proxima + RetornaQuery(requestObras) : null;

            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);
            anterior = anterior != null ? anterior + RetornaQuery(requestObras) : null;

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        internal static object CriarObjetoRetonoObrasRecomendadas<T>(HttpContext httpContext, List<T> listaGenerica)
        {
            var itensPorPagina = 6;
            var itensPulados = 0;

            var dados = listaGenerica.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = listaGenerica.Count;

            return new { total, data = dados };
        }

        public static ObjetoRetornoCapituloComicResponse CriarObjetoRetonoCapitulosComics(HttpContext httpContext, List<RetornoCapituloComic> listaRetornoCapituloComic, Guid idCapitulo)
        {
            var retornoCapituloComic = listaRetornoCapituloComic.FirstOrDefault(x => x.Id == idCapitulo);

            var indiceCapitulo = listaRetornoCapituloComic
                .IndexOf(retornoCapituloComic);

            var request = httpContext.Request;
            string anterior = null;
            if (indiceCapitulo > 0)
            {
                var idAnterior = listaRetornoCapituloComic[indiceCapitulo - 1].Id;
                var urlSemIdAtual = request.Path.Value.Substring(0, request.Path.Value.LastIndexOf("/"));
                anterior = $"{request.Scheme}://{request.Host}{urlSemIdAtual}/{idAnterior}";
            }

            string proxima = null;
            if (indiceCapitulo + 1 < listaRetornoCapituloComic.Count)
            {
                var idProximo = listaRetornoCapituloComic[indiceCapitulo + 1].Id;
                var urlSemIdAtual = request.Path.Value.Substring(0, request.Path.Value.LastIndexOf("/"));
                proxima = $"{request.Scheme}://{request.Host}{urlSemIdAtual}/{idProximo}";
            }

            return new ObjetoRetornoCapituloComicResponse { Anterior = anterior , Proxima = proxima, Data = retornoCapituloComic };
        }

        public static ObjetoRetornoCapituloComicResponse CriarObjetoRetonoCapitulosPorSlugComics(HttpContext httpContext, List<RetornoCapituloComic> listaRetornoCapituloComic, Guid idCapitulo)
        {
            var retornoCapituloComic = listaRetornoCapituloComic.FirstOrDefault(x => x.Id == idCapitulo);

            var indiceCapitulo = listaRetornoCapituloComic
                .IndexOf(retornoCapituloComic);

            var request = httpContext.Request;
            string anterior = null;
            if (indiceCapitulo > 0)
            {
                var idAnterior = listaRetornoCapituloComic[indiceCapitulo - 1].Id;
                var urlSemSlugAtual = request.Path.Value.Substring(0, request.Path.Value.LastIndexOf("/"));
                anterior = $"{request.Scheme}://{request.Host}{urlSemSlugAtual}/{idAnterior}";
            }

            string proxima = null;
            if (indiceCapitulo + 1 < listaRetornoCapituloComic.Count)
            {
                var idProximo = listaRetornoCapituloComic[indiceCapitulo + 1].Id;
                var urlSemSlugAtual = request.Path.Value.Substring(0, request.Path.Value.LastIndexOf("/"));
                proxima = $"{request.Scheme}://{request.Host}{urlSemSlugAtual}/{idProximo}";
            }

            return new ObjetoRetornoCapituloComicResponse { Anterior = anterior, Proxima = proxima, Data = retornoCapituloComic };
        }
        
        public static ObjetoRetornoCapituloComicResponse CriarObjetoPorSlug(
            HttpContext httpContext,
            List<RetornoCapituloComic> listaRetornoCapituloComic,
            string slugCapitulo)
        {
            var capituloAtual = listaRetornoCapituloComic.FirstOrDefault(x => x.Slug == slugCapitulo);

            if (capituloAtual == null)
            {
                throw new ArgumentException("Capítulo não encontrado na lista.", nameof(slugCapitulo));
            }

            int indice = listaRetornoCapituloComic.IndexOf(capituloAtual);

            var request = httpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var caminhoBase = request.Path.Value[..request.Path.Value.LastIndexOf("/")];

            string anterior = null;
            if (indice > 0)
            {
                var slugAnterior = listaRetornoCapituloComic[indice - 1].Slug;
                anterior = $"{baseUrl}{caminhoBase}/{slugAnterior}";
            }

            string proxima = null;
            if (indice + 1 < listaRetornoCapituloComic.Count)
            {
                var slugProximo = listaRetornoCapituloComic[indice + 1].Slug;
                proxima = $"{baseUrl}{caminhoBase}/{slugProximo}";
            }

            return new ObjetoRetornoCapituloComicResponse
            {
                Anterior = anterior,
                Proxima = proxima,
                Data = capituloAtual
            };
        }

        public static ObjetoRetornoCapituloNovelResponse CriarObjetoRetonoCapitulosNovel(HttpContext httpContext, List<RetornoCapituloNovel> listaRetornoCapituloNovel, Guid idCapitulo)
        {
            var retornoCapituloNovel = listaRetornoCapituloNovel.FirstOrDefault(x => x.Id == idCapitulo);

            var indiceCapitulo = listaRetornoCapituloNovel
                .IndexOf(retornoCapituloNovel);

            var request = httpContext.Request;
            string anterior = null;
            if (indiceCapitulo > 0)
            {
                var idAnterior = listaRetornoCapituloNovel[indiceCapitulo - 1].Id;
                var urlSemIdAtual = request.Path.Value.Substring(0, request.Path.Value.LastIndexOf("/"));
                anterior = $"{request.Scheme}://{request.Host}{urlSemIdAtual}/{idAnterior}";
            }

            string proxima = null;
            if (indiceCapitulo + 1 < listaRetornoCapituloNovel.Count)
            {
                var idProximo = listaRetornoCapituloNovel[indiceCapitulo + 1].Id;
                var urlSemIdAtual = request.Path.Value.Substring(0, request.Path.Value.LastIndexOf("/"));
                proxima = $"{request.Scheme}://{request.Host}{urlSemIdAtual}/{idProximo}";
            }

            return new ObjetoRetornoCapituloNovelResponse { Anterior = anterior, Proxima = proxima, Data = retornoCapituloNovel };
        }

        public static ObjetoRetornoCapituloNovelResponse CriarObjetoRetonoCapitulosPorSlugNovels(HttpContext httpContext, List<RetornoCapituloNovel> listaRetornoCapituloNovel, Guid idCapitulo)
        {
            var retornoCapituloNovel = listaRetornoCapituloNovel.FirstOrDefault(x => x.Id == idCapitulo);

            var indiceCapitulo = listaRetornoCapituloNovel
                .IndexOf(retornoCapituloNovel);

            var request = httpContext.Request;
            string anterior = null;
            if (indiceCapitulo > 0)
            {
                var idAnterior = listaRetornoCapituloNovel[indiceCapitulo - 1].Id;
                var urlSemSlugAtual = request.Path.Value.Substring(0, request.Path.Value.LastIndexOf("/"));
                anterior = $"{request.Scheme}://{request.Host}{urlSemSlugAtual}/{idAnterior}";
            }

            string proxima = null;
            if (indiceCapitulo + 1 < listaRetornoCapituloNovel.Count)
            {
                var idProximo = listaRetornoCapituloNovel[indiceCapitulo + 1].Id;
                var urlSemSlugAtual = request.Path.Value.Substring(0, request.Path.Value.LastIndexOf("/"));
                proxima = $"{request.Scheme}://{request.Host}{urlSemSlugAtual}/{idProximo}";
            }

            return new ObjetoRetornoCapituloNovelResponse { Anterior = anterior, Proxima = proxima, Data = retornoCapituloNovel };
        }
        
        public static ObjetoRetornoCapituloNovelResponse CriarObjetoNovelSlugECapituloSlug(
            HttpContext httpContext,
            List<RetornoCapituloNovel> listaRetornoCapituloNovel,
            string slugCapitulo)
        {
            var capituloAtual = listaRetornoCapituloNovel.FirstOrDefault(x => x.Slug == slugCapitulo);

            if (capituloAtual == null)
            {
                throw new ArgumentException("Capítulo não encontrado na lista.", nameof(slugCapitulo));
            }

            int indice = listaRetornoCapituloNovel.IndexOf(capituloAtual);

            var request = httpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var caminhoBase = request.Path.Value[..request.Path.Value.LastIndexOf("/")];

            string anterior = null;
            if (indice > 0)
            {
                var slugAnterior = listaRetornoCapituloNovel[indice - 1].Slug;
                anterior = $"{baseUrl}{caminhoBase}/{slugAnterior}";
            }

            string proxima = null;
            if (indice + 1 < listaRetornoCapituloNovel.Count)
            {
                var slugProximo = listaRetornoCapituloNovel[indice + 1].Slug;
                proxima = $"{baseUrl}{caminhoBase}/{slugProximo}";
            }

            return new ObjetoRetornoCapituloNovelResponse
            {
                Anterior = anterior,
                Proxima = proxima,
                Data = capituloAtual
            };
        }

        public static ObjetoRetornoVolumeComicResponse CriarObjetoRetonoVolumesComics(HttpContext httpContext, List<RetornoVolumeComic> listaRetornoVolumeComic, int? skip, int? take)
        {
            if (!skip.HasValue && !take.HasValue)
            {
                return new ObjetoRetornoVolumeComicResponse { Anterior = null, Proxima = null, Data = listaRetornoVolumeComic, Total = listaRetornoVolumeComic.Count };
            }

            var itensPorPagina = ValidacaoRequest.RetornaTakeTratado(take);
            var itensPulados = ValidacaoRequest.RetornaSkipTratado(skip, itensPorPagina);

            var dados = listaRetornoVolumeComic.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = listaRetornoVolumeComic.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);

            return new ObjetoRetornoVolumeComicResponse { Anterior = anterior, Proxima = proxima, Data = dados, Total = total };
        }

        public static ObjetoRetornoVolumeNovelResponse CriarObjetoRetonoVolumesNovels(List<RetornoVolumeNovel> listaRetornoVolumeNovel)
        {
            return new ObjetoRetornoVolumeNovelResponse { Anterior = null, Proxima = null, Data = listaRetornoVolumeNovel, Total = listaRetornoVolumeNovel.Count };
        }

        public static ObjetoRetornoGenerosCadastradosResponse CriarObjetoRetornoGenerosCadastrados(List<RetornoGeneroCadastrado> listaRetornoGenerosCadastrados)
        {
            return new ObjetoRetornoGenerosCadastradosResponse { Anterior = null, Proxima = null, Data = listaRetornoGenerosCadastrados, Total = listaRetornoGenerosCadastrados.Count };
        }

        public static ObjetoRetornoNovelsRecentes CriarObjetoRetonoNovelsRecentes(HttpContext httpContext, List<RetornoNovelsRecentes> listaNovelsRecentes, int? skip, int? take)
        {
            if (!skip.HasValue && !take.HasValue)
            {
                return new ObjetoRetornoNovelsRecentes { Anterior = null, Proxima = null, Data = listaNovelsRecentes, Total = listaNovelsRecentes.Count };
            }

            var itensPorPagina = ValidacaoRequest.RetornaTakeTratado(take);
            var itensPulados = ValidacaoRequest.RetornaSkipTratado(skip, itensPorPagina);

            var dados = listaNovelsRecentes.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = listaNovelsRecentes.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);

            return new ObjetoRetornoNovelsRecentes { Total = total, Proxima = proxima, Anterior = anterior, Data = dados };
        }

        public static ObjetoRetornoComicsRecentes CriarObjetoRetonoComicsRecentes(HttpContext httpContext, List<RetornoComicsRecentes> listaComicsRecentes, int? skip, int? take)
        {
            if (!skip.HasValue && !take.HasValue)
            {
                return new ObjetoRetornoComicsRecentes { Anterior = null, Proxima = null, Data = listaComicsRecentes, Total = listaComicsRecentes.Count };
            }

            var itensPorPagina = ValidacaoRequest.RetornaTakeTratado(take);
            var itensPulados = ValidacaoRequest.RetornaSkipTratado(skip, itensPorPagina);

            var dados = listaComicsRecentes.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = listaComicsRecentes.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);

            return new ObjetoRetornoComicsRecentes { Total = total, Proxima = proxima, Anterior = anterior, Data = dados };
        }

        public static ObjetoRetornoGenerosResponse CriarObjetoRetornoGeneros(List<RetornoGenero> listaRetornoGeneros)
        {
            return new ObjetoRetornoGenerosResponse  { Total = listaRetornoGeneros.Count, Proxima = null, Anterior = null, Data = listaRetornoGeneros };
        }

        public static ObjetoRetornoCapitulosPorSlugComics CriarObjetoRetornoCapitulosPorSlugObra(HttpContext httpContext, List<RetornoCapituloComic> listaRetornoCapituloComic, int? skip, int? take)
        {
            if (!skip.HasValue && !take.HasValue)
            {
                return new ObjetoRetornoCapitulosPorSlugComics { Anterior = null, Proxima = null, Data = listaRetornoCapituloComic, Total = listaRetornoCapituloComic.Count };
            }

            var itensPorPagina = ValidacaoRequest.RetornaTakeCapitulosTratado(take);
            var itensPulados = ValidacaoRequest.RetornaSkipTratado(skip, itensPorPagina);

            var dados = listaRetornoCapituloComic.Skip(itensPulados).Take(itensPorPagina).ToList();
            var total = listaRetornoCapituloComic.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);

            return new ObjetoRetornoCapitulosPorSlugComics { Total = total, Proxima = proxima, Anterior = anterior, Data = dados };
        }

        private static string RetornaLinkPaginacaoAnterior(int itensPorPagina, int itensPulados, string url)
        {
            string anterior = null;
            if (itensPulados > 0)
            {
                int skip = 0;
                if (itensPulados < itensPorPagina)
                {
                    skip = --itensPulados;
                }
                else
                {
                    skip = itensPulados - itensPorPagina;
                }

                anterior = $"{url}?Skip={skip}&Take={itensPorPagina}";
            }

            return anterior;
        }

        private static string RetornaLinkPaginacaoProxima<T>(int itensPorPagina, int itensPulados, List<T> dados, string url)
        {
            string proxima = null;
            if (dados.Count >= itensPorPagina)
            {
                proxima = $"{url}?Skip={itensPulados + itensPorPagina}&Take={itensPorPagina}";
            }

            return proxima;
        }

        private static string RetornaQuery(RequestObras requestObras)
        {
            var pesquisar = TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Pesquisar);
            if (!string.IsNullOrEmpty(pesquisar))
                return $"&Pesquisar={pesquisar}";

            var nacionalidade = TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Nacionalidade);
            var status = TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Status);
            var tipo = TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Tipo);
            var genero = TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Genero);
            var idCapitulo = TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.IdCapitulo);
            var idObra = TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.IdObra);

            string queryStringComposta = string.Empty;

            queryStringComposta +=
                string.IsNullOrEmpty(nacionalidade) ? "" : $"&Nacionalidade={nacionalidade}";

            queryStringComposta +=
                string.IsNullOrEmpty(status) ? "" : $"&Status={status}";

            queryStringComposta +=
                string.IsNullOrEmpty(tipo) ? "" : $"&Tipo={tipo}";

            queryStringComposta +=
                string.IsNullOrEmpty(genero) ? "" : $"&Genero={genero}";

            queryStringComposta +=
                string.IsNullOrEmpty(idCapitulo) ? "" : $"&IdCapitulo={idCapitulo}";

            queryStringComposta +=
                string.IsNullOrEmpty(idObra) ? "" : $"&IdObra={idObra}";

            return queryStringComposta;
        }       
    }
}
