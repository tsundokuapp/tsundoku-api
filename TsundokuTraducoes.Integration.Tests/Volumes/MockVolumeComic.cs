﻿namespace TsundokuTraducoes.Integration.Tests.Volumes
{
    #nullable disable

    public class MockVolumeComic : AppIntegrationBase
    {
        public MultipartFormDataContent RetornaFormDataMockAdicionarVolumeComic(bool falhar, Guid obraid)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";

            if (falhar)
            {
                numero = string.Empty;
            }

            form.Add(new StringContent(""), "Titulo");
            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent("O mestre"), "Sinopse");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(obraid.ToString()), "ObraId");
            var contentImagemVolume = RetornaStreamImagemMock("imagemVolume.jpeg", "ImagemVolumeFile");
            form.Add(contentImagemVolume);

            return form;
        }

        public MultipartFormDataContent RetornaFormDataMockAtualizarVolumeComic(bool falhar, Guid obraid, Guid? idVolume)
        {
            var form = new MultipartFormDataContent();
            var numero = $"{RetornaNumeroAleatorio()}";
            var loginAlteracao = "Araragui";

            if (falhar)
                numero = string.Empty;

            if (falhar)
                loginAlteracao = string.Empty;

            form.Add(new StringContent(idVolume.ToString()), "Id");
            form.Add(new StringContent("Esse volume tem título"), "Titulo");
            form.Add(new StringContent(numero), "Numero");
            form.Add(new StringContent("Teste de sinopse alterada"), "Sinopse");
            form.Add(new StringContent("Bravo"), "UsuarioInclusao");
            form.Add(new StringContent(loginAlteracao), "UsuarioAlteracao");
            form.Add(new StringContent(obraid.ToString()), "ObraId");
            var contentImagemVolume = RetornaStreamImagemMock("imagemVolume.jpeg", "ImagemVolumeFile");
            form.Add(contentImagemVolume);

            return form;
        }
    }
}
