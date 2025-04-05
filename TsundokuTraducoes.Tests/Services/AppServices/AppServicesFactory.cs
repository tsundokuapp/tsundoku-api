using AutoFixture;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Tests.Utils;

namespace TsundokuTraducoes.Services.AppServices;

public class AppServicesFactory
{
    public CapituloNovel GerarCapituloNovel(Guid? idCapitulo, Guid? IdVolume)
    {
        return new CapituloNovel()
        {
            Id = idCapitulo ?? Guid.NewGuid(),
            VolumeId = IdVolume ?? Guid.NewGuid(),
            Numero = "1",
            Parte = "1",
            OrdemCapitulo = 1,
            Titulo = "Capítulo 1",
            ConteudoNovel = "Conteúdo do capítulo 1",
            Slug = "capitulo-1",
            UsuarioInclusao = "UsuarioTeste",
            UsuarioAlteracao = "UsuarioTeste",
            DataInclusao = new DateTime(2024, 1, 1, 10, 30, 0),
            DataAlteracao = new DateTime(2024, 1, 2, 14, 45, 0),
            EhIlustracoesNovel = false,
            Tradutor = "TradutorTeste",
            Revisor = "RevisorTeste",
            QC = "QCTeste",
            ListaImagensJson = "[]",
            Publicado = true
        };
    }

