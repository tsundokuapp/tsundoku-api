using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

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

    public Novel GerarNovel()
    {
        var novel = new Novel();
        novel.AdicionaNovel(
            Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
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

        var listaGeneros = GerarListaGeneroNovel();
        novel.AdicionaListaGeneroNovels(listaGeneros);
        novel.AtualizaDadosUltimoVolume(novel.ImagemCapaPrincipal, "01", "volume-01");

        return novel;
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

    public List<GeneroNovel> GerarListaGeneroNovel()
    {
        var listaGeneroNovel = new List<GeneroNovel>()
        {
            new GeneroNovel
            {
                GeneroId = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"),
                NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
            },

            new GeneroNovel
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916083"),
                NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
            },

            new GeneroNovel
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916084"),
                NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
            },

            new GeneroNovel
            {
                GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916085"),
                NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
            },
        };
        
        return listaGeneroNovel;
    }
}