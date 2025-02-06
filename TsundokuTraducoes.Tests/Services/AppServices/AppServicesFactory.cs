using TsundokuTraducoes.Entities.Entities.Capitulo;
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
                                  false);

            listaNovels.Add(novel);
        }

        return listaNovels;
    }
}