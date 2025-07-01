namespace TsundokuTraducoes.Helpers
{
#nullable disable
    public static class SlugAuxiliar
    {
        public static string RetornaSlugTipoObra(string slug)
        {
            var dicionarioTipoObraSlug = new Dictionary<string, string>
            {
                { "Light Novel", "light-novel" },
                { "Web Novel", "web-novel" },
                { "Mangá", "manga" },
                { "Manhua", "manhua" },
                { "Manhwa", "manhwa" },
                { "Comic", "comic" },
                { "Novel", "novel" }
            };
           
            return dicionarioTipoObraSlug.GetValueOrDefault(slug);
        }

        public static string RetornaSlugStatusObra(string slug)
        {
            var dicionarioStatusObraSlug = new Dictionary<string, string>
            {
                { "Em andamento", "em-andamento" },
                { "Concluído", "concluido" },
                { "Cancelado", "cancelado" },
                { "Hiato", "hiato" },
            };

            return dicionarioStatusObraSlug.GetValueOrDefault(slug);
        }

        public static string RetornaSlugNacionalidade(string slug)
        {
            var dicionarioNacionalidadeSlug = new Dictionary<string, string>
            {
                { "Japonesa", "japonesa" },
                { "Coreana", "coreana" },
                { "Chinesa", "chinesa" },
                { "Americana", "americana" },
                { "Brasileira", "brasileira" },
                { "Espanhola", "espanhola" }
            };

            return dicionarioNacionalidadeSlug.GetValueOrDefault(slug);
        }
    }
}