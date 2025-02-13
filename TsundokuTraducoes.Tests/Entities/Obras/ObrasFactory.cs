using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Tests.Entities.Obras
{
    public class ObrasFactory
    {
        #region => MOCK NOVEL

        public Novel GerarNovel(string titulo, string tituloAlternativo, string alias,  string autor, string artista, 
            string ano, string slug, string usuarioInclusao, string usuarioAlteracao, string imagemCapaPrincipal, string sinopse, 
            string imagemBanner, string status, string tipo, string nacionalidade, string observacao)
        {
            var novel = new Novel();
            novel.AdicionaNovel(
                Guid.NewGuid(),
                titulo,
                tituloAlternativo,
                alias,
                autor,
                artista,
                ano,
                slug,
                usuarioInclusao,
                usuarioAlteracao,
                imagemCapaPrincipal,
                sinopse,
                DateTime.Now,
                DateTime.Now,
                false,
                false,
                "#81F7F3",
                imagemBanner,
                "@Bruxa Errante, a Jornada de Elaina",
                Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
                status,
                tipo,
                nacionalidade,
                observacao,
                false);

            novel.GenerosNovel = new List<GeneroNovel> { 
                new GeneroNovel { GeneroId = Guid.NewGuid(), NovelId = Guid.NewGuid() } 
            };

            return novel;
        }

        public Novel GerarNovelComAdicaoVolume(string titulo, string tituloAlternativo, string alias, string autor, string artista, 
            string ano, string slug, string usuarioInclusao, string usuarioAlteracao, string imagemCapaPrincipal, string sinopse,
            string imagemBanner, string status, string tipo, string nacionalidade, string observacao, string imagemUltimoVolume, 
            string numeroUltimoVolume, string slugUltimoVolume)
        {
            var novel = new Novel();
            novel.AdicionaNovel(
                Guid.NewGuid(),
                titulo,
                tituloAlternativo,
                alias,
                autor,
                artista,
                ano,
                slug,
                usuarioInclusao,
                usuarioAlteracao,
                imagemCapaPrincipal,
                sinopse,
                DateTime.Now,
                DateTime.Now,
                false,
                false,
                "#81F7F3",
                imagemBanner,
                "@Bruxa Errante, a Jornada de Elaina",
                Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
                status,
                tipo,
                nacionalidade,
                observacao,
                false);

            novel.GenerosNovel = new List<GeneroNovel> 
            {
                new GeneroNovel { GeneroId = Guid.NewGuid(), NovelId = Guid.NewGuid() }
            };

            novel.Volumes = new List<VolumeNovel>
            {
                new VolumeNovel()
            };
            
            novel.AtualizaDadosUltimoVolume(imagemUltimoVolume, numeroUltimoVolume, slugUltimoVolume);

            return novel;
        }

        public Novel GerarNovelComAdicaoCapitulo(string titulo, string tituloAlternativo, string alias, string autor, string artista,
            string ano, string slug, string usuarioInclusao, string usuarioAlteracao, string imagemCapaPrincipal, string sinopse,
            string imagemBanner, string status, string tipo, string nacionalidade, string observacao, string imagemUltimoVolume,
            string numeroUltimoVolume, string slugUltimoVolume, string numeroUltimoCapitulo, string slugUltimoCapitulo, DateTime dataAtualizacaoUltimoCapitulo)
        {
            var novel = new Novel();
            novel.AdicionaNovel(
                Guid.NewGuid(),
                titulo,
                tituloAlternativo,
                alias,
                autor,
                artista,
                ano,
                slug,
                usuarioInclusao,
                usuarioAlteracao,
                imagemCapaPrincipal,
                sinopse,
                DateTime.Now,
                DateTime.Now,
                false,
                false,
                "#81F7F3",
                imagemBanner,
                "@Bruxa Errante, a Jornada de Elaina",
                Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
                status,
                tipo,
                nacionalidade,
                observacao,
                false);

            novel.GenerosNovel = new List<GeneroNovel> 
            {
                new GeneroNovel { GeneroId = Guid.NewGuid(), NovelId = Guid.NewGuid() }
            };

            var volumeNovel = new VolumeNovel();
            volumeNovel.ListaCapitulo = new List<CapituloNovel>
            {
                new CapituloNovel()
            };

            novel.Volumes = new List<VolumeNovel> { volumeNovel };          

            novel.AtualizaDadosUltimoVolume(imagemUltimoVolume, numeroUltimoVolume, slugUltimoVolume);
            novel.AtualizaDadosUltimoCapitulo(numeroUltimoCapitulo, slugUltimoCapitulo, dataAtualizacaoUltimoCapitulo);

            return novel;
        }

        #endregion

        #region => MOCK COMIC

        public Comic GerarComic(string titulo, string tituloAlternativo, string alias, string autor, string artista,
            string ano, string slug, string usuarioInclusao, string usuarioAlteracao, string imagemCapaPrincipal, string sinopse,
            string imagemBanner, string status, string tipo, string nacionalidade, string observacao)
        {
            var comic = new Comic();
            comic.AdicionaComic(
                Guid.NewGuid(),
                titulo,
                tituloAlternativo,
                alias,
                autor,
                artista,
                ano,
                slug,
                usuarioInclusao,
                usuarioAlteracao,
                imagemCapaPrincipal,
                sinopse,
                DateTime.Now,
                DateTime.Now,
                false,
                false,
                "#01DFD7",
                imagemBanner,
                "@Hatsukoi Losstime",
                Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
                status,
                tipo,
                nacionalidade,
                observacao,
                false);

            return comic;
        }

        public Comic GerarComicComAdicaoVolume(string titulo, string tituloAlternativo, string alias, string autor, string artista,
            string ano, string slug, string usuarioInclusao, string usuarioAlteracao, string imagemCapaPrincipal, string sinopse,
            string imagemBanner, string status, string tipo, string nacionalidade, string observacao, string imagemUltimoVolume,
            string numeroUltimoVolume, string slugUltimoVolume)
        {
            var comic = new Comic();
            comic.AdicionaComic(
                Guid.NewGuid(),
                titulo,
                tituloAlternativo,
                alias,
                autor,
                artista,
                ano,
                slug,
                usuarioInclusao,
                usuarioAlteracao,
                imagemCapaPrincipal,
                sinopse,
                DateTime.Now,
                DateTime.Now,
                false,
                false,
                "#01DFD7",
                imagemBanner,
                "@Hatsukoi Losstime",
                Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
                status,
                tipo,
                nacionalidade,
                observacao,
                false);

            comic.GenerosComic = new List<GeneroComic>
            {
                new GeneroComic { GeneroId = Guid.NewGuid(), ComicId = Guid.NewGuid() }
            };

            comic.Volumes = new List<VolumeComic>
            {
                new VolumeComic()
            };

            comic.AtualizaDadosUltimoVolume(imagemUltimoVolume, numeroUltimoVolume, slugUltimoVolume);
            return comic;
        }

        public Comic GerarComicComAdicaoCapitulo(string titulo, string tituloAlternativo, string alias, string autor, string artista,
            string ano, string slug, string usuarioInclusao, string usuarioAlteracao, string imagemCapaPrincipal, string sinopse,
            string imagemBanner, string status, string tipo, string nacionalidade, string observacao, string imagemUltimoVolume,
            string numeroUltimoVolume, string slugUltimoVolume, string numeroUltimoCapitulo, string slugUltimoCapitulo, DateTime dataAtualizacaoUltimoCapitulo)
        {
            var comic = new Comic();
            comic.AdicionaComic(
                Guid.NewGuid(),
                titulo,
                tituloAlternativo,
                alias,
                autor,
                artista,
                ano,
                slug,
                usuarioInclusao,
                usuarioAlteracao,
                imagemCapaPrincipal,
                sinopse,
                DateTime.Now,
                DateTime.Now,
                false,
                false,
                "#01DFD7",
                imagemBanner,
                "@Hatsukoi Losstime",
                Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
                status,
                tipo,
                nacionalidade,
                observacao,
                false);

            comic.GenerosComic = new List<GeneroComic>
            {
                new GeneroComic { GeneroId = Guid.NewGuid(), ComicId = Guid.NewGuid() }
            };

            var volumeComic = new VolumeComic();            
            var capituloComic = new CapituloComic();
            capituloComic.ListaImagensJson = "[]";            
            volumeComic.ListaCapitulo = new List<CapituloComic>{ capituloComic };
            comic.Volumes = new List<VolumeComic> { volumeComic };            

            comic.AtualizaDadosUltimoVolume(imagemUltimoVolume, numeroUltimoVolume, slugUltimoVolume);
            comic.AtualizaDadosUltimoCapitulo(numeroUltimoCapitulo, slugUltimoCapitulo, dataAtualizacaoUltimoCapitulo);
            return comic;
        }

        #endregion
    }
}