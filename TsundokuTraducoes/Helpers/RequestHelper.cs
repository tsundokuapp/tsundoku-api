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
