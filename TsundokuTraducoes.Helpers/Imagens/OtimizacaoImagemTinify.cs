using TinifyAPI;

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

        public static byte[] ConverteStreamParaByteArray(Stream stream)
        {
            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mStream = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mStream.Write(byteArray, 0, bit);
                }
                return mStream.ToArray();
            }
        }

        public static bool SalvaArquivoImagem(byte[] array, string caminhoArquivoImagem)
        {
            try
            {
                using MemoryStream ms = new(array);
                string nome = Path.GetRandomFileName();
                FileStream file = new(caminhoArquivoImagem, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}