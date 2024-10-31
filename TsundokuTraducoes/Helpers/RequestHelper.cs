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
        public static object CriaObjetoRetonoObra(HttpContext httpContext, List<RetornoObra> value, RequestObra requestObra)
        {
            var numeroPagina = ValidacaoRequest.RetornaSkipTratadoAdmin(requestObra.Skip);
            var obrasPorPagina = ValidacaoRequest.RetornaTakeTratadoAdmin(requestObra.Take);

            var dados = value.Skip(numeroPagina).Take(obrasPorPagina).ToList();
            var total = value.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = $"{url}?Skip={numeroPagina + 1}&Take={obrasPorPagina}";
            string anterior = null;

            if (numeroPagina > 0)
            {
                anterior = $"{url}?Skip={numeroPagina - 1}&Take={obrasPorPagina}";
            }

            return new { total = total, proxima = proxima, anterior = anterior, dados = dados };
        }

        public static object CriaObjetoRetonoVolume(HttpContext httpContext, List<RetornoVolume> value, RequestVolume requestVolume)
        {
            var numeroPagina = ValidacaoRequest.RetornaSkipTratadoAdmin(requestVolume.Skip);
            var obrasPorPagina = ValidacaoRequest.RetornaTakeTratadoAdmin(requestVolume.Take);

            var dados = value.Skip(numeroPagina).Take(obrasPorPagina).ToList();
            var total = value.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = $"{url}?Skip={numeroPagina + 1}&Take={obrasPorPagina}&IdObra={requestVolume.IdObra}";
            string anterior = null;

            if (numeroPagina > 0)
            {
                anterior = $"{url}?Skip={numeroPagina - 1}&Take={obrasPorPagina}&IdObra={requestVolume.IdObra}";
            }

            return new { total = total, proxima = proxima, anterior = anterior, dados = dados };
        }

        public static object CriaObjetoRetonoCapitulo(HttpContext httpContext, List<RetornoCapitulo> value, RequestCapitulo requestCapitulo)
        {
            var numeroPagina = ValidacaoRequest.RetornaSkipTratadoAdmin(requestCapitulo.Skip);
            var obrasPorPagina = ValidacaoRequest.RetornaTakeTratadoAdmin(requestCapitulo.Take);

            var dados = value.Skip(numeroPagina).Take(obrasPorPagina).ToList();
            var total = value.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = $"{url}?Skip={numeroPagina + 1}&Take={obrasPorPagina}&IdVolume={requestCapitulo.IdVolume}";
            string anterior = null;

            if (numeroPagina > 0)
            {
                anterior = $"{url}?Skip={numeroPagina - 1}&Take={obrasPorPagina}&IdVolume={requestCapitulo.IdVolume}";
            }

            return new { total = total, proxima = proxima, anterior = anterior, dados = dados };
        }

        internal static object CriaObjetoRetonoObras<T>(HttpContext httpContext, List<T> lista, RequestObras requestObras, bool ehHome = false)
        {
            var numeroPagina = ValidacaoRequest.RetornaSkipTratado(requestObras.Skip);
            var obrasPorPagina = ValidacaoRequest.RetornaTakeTratado(requestObras.Take, ehHome);

            var dados = lista.Skip(numeroPagina).Take(obrasPorPagina).ToList();
            var total = lista.Count;

            var request = httpContext.Request;
            var url = $"{request.Scheme}://{request.Host}{request.Path}";

            string proxima = $"{url}?Skip={numeroPagina + 1}&Take={obrasPorPagina}" + RetornaQuery(requestObras);
            string anterior = null;

            if (numeroPagina > 0)
            {
                anterior = $"{url}?Skip={numeroPagina - 1}&Take={obrasPorPagina}" + RetornaQuery(requestObras);
            }

            return new { total = total, proxima = proxima, anterior = anterior, dados = dados };
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
