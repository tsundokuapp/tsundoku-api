using TsundokuTraducoes.Helpers.Imagens;
using TsundokuTraducoes.Integration.Tests.Recursos;

namespace TsundokuTraducoes.Integration.Tests.Imagens
{
    public class UploadImagemAwsS3TesteIntegracao
    {
        [Fact]
        public async Task DeveCriarPastaS3()
        {
            var nomePasta = RetornoNomePasta();
            var servicoAmazon = new ServicosImagemAmazonS3();
            var pastaCriadaAwsS3 = await servicoAmazon.CriarPastaS3(nomePasta);

            Assert.True(pastaCriadaAwsS3);

            await Dispose(nomePasta);
        }

        [Fact]
        public async Task DeveFalharParaCriarPastaS3()
        {
            var nomePasta = "";
            var servicoAmazon = new ServicosImagemAmazonS3();
            var pastaCriadaAwsS3 = await servicoAmazon.CriarPastaS3(nomePasta);

            Assert.True(!pastaCriadaAwsS3);

            await Dispose(nomePasta);
        }

        [Fact]
        public async Task DeveCriarUmaPastaDepoisVerificarPastaS3Existente()
        {
            var nomePasta = RetornoNomePasta();
            var servicoAmazon = new ServicosImagemAmazonS3();
            var pastaExistente = await servicoAmazon.VerificaObjetoExistenteAwsS3(nomePasta);
            var pastaCriadaAwsS3 = false;

            if (!pastaExistente)
                pastaCriadaAwsS3 = await servicoAmazon.CriarPastaS3(nomePasta);

            Assert.True(pastaCriadaAwsS3);

            await Dispose(nomePasta);
        }

        [Fact]
        public async Task NaoDeveCriarUmaPastaS3JahExistente()
        {
            var nomePasta = await RetornaNomePastaExistente();
            var servicoAmazon = new ServicosImagemAmazonS3();
            var pastaExistente = await servicoAmazon.VerificaObjetoExistenteAwsS3(nomePasta);
            var pastaCriadaAwsS3 = false;

            if (!pastaExistente)
                pastaCriadaAwsS3 = await servicoAmazon.CriarPastaS3(nomePasta);

            Assert.True(!pastaCriadaAwsS3);
        }

        [Fact]
        public async Task DeveFazerUploadEmUmPastaS3Criada()
        {
            var nomePasta = await RetornaNomePastaExistente();
            var caminhoCompletoImagem = $"{nomePasta}imagem_teste_s3{Guid.NewGuid().ToString()[..5]}.png";
            var streamImagem = MockBase.RetornaImagemTeste();
            var servicoAmazon = new ServicosImagemAmazonS3();
            var uploadImagemRealizada = await servicoAmazon.UploadImagem(streamImagem, caminhoCompletoImagem, false);

            Assert.True(uploadImagemRealizada);

            await Dispose(nomePasta);
        }

        [Fact]
        public async Task DeveFalhaAoTentarFazerUploadEmUmPastaS3Criada()
        {
            var nomePasta = await RetornaNomePastaExistente();
            var caminhoCompletoImagem = "";
            var streamImagem = new MemoryStream();
            streamImagem = null;
            var servicoAmazon = new ServicosImagemAmazonS3();
            var uploadImagemRealizada = await servicoAmazon.UploadImagem(streamImagem, caminhoCompletoImagem, false);

            Assert.True(!uploadImagemRealizada);

            await Dispose(nomePasta);
        }

        [Fact]
        public async Task DeveExcluirObjetoS3()
        {
            var nomePasta = RetornoNomePasta();
            var servicoAmazon = new ServicosImagemAmazonS3();
            var pastaCriadaAwsS3 = await servicoAmazon.CriarPastaS3(nomePasta);

            var caminhoCompletoImagem = $"{nomePasta}imagem_teste_s3{Guid.NewGuid().ToString()[..5]}.png";
            var streamImagem = MockBase.RetornaImagemTeste();
            var uploadImagemRealizada = await servicoAmazon.UploadImagem(streamImagem, caminhoCompletoImagem, false);

            var pastaExcluida = await servicoAmazon.ExcluiObjetoBucket(nomePasta);

            Assert.True(pastaExcluida);
        }

        [Fact]
        public async Task DeveFalharAoTentarExcluirObjetoS3()
        {
            bool pastaExcluida;
            var nomePasta = RetornoNomePasta();
            var servicoAmazon = new ServicosImagemAmazonS3();

            pastaExcluida = await servicoAmazon.ExcluiObjetoBucket(nomePasta);

            Assert.True(!pastaExcluida);
        }

        public static string RetornoNomePasta()
        {
            return $"pasta-teste-{Guid.NewGuid().ToString()[..4]}/";
        }

        public static async Task<string> RetornaNomePastaExistente()
        {
            var nomePasta = RetornoNomePasta();
            var servicoAmazon = new ServicosImagemAmazonS3();
            var pastaExistente = await servicoAmazon.VerificaObjetoExistenteAwsS3(nomePasta);
            var pastaCriadaAwsS3 = false;

            if (!pastaExistente)
                pastaCriadaAwsS3 = await servicoAmazon.CriarPastaS3(nomePasta);

            return nomePasta;
        }
    
        public static async Task Dispose(string nomePasta)
        {
            var servicoAmazon = new ServicosImagemAmazonS3();
            await servicoAmazon.ExcluiObjetoBucket(nomePasta);
        }
    }
}
