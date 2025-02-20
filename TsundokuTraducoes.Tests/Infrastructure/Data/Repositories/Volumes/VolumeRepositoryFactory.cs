using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Tests.Infrastructure.Data.Repositories.Volumes
{
    public class VolumeRepositoryFactory
    {
        #region => MOCK VOLUME NOVEL

        public VolumeNovel GerarVolumeNovel(Guid? idObra)
        {
            var volume = new VolumeNovel();
            volume.AdicionaVolume(
                Guid.NewGuid(),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg",
                "volume-1",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01"),
                idObra ?? Guid.NewGuid());

            return volume;
        }

        public VolumeDTO GerarVolumeNovelDTO(Guid idVolume, Guid idObra)
        {
            return new VolumeDTO()
            {
                Id = idVolume,
                Numero = "1",
                ObraId = idObra,
                Sinopse = "A adorável bruxinha Elaina e suas viagens",
                Titulo = "Elaina, a bruxa cinzenta",
                UsuarioAlteracao = "Axios",
                UsuarioInclusao = "Bravo"
            };
        }

        #endregion

        #region => MOCK VOLUME COMIC

        public VolumeComic GerarVolumeComic(Guid? idObra)
        {
            var volume = new VolumeComic();
            volume.AdicionaVolume(
                Guid.NewGuid(),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2021/12/Shadow-Manga-V13.jpg",
                "volume-1",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("Shadow", "Volume01"),
                idObra ?? Guid.NewGuid());

            return volume;
        }

        public VolumeDTO GerarVolumeComicDTO(Guid idVolume, Guid idObra)
        {
            return new VolumeDTO()
            {
                Id = idVolume,
                Numero = "1",
                ObraId = idObra,
                Sinopse = "Da mesma forma que todos já adoraram heróis em sua infância, um certo jovem admirava aqueles que agiam nas sombras.",
                UsuarioAlteracao = "NERO_SL",
                UsuarioInclusao = "Bravo"
            };
        }

        #endregion
    }
}