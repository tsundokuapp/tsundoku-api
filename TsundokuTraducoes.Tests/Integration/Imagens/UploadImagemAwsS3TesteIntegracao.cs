using TsundokuTraducoes.Domain.Services;
using TsundokuTraducoes.Integration.Tests.Recursos;

namespace TsundokuTraducoes.Integration.Tests.Imagens
{
    public class UploadImagemAwsS3TesteIntegracao
    {
        private readonly AwsS3Service _servicoAmazon; 

        public UploadImagemAwsS3TesteIntegracao()
        {
            _servicoAmazon = new AwsS3Service();
        }

        [Fact(Skip = "Esperando refatoração")]
        public async Task DeveCriarPastaS3()
        {
            var nomePasta = RetornoNomePasta();
            var pastaCriadaAwsS3 = await _servicoAmazon.CriarPastaS3(nomePasta);

            Assert.True(pastaCriadaAwsS3);

            await Dispose(nomePasta);
        }

        [Fact(Skip = "Esperando refatoração")]
        public async Task DeveFalharParaCriarPastaS3()
        {
            var nomePasta = "";
            var pastaCriadaAwsS3 = await _servicoAmazon.CriarPastaS3(nomePasta);

            Assert.True(!pastaCriadaAwsS3);

            await Dispose(nomePasta);
        }

        [Fact(Skip = "Esperando refatoração")]
        public async Task DeveCriarUmaPastaDepoisVerificarPastaS3Existente()
        {
            var nomePasta = RetornoNomePasta();
            var pastaExistente = await _servicoAmazon.VerificaObjetoExistenteAwsS3(nomePasta);
            var pastaCriadaAwsS3 = false;

            if (!pastaExistente)
                pastaCriadaAwsS3 = await _servicoAmazon.CriarPastaS3(nomePasta);

            Assert.True(pastaCriadaAwsS3);

            await Dispose(nomePasta);
        }

        [Fact(Skip = "Esperando refatoração")]
        public async Task NaoDeveCriarUmaPastaS3JahExistente()
        {
            var nomePasta = await RetornaNomePastaExistente();
            var pastaExistente = await _servicoAmazon.VerificaObjetoExistenteAwsS3(nomePasta);
            var pastaCriadaAwsS3 = false;

            if (!pastaExistente)
                pastaCriadaAwsS3 = await _servicoAmazon.CriarPastaS3(nomePasta);

            Assert.True(!pastaCriadaAwsS3);
        }

        [Fact(Skip = "Esperando refatoração")]
        public async Task DeveFazerUploadEmUmPastaS3Criada()
        {
            var nomePasta = await RetornaNomePastaExistente();
            var caminhoCompletoImagem = $"{nomePasta}imagem_teste_s3{Guid.NewGuid().ToString()[..5]}.png";
            var streamImagem = MockBase.RetornaImagemTeste();
            var uploadImagemRealizada = await _servicoAmazon.UploadImagem(streamImagem, caminhoCompletoImagem, false);

            Assert.True(uploadImagemRealizada);

            await Dispose(nomePasta);
        }

        [Fact(Skip = "Esperando refatoração")]
        public async Task DeveFalhaAoTentarFazerUploadEmUmPastaS3Criada()
        {
            var nomePasta = await RetornaNomePastaExistente();
            var caminhoCompletoImagem = "";
            var streamImagem = new MemoryStream();
            streamImagem = null;
            var uploadImagemRealizada = await _servicoAmazon.UploadImagem(streamImagem, caminhoCompletoImagem, false);

            Assert.True(!uploadImagemRealizada);

            await Dispose(nomePasta);
        }

        [Fact(Skip = "Esperando refatoração")]
        public async Task DeveExcluirObjetoS3()
        {
            var nomePasta = RetornoNomePasta();
            var pastaCriadaAwsS3 = await _servicoAmazon.CriarPastaS3(nomePasta);

            var caminhoCompletoImagem = $"{nomePasta}imagem_teste_s3{Guid.NewGuid().ToString()[..5]}.png";
            var streamImagem = MockBase.RetornaImagemTeste();
            var uploadImagemRealizada = await _servicoAmazon.UploadImagem(streamImagem, caminhoCompletoImagem, false);

            var pastaExcluida = await _servicoAmazon.ExcluiObjetoBucket(nomePasta);

            Assert.True(pastaExcluida);
        }

        [Fact(Skip = "Esperando refatoração")]
        public async Task DeveFalharAoTentarExcluirObjetoS3()
        {
            bool pastaExcluida;
            var nomePasta = RetornoNomePasta();

            pastaExcluida = await _servicoAmazon.ExcluiObjetoBucket(nomePasta);

            Assert.True(!pastaExcluida);
        }

        public static string RetornoNomePasta()
        {
            return $"pasta-teste-{Guid.NewGuid().ToString()[..4]}/";
        }

        public async Task<string> RetornaNomePastaExistente()
        {
            var nomePasta = RetornoNomePasta();
            var pastaExistente = await _servicoAmazon.VerificaObjetoExistenteAwsS3(nomePasta);
            var pastaCriadaAwsS3 = false;

            if (!pastaExistente)
                pastaCriadaAwsS3 = await _servicoAmazon.CriarPastaS3(nomePasta);

            return nomePasta;
        }
    
        public async Task Dispose(string nomePasta)
        {
            await _servicoAmazon.ExcluiObjetoBucket(nomePasta);
        }
    }
}
