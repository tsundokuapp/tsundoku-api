using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Tests.Entities.Generos;

namespace TsundokuTraducoes.Entities.Tests.Generos
{
    public class GenerosTestes
    {
        #region => TESTES - GENEROS

        [Fact]
        public void CriaGeneroValido()
        {
            var id = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0");
            var genero = new Genero();
            genero.AdicionaGenero(id, "Aventura", "aventura", "Axios", "Bravo", DateTime.Now, DateTime.Now);

            Assert.NotNull(genero);
            Assert.Equal(Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"), genero.Id);
            Assert.Equal("aventura", genero.Slug);
        }

        [Fact]
        public void DeveFalharAoCriarComDescricaVazia()
        {
            var id = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0");
            var genero = new Genero();
            genero.AdicionaGenero(id, "", "aventura", "Axios", "Bravo", DateTime.Now, DateTime.Now);

            Assert.True(string.IsNullOrEmpty(genero.Descricao));
        }

        [Fact]
        public void EntidadeGenero_DeveCriarUmGenero()
        {
            var generosFactory = new GenerosFactory();
            var genero = generosFactory.GerarGenero();

            Assert.NotNull(genero);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", genero.Id.ToString());
            Assert.False(string.IsNullOrEmpty(genero.Descricao));
            Assert.False(string.IsNullOrEmpty(genero.Slug));
            Assert.False(string.IsNullOrEmpty(genero.UsuarioInclusao));
            Assert.False(string.IsNullOrEmpty(genero.UsuarioAlteracao));
            Assert.NotNull(genero.DataInclusao);
            Assert.NotNull(genero.DataAlteracao);
        }

        [Fact]
        public void EntidadeGenero_DeveCriarListaGeneros()
        {
            var generosFactory = new GenerosFactory();
            var listaGenero = generosFactory.GerarListaGeneros();

            Assert.True(listaGenero.Any());
            foreach (var item in listaGenero)
            {
                Assert.NotNull(item);
                Assert.NotEqual("00000000-0000-0000-0000-000000000000", item.Id.ToString());
                Assert.False(string.IsNullOrEmpty(item.Descricao));
                Assert.False(string.IsNullOrEmpty(item.Slug));
                Assert.False(string.IsNullOrEmpty(item.UsuarioInclusao));
                Assert.False(string.IsNullOrEmpty(item.UsuarioAlteracao));
                Assert.NotNull(item.DataInclusao);
                Assert.NotNull(item.DataAlteracao);
            }
        }

        #endregion

        #region => TESTES - GENEROS NOVELS

        [Fact]
        public void CriarGeneroNovelValido()
        {
            var idNovel = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0");
            var generoNovel = new GeneroNovel();
            generoNovel.AdicionaGeneroNovel(idNovel, Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"));

            Assert.NotNull(generoNovel);
            Assert.Equal(Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"), generoNovel.NovelId);
        }

        [Fact]
        public void DeveFalharAoCriarComIdNovelIncorreto()
        {
            var idNovel = Guid.Parse("64329027-9111-418c-a6ff-842689916083");
            var generoNovel = new GeneroNovel();
            generoNovel.AdicionaGeneroNovel(idNovel, Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"));

            Assert.NotEqual(Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"), generoNovel.NovelId);
        }

        [Fact]
        public void EntidadeGeneroNovel_DeveCriarUmGeneroNovel()
        {
            var generosFactory = new GenerosFactory();
            var generoNovel = generosFactory.GerarGeneroNovel();

            Assert.NotNull(generoNovel);
        }

        #endregion

        #region => TESTES - GENEROS COMICS

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

        [Fact]
        public void EntidadeGeneroNovel_DeveCriarUmGeneroComic()
        {
            var generosFactory = new GenerosFactory();
            var generoComic = generosFactory.GerarGeneroComic();

            Assert.NotNull(generoComic);
        }

        #endregion
    }
}