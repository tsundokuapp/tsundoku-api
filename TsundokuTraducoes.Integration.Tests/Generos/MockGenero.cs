namespace TsundokuTraducoes.Integration.Tests.Generos
{
    public class MockGenero
    {
        public static MultipartFormDataContent RetornaFormDataMockAdicionarGenero(bool falhar = false, string tituloExistente = "")
        {
            var titulo = $"Teste{Guid.NewGuid().ToString().Substring(0, 4)}";

            if (falhar)
                titulo = tituloExistente;

            var form = new MultipartFormDataContent
            {
                { new StringContent(titulo), "Descricao" },
                { new StringContent("Bravo"), "UsuarioInclusao" }
            };

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAtualizarGenero(bool falhar, Guid idGenero)
        {
            var titulo = $"Teste{Guid.NewGuid().ToString().Substring(0, 4)}";

            if (falhar)
                titulo = "";

            var form = new MultipartFormDataContent
            {
                { new StringContent(idGenero.ToString()), "Id" },
                { new StringContent(titulo), "Descricao" },
                { new StringContent("Bravo"), "UsuarioInclusao" },
                { new StringContent("Axios"), "UsuarioAlteracao" }
            };

            return form;
        }

        public static MultipartFormDataContent RetornaFormDataMockAdicionarGeneroParaTeste()
        {
            var titulo = $"Teste{Guid.NewGuid().ToString().Substring(0, 4)}";
            var form = new MultipartFormDataContent
            {
                { new StringContent(titulo), "Descricao" },
                { new StringContent("Bravo"), "UsuarioInclusao" }
            };

            return form;
        }
    }
}