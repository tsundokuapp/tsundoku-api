using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin.Request;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
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

            string proxima = RetornaLinkPaginacaoProxima(itensPorPagina, itensPulados, dados, url) + RetornaQuery(requestObras);
            string anterior = RetornaLinkPaginacaoAnterior(itensPorPagina, itensPulados, url);

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        private static string RetornaLinkPaginacaoAnterior(int itensPorPagina, int itensPulados, string url)
        {
            string anterior = null;
            if (itensPulados > 0)
            {
                anterior = $"{url}?Skip={itensPulados - itensPorPagina}&Take={itensPorPagina}";
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
            if (!string.IsNullOrEmpty(requestObras.Pesquisar))
                return $"&Pesquisar={TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Pesquisar)}";

            return $"&Nacionalidade={TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Nacionalidade)}" +
                    $"&Status={TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Status)}" +
                    $"&Tipo={TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Tipo)}" +
                    $"&Genero={TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.Genero)}" +
                    $"&IdCapitulo={TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.IdCapitulo)}" +
                    $"&IdObra={TratamentoDeStrings.RetornaStringTratadaSemNull(requestObras.IdObra)}";
        }
    }
}
