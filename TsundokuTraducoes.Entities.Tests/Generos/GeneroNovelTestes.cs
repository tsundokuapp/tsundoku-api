using TsundokuTraducoes.Entities.Entities.DePara;

namespace TsundokuTraducoes.Entities.Tests.Generos
{
    public class GeneroNovelTestes
    {
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
    }
}
