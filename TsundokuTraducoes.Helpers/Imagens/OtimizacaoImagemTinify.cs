﻿using TinifyAPI;

namespace TsundokuTraducoes.Helpers.Imagens
{
    public static class OtimizacaoImagemTinify
    {
        public static async Task<FluentResults.Result<byte[]>> OtimizarImagem(string apiKeyTinify, byte[] byteImagem)
        {
            Tinify.Key = apiKeyTinify;
            Source retornoByteImagemTinify;
            Result response;

            try
            {   
                retornoByteImagemTinify = await Tinify.FromBuffer(byteImagem);
                var converted = retornoByteImagemTinify.Convert(new { type = new[] { "image/jpg" } });
                response = await converted.GetResult();
                if (response.ToBuffer() == null || response.ToBuffer().Length == 0)
                    return FluentResults.Result.Fail("Erro ao tentar otimizar a imagem");
            }
            catch (Exception ex)
            {
                return FluentResults.Result.Fail("Erro ao otimizar a imagem" + ex.ToString());
            }

            return FluentResults.Result.Ok(response.ToBuffer());
        }
    }
}