    public VolumeNovel GerarVolumeNovel(Guid? idVolume)
    {
        var volume = new VolumeNovel();
        volume.AdicionaVolume(
            Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
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
            Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"));
        
        return volume;
    }

    public VolumeComic GerarVolumeComic(Guid? idVolume)
    {
        var volume = new VolumeComic();
        volume.AdicionaVolume(
            Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
            "1",
            "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg",
            "volume-1",
            "",
            "",
            "Bravo",
            "Bravo",
            DateTime.Now,
            DateTime.Now,
            Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01"),
            Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"));

        return volume;
    }

    public List<Comic> GerarListaComicsRecomendadas(int quantidadeComicsNaLista)
    {
        var listaComics = new List<Comic>();

        for (int i = 0; i < quantidadeComicsNaLista; i++) 
        {
            var comic = new Comic();
            comic.AdicionaComic(Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                                  $"Hatsukoi Losstime",
                                  "初恋ロスタイム",
                                  $"Hatsukoi Losstime_{i}{i}",
                                  "Nishina Yuuki",
                                  "Nanora & Zerokich",
                                  "2019",
                                  "hatsukoi-losstime",
                                  "Bravo",
                                  "Bravo",
                                  "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg",
                                  "Em um mundo onde apenas duas pessoas se moviam...",
                                  DateTime.Now,
                                  DateTime.Now,
                                  false,
                                  true,
                                  "#01DFD7",
                                  "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg",
                                  "@Hatsukoi Losstime",
                                  Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
                                  "Em andamento",
                                  "Mangá",
                                  "Japonesa",
                                  "Uma obra muito boa",
                                  false);

            listaComics.Add(comic);
        }

        return listaComics;
    }

    public List<Novel> GerarListaNovelsRecomendadas(int quantidadeNovelsNaLista)
    {
        var listaNovels = new List<Novel>();

        for (int i = 0; i < quantidadeNovelsNaLista; i++)
        {
            var novel = new Novel();
            novel.AdicionaNovel(Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                                  "Bruxa Errante, a Jornada de Elaina",
                                  "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                                  $"Bruxa Errante_{i}{i}",
                                  "Shiraishi Jougi",
                                  "Azure",
                                  "2017",
                                  "bruxa-errante-a-jornada-de-elaina",
                                  "Bravo",
                                  "Bravo",
                                  "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg",
                                  "A Bruxa, Sim, sou eu.",
                                  DateTime.Now,
                                  DateTime.Now,
                                  false,
                                  true,
                                  "#81F7F3",
                                  "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg",
                                  "@Bruxa Errante, a Jornada de Elaina",
                                  Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
                                  "Em andamento",
                                  "Light Novel",
                                  "Japonesa",
                                  "Uma obra muito boa",
                                  false,
                                  true);

            listaNovels.Add(novel);
        }

        return listaNovels;
    }

    public Novel GerarNovelComVolumeComGeneros()
    {
        var idNovel = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8");
        var novel = new Novel();
        novel.AdicionaNovel(
            idNovel,
            "Bruxa Errante, a Jornada de Elaina",
            "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
            "Bruxa Errante",
            "Shiraishi Jougi",
            "Azure",
            "2017",
            "bruxa-errante-a-jornada-de-elaina",
            "Bravo",
            "Bravo",
            "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg",
            "A Bruxa, Sim, sou eu.",
            new DateTime(2024, 1, 1, 10, 30, 0),
            new DateTime(2024, 1, 2, 10, 30, 0),
            false,
            false,
            "#81F7F3",
            "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg",
            "@Bruxa Errante, a Jornada de Elaina",
            Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
            "Em andamento",
            "Light Novel",
            "Japonesa",
            "Observação sobre a jornada da bruxinha mais linda!",
            false,
            true);

        var listaGeneros = GerarListaGeneros();
        var listaGeneroNovels = GerarListaGeneroNovel(listaGeneros);
        novel.Volumes.Add(GerarVolumeNovel(idNovel));
        novel.AdicionaListaGeneroNovels(listaGeneroNovels);
        novel.AtualizaDadosUltimoVolume(novel.Volumes.First().ImagemVolume, novel.Volumes.First().Numero, novel.Volumes.First().Slug);

        return novel;
    }

    public Comic GerarComicComVolumeComGeneros()
    {
        var idComic = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8");
        var comic = new Comic();
        comic.AdicionaComic(
            idComic,
            "Hatsukoi Losstime",
            "初恋ロスタイム",
            "Hatsukoi Losstime",
            "Nishina Yuuki",
            "Nanora & Zerokich",
            "2017",
            "hatsukoi-losstime",
            "Bravo",
            "Bravo",
            "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg",
            "A Bruxa, Sim, sou eu.",
            new DateTime(2024, 1, 1, 10, 30, 0),
            new DateTime(2024, 1, 2, 10, 30, 0),
            false,
            false,
            "#81F7F3",
            "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg",
            "@Hatsukoi Losstime",
            Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
            "Em andamento",
            "Mangá",
            "Japonesa",
            "Observação sobre um romance lindinho!",
            false);

        var listaGeneros = GerarListaGeneros();
        var listaGeneroComics = GerarListaGeneroComic(listaGeneros);
        comic.Volumes.Add(GerarVolumeComic(idComic));
        comic.AdicionaListaGeneroComics(listaGeneroComics);
        comic.AtualizaDadosUltimoVolume(comic.Volumes.First().ImagemVolume, comic.Volumes.First().Numero, comic.Volumes.First().Slug);

        return comic;
    }

    public List<Genero> GerarListaGeneros()
    {
        var listaGeneros = new List<Genero>()
        {
            new Genero{Id = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"), Descricao = "Aventura", Slug = "aventura" },
            new Genero{Id = Guid.Parse("64329027-9111-418c-a6ff-842689916083"), Descricao =  "Seinen", Slug = "seinen" },
            new Genero{Id = Guid.Parse("64329027-9111-418c-a6ff-842689916084"), Descricao = "Drama", Slug = "drama" },
            new Genero{Id = Guid.Parse("64329027-9111-418c-a6ff-842689916085"), Descricao = "Fantasia", Slug = "fantasia" }
        };

        return listaGeneros;
    }

    public List<GeneroNovel> GerarListaGeneroNovel(List<Genero> listaGeneros)
    {
        var listaGeneroNovel = new List<GeneroNovel>()
        {
            new GeneroNovel
            {
                GeneroId = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"),
                NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                Genero = listaGeneros.FirstOrDefault(x => x.Id == Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0")),
            },

            new GeneroNovel
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916083"),
                NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                Genero = listaGeneros.FirstOrDefault(x => x.Id == Guid.Parse("64329027-9111-418c-a6ff-842689916083")),
            },

            new GeneroNovel
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916084"),
                NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                Genero = listaGeneros.FirstOrDefault(x => x.Id == Guid.Parse("64329027-9111-418c-a6ff-842689916084")),
            },

            new GeneroNovel
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916085"),
                NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                Genero = listaGeneros.FirstOrDefault(x => x.Id == Guid.Parse("64329027-9111-418c-a6ff-842689916085")),
            },
        };

        return listaGeneroNovel;
    }

    public List<GeneroComic> GerarListaGeneroComic(List<Genero> listaGeneros)
    {
        var listaGeneroNovel = new List<GeneroComic>()
        {
            new GeneroComic
            {
                GeneroId = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"),
                ComicId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                Genero = listaGeneros.FirstOrDefault(x => x.Id == Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0")),
            },

            new GeneroComic
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916083"),
                ComicId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                Genero = listaGeneros.FirstOrDefault(x => x.Id == Guid.Parse("64329027-9111-418c-a6ff-842689916083")),
            },

            new GeneroComic
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916084"),
                ComicId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                Genero = listaGeneros.FirstOrDefault(x => x.Id == Guid.Parse("64329027-9111-418c-a6ff-842689916084")),
            },

            new GeneroComic
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916085"),
                ComicId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                Genero = listaGeneros.FirstOrDefault(x => x.Id == Guid.Parse("64329027-9111-418c-a6ff-842689916085")),
            },
        };

        return listaGeneroNovel;
    }

    public List<Genero> GerarListaGenerosPorParametros(List<string> listaGenero)
    {
        var listaGeneros = new List<Genero>();

        foreach (var descricaoGenero in listaGenero)
        {
            listaGeneros
                .Add(FixtureCustomizado
                    .RetornaFixtureCustomizado
                    .Build<Genero>()
                    .With(x => x.Descricao, descricaoGenero)
                    .Create()
                );
        }

        return listaGeneros;
    }

    public Novel GerarNovelComBanner(string banner)
    {
        return FixtureCustomizado
            .RetornaFixtureCustomizado
            .Build<Novel>()
            .With(x => x.ImagemBanner, banner)
            .With(x => x.GenerosNovel, new List<GeneroNovel>())
            .Create();
    }

    public Comic GerarComicComBanner(string banner)
    {
        return FixtureCustomizado
            .RetornaFixtureCustomizado
            .Build<Comic>()
            .With(x => x.ImagemBanner, banner)
            .With(x => x.GenerosComic, new List<GeneroComic>())
            .Create();
    }

    public List<GeneroNovel> GerarNovel_Com_ListaGeneroNovels(Novel novel, List<Genero> listaGeneros)
    {
        novel.GenerosNovel = new List<GeneroNovel>();

        foreach (var item in listaGeneros)
        {
            var generoNovel = FixtureCustomizado
                .RetornaFixtureCustomizado
                .Build<GeneroNovel>()
                .With(x => x.Genero, item)
                .Create();

           novel.GenerosNovel.Add(generoNovel);
        }

        return novel.GenerosNovel;
    }

    public List<GeneroComic> GerarComic_Com_ListaGeneroComics(Comic comic, List<Genero> listaGeneros)
    {
        comic.GenerosComic = new List<GeneroComic>();

        foreach (var item in listaGeneros)
        {
            var generoNovel = FixtureCustomizado
                .RetornaFixtureCustomizado
                .Build<GeneroComic>()
                .With(x => x.Genero, item)
                .Create();

            comic.GenerosComic.Add(generoNovel);
        }

        return comic.GenerosComic;
    }

    public List<Novel> GerarListaNovelsRecentes(Dictionary<string, DateTime> dicionarioObrasRecentes)
    {
        var listaNovels = new List<Novel>();

        var capitulo = FixtureCustomizado.RetornaFixtureCustomizado.Build<CapituloNovel>().Create();
        var volume = FixtureCustomizado.RetornaFixtureCustomizado.Build<VolumeNovel>().With(x => x.ListaCapitulo, [capitulo]).Create();

        foreach (var obra in dicionarioObrasRecentes)
        {
            listaNovels
                .Add(FixtureCustomizado
                    .RetornaFixtureCustomizado
                    .Build<Novel>()
                    .With(x => x.Titulo, obra.Key)
                    .With(x => x.DataAtualizacaoUltimoCapitulo, obra.Value)
                    .With(x => x.Volumes, [volume])
                    .Create()
                );
        }
        
        return listaNovels.Where(x => x.Volumes.Where(x => x.ListaCapitulo.Count != 0).Any())
                .OrderByDescending(o => o.DataAtualizacaoUltimoCapitulo)
                .ToList();
    }

    public List<Comic> GerarListaComicsRecentes(Dictionary<string, DateTime> dicionarioObrasRecentes)
    {
        var listaComics = new List<Comic>();

        var capitulo = FixtureCustomizado.RetornaFixtureCustomizado.Build<CapituloComic>().Create();
        var volume = FixtureCustomizado.RetornaFixtureCustomizado.Build<VolumeComic>().With(x => x.ListaCapitulo, [capitulo]).Create();

        foreach (var obra in dicionarioObrasRecentes)
        {
            listaComics
                .Add(FixtureCustomizado
                    .RetornaFixtureCustomizado
                    .Build<Comic>()
                    .With(x => x.Titulo, obra.Key)
                    .With(x => x.DataAtualizacaoUltimoCapitulo, obra.Value)
                    .With(x => x.Volumes, [volume])
                    .Create()
                );
        }

        return listaComics.Where(x => x.Volumes.Where(x => x.ListaCapitulo.Count != 0).Any())
                .OrderByDescending(o => o.DataAtualizacaoUltimoCapitulo)
                .ToList();
    }
}