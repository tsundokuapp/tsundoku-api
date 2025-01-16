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
        public static object CriaObjetoRetono<T>(HttpContext httpContext, List<T> listaGenerica, int? skip, int? take)
        {
            var obrasPorPagina = ValidacaoRequest.RetornaTakeTratado(take);
            var numeroPagina = ValidacaoRequest.RetornaSkipTratado(skip, obrasPorPagina);

            var dados = listaGenerica.Skip(numeroPagina).Take(obrasPorPagina).ToList();
            var total = listaGenerica.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(obrasPorPagina, numeroPagina, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(obrasPorPagina, numeroPagina, url);

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        public static object CriaObjetoRetonoVolume(HttpContext httpContext, List<RetornoVolume> listaRetornoVolume, RequestVolume requestVolume)
        {
            var obrasPorPagina = ValidacaoRequest.RetornaTakeTratado(requestVolume.Take);
            var numeroPagina = ValidacaoRequest.RetornaSkipTratado(requestVolume.Skip, obrasPorPagina);

            var dados = listaRetornoVolume.Skip(numeroPagina).Take(obrasPorPagina).ToList();
            var total = listaRetornoVolume.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(obrasPorPagina, numeroPagina, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(obrasPorPagina, numeroPagina, url);

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        public static object CriaObjetoRetonoCapitulo(HttpContext httpContext, List<RetornoCapitulo> ListaRetornoCapitulo, RequestCapitulo requestCapitulo)
        {
            var obrasPorPagina = ValidacaoRequest.RetornaTakeTratado(requestCapitulo.Take);
            var numeroPagina = ValidacaoRequest.RetornaSkipTratado(requestCapitulo.Skip, obrasPorPagina);

            var dados = ListaRetornoCapitulo.Skip(numeroPagina).Take(obrasPorPagina).ToList();
            var total = ListaRetornoCapitulo.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(obrasPorPagina, numeroPagina, dados, url);
            string anterior = RetornaLinkPaginacaoAnterior(obrasPorPagina, numeroPagina, url);

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        internal static object CriaObjetoRetonoObras<T>(HttpContext httpContext, List<T> listaGenerica, RequestObras requestObras, bool ehHome = false)
        {
            var obrasPorPagina = ValidacaoRequest.RetornaTakeTratado(requestObras.Take, ehHome);
            var numeroPagina = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip, obrasPorPagina);

            var dados = listaGenerica.Skip(numeroPagina).Take(obrasPorPagina).ToList();
            var total = listaGenerica.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = RetornaLinkPaginacaoProxima(obrasPorPagina, numeroPagina, dados, url) + RetornaQuery(requestObras);
            string anterior = RetornaLinkPaginacaoAnterior(obrasPorPagina, numeroPagina, url);

            return new { total = total, proxima = proxima, anterior = anterior, data = dados };
        }

        private static string RetornaLinkPaginacaoAnterior(int obrasPorPagina, int numeroPagina, string url)
        {
            string anterior = null;
            if (numeroPagina > 0)
            {
                anterior = $"{url}?Skip={numeroPagina - obrasPorPagina}&Take={obrasPorPagina}";
            }

            return anterior;
        }

        private static string RetornaLinkPaginacaoProxima<T>(int obrasPorPagina, int numeroPagina, List<T> dados, string url)
        {
            string proxima = null;
            if (dados.Count >= obrasPorPagina)
            {
                proxima = $"{url}?Skip={numeroPagina + obrasPorPagina}&Take={obrasPorPagina}";
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
