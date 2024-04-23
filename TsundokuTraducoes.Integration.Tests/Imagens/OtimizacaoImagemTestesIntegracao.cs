using FluentResults;
using System.Configuration;
using TsundokuTraducoes.Helpers.Imagens;
using TsundokuTraducoes.Integration.Tests.Recursos;

namespace TsundokuTraducoes.Integration.Tests.Imagens
{
    public class OtimizacaoImagemTestesIntegracao
    {
        [Fact]
        public async Task DeveOtimizarImagem()
        {
            var streamImagem = MockBase.RetornaImagemTeste();
            var byteImagem = UtilidadeImagem.ConverteStreamParaByteArray(streamImagem);
            var retornoImagemOtimizada = await Task.Run(() => OtimizacaoImagemTinify.OtimizarImagem(RetornaApiKey(), byteImagem));            
            Assert.True(retornoImagemOtimizada.IsSuccess);
        }

        [Fact]
        public async Task DeveFalharAoEnviarArquivoQueNaoEhImagem()
        {
            var streamImagem = MockBase.RetornaImagemTesteFalha();
            var byteImagem = UtilidadeImagem.ConverteStreamParaByteArray(streamImagem);
            var retornoImagemOtimizada = await Task.Run(() => OtimizacaoImagemTinify.OtimizarImagem(RetornaApiKey(), byteImagem));
            Assert.True(retornoImagemOtimizada.IsFailed);
        }

        [Fact]
        public void DeveFalharAoTentarOtimizarImagemAcimaDoisSegundos()
        {
            var streamImagem = MockBase.RetornaImagemTeste();
            var byteImagem = UtilidadeImagem.ConverteStreamParaByteArray(streamImagem);
            var retornoImagemOtimizada = Task.Run(() => RetornaImagemOtimizadaAcimaDoisSegundos(byteImagem));

            Assert.True(!retornoImagemOtimizada.Wait(TimeSpan.FromSeconds(2)));
        }

        private static Task<Result<byte[]>> RetornaImagemOtimizadaAcimaDoisSegundos(byte[] byteImagem)
        {
            Thread.Sleep(1900);
            return OtimizacaoImagemTinify.OtimizarImagem(RetornaApiKey(), byteImagem);
        }

        private static string RetornaApiKey()
        {
            return ConfigurationManager.AppSettings["ApiKey"];
        }
    }
}
