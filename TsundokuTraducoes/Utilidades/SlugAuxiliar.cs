using System;
using System.Collections.Generic;

namespace TsundokuTraducoes.Api.Utilidades
{
    public static class SlugAuxiliar
    {
        public static string RetornaTipoObraPorSlug(string slug)
        {
            var dicionarioTipoObraSlug = new Dictionary<string, string>
            {
                { "light-novel", "Light Novel" },
                { "web-novel", "Web Novel" },
                { "manga", "Mangá" },
                { "manhua", "Manhua" },
                { "manhwa", "Manhwa" }
            };
           
            return dicionarioTipoObraSlug.GetValueOrDefault(slug);
        }

        public static string RetornaStatusObraPorSlug(string slug)
        {
            var dicionarioStatusObraSlug = new Dictionary<string, string>
            {
                { "em-andamento", "Em andamento" },
                { "pausada", "Pausada" },
                { "dropada", "Dropada" },
                { "completa", "Completa" }
            };

            return dicionarioStatusObraSlug.GetValueOrDefault(slug);
        }

        public static string RetornaNacionalidadePorSlug(string slug)
        {
            var dicionarioNacionalidadeSlug = new Dictionary<string, string>
            {
                { "japonesa", "Japonesa" },
                { "coreana", "Coreana" },
                { "chinesa", "Chinesa" },
                { "americana", "Americana" }
            };

            return dicionarioNacionalidadeSlug.GetValueOrDefault(slug);
        }       
    }
}
