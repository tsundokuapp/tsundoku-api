using TsundokuTraducoes.Entities.Entities.DePara;

namespace TsundokuTraducoes.Entities.Tests.Generos
{
    public class GeneroComicTestes
    {
        [Fact]
        public void CriarGeneroComicValido()
        {
            var idComic = Guid.Parse("64329027-9111-418c-a6ff-842689916083");
            var generoComic = new GeneroComic();
            generoComic.AdicionaGeneroComic(idComic, Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"));
        
            Assert.NotNull(generoComic);
            Assert.Equal(Guid.Parse("64329027-9111-418c-a6ff-842689916083"), generoComic.ComicId);        
        }

        [Fact]
        public void DeveFalharAoCriarComIdComicIncorreto()
        {
            var idComic = Guid.Parse("64329027-9111-418c-a6ff-842689916083");
            var generoComic = new GeneroComic();
            generoComic.AdicionaGeneroComic(idComic, Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"));
                        
            Assert.NotEqual(Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"), generoComic.ComicId);
        }
    }
}
