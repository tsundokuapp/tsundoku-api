using TsundokuTraducoes.Entities.Entities.Generos;

namespace TsundokuTraducoes.Entities.Tests.Generos
{
    public class GenerosTestes
    {
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
    }
}
