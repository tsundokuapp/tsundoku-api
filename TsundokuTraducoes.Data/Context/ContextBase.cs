using Microsoft.EntityFrameworkCore;
using System.Data;
using TsundokuTraducoes.Data.Configuration;
using TsundokuTraducoes.Data.Context.Interface;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Entities.Entities.DePara;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Data.Context
{
    public class ContextBase : DbContext, IContextBase
    {
        public DbSet<Comic> Comics { get; set; }
        public DbSet<Novel> Novels { get; set; }
        public DbSet<VolumeComic> VolumesComic { get; set; }
        public DbSet<VolumeNovel> VolumesNovel { get; set; }
        public DbSet<CapituloComic> CapitulosComic { get; set; }
        public DbSet<CapituloNovel> CapitulosNovel { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<GeneroNovel> GenerosNovel { get; set; }
        public DbSet<GeneroComic> GenerosComic { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();

        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string stringconexao = SourceConnection.RetornaConnectionStringConfig();
                optionsBuilder.UseMySql(stringconexao, ServerVersion.AutoDetect(stringconexao));
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeneroNovel>()
            .HasKey(gn => new { gn.NovelId, gn.GeneroId });

            modelBuilder.Entity<GeneroNovel>()
                .HasOne(gn => gn.Novel)
                .WithMany(novel => novel.GenerosNovel)
                .HasForeignKey(gn => gn.NovelId);

            modelBuilder.Entity<GeneroNovel>()
                .HasOne(gn => gn.Genero)
                .WithMany(genero => genero.GenerosNovel)
                .HasForeignKey(gn => gn.GeneroId);

            modelBuilder.Entity<GeneroComic>()
            .HasKey(gn => new { gn.ComicId, gn.GeneroId });

            modelBuilder.Entity<GeneroComic>()
                .HasOne(gn => gn.Comic)
                .WithMany(novel => novel.GenerosComic)
                .HasForeignKey(gn => gn.ComicId);

            modelBuilder.Entity<GeneroComic>()
                .HasOne(gn => gn.Genero)
                .WithMany(genero => genero.GenerosComic)
                .HasForeignKey(gn => gn.GeneroId);

            GerarSeed(modelBuilder);
        }

        private void GerarSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genero>()
               .HasData(CarregaListaGeneros());

            modelBuilder.Entity<GeneroNovel>()
                .HasData(CarregaListaGeneroNovel());

            modelBuilder.Entity<GeneroComic>()
                .HasData(CarregaListaGeneroComic());

            modelBuilder.Entity<Novel>()
                .HasData(CarregaNovel());

            modelBuilder.Entity<Comic>()
                .HasData(CarregaComic());

            modelBuilder.Entity<VolumeNovel>()
                .HasData(CarregaVolumeNovel());

            modelBuilder.Entity<VolumeComic>()
                .HasData(CarregaVolumeComic());

            modelBuilder.Entity<CapituloNovel>()
                .HasData(CarregaCapituloNovelIlustracoes());

            modelBuilder.Entity<CapituloNovel>()
                .HasData(CarregaCapituloNovel());

            modelBuilder.Entity<CapituloComic>()
                .HasData(CarregaCapítuloComic());
        }

        private List<Genero> CarregaListaGeneros()
        {

            var listaGeneros = new List<Genero>()
               {

                    new Genero{Id = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"), Descricao = "Aventura", Slug = "aventura" },
                    new Genero{Id = Guid.Parse("64329027-9111-418c-a6ff-842689916083"), Descricao =  "Seinen", Slug = "seinen" },
                    new Genero{Id = Guid.NewGuid(), Descricao = "Ação", Slug = "acao" },
                    new Genero{Id = Guid.NewGuid(), Descricao = "Comédia", Slug = "comedia" },
                    new Genero{Id = Guid.NewGuid(), Descricao = "Drama", Slug = "drama" },
                    new Genero{Id = Guid.NewGuid(), Descricao = "Slice of Life", Slug = "slice-of-life" },
                    new Genero{Id = Guid.NewGuid(), Descricao = "Isekai", Slug = "isekai" },
                    new Genero{Id = Guid.NewGuid(), Descricao = "Harém", Slug = "harem" },
                    new Genero{Id = Guid.NewGuid(), Descricao = "Horror", Slug = "horror" },
                    new Genero{Id = Guid.NewGuid(), Descricao = "Fantasia", Slug = "fantasia" },
               };

            return listaGeneros;
        }

        private List<GeneroNovel> CarregaListaGeneroNovel()
        {
            var listaGeneroNovel = new List<GeneroNovel>
            {
                new GeneroNovel{ NovelId = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"), GeneroId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8") }
            };

            return listaGeneroNovel;
        }

        private List<GeneroComic> CarregaListaGeneroComic()
        {
            var listaGeneroComic = new List<GeneroComic>
            {
                new GeneroComic{ ComicId = Guid.Parse("64329027-9111-418c-a6ff-842689916083"), GeneroId = Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb") }
            };

            return listaGeneroComic;
        }

        private Novel CarregaNovel()
        {
            var novel = new Novel();
            novel.AdicionaNovel(Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
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
                                  DateTime.Now,
                                  DateTime.Now,
                                  false,
                                  false,
                                  "#81F7F3",
                                  "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg",
                                  "@Bruxa Errante, a Jornada de Elaina",
                                  Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
                                  "em-andamento",
                                  "light-novel",
                                  "japonesa");

            novel.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg", "Volume 01", "volume-1");
            novel.AtualizaDadosUltimoCapitulo("Ilustrações", "ilustracoes", DateTime.Now);

            return novel;
        }

        private Comic CarregaComic()
        {
            var comic = new Comic();
            comic.AdicionaComic(Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                                  "Hatsukoi Losstime",
                                  "初恋ロスタイム",
                                  "Hatsukoi Losstime",
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
                                  false,
                                  "#01DFD7",
                                  "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg",
                                  "@Hatsukoi Losstime",
                                  Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
                                  "em-andamento",
                                  "comic",
                                  "japonesa");

            comic.AtualizaDadosUltimoVolume("https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg", "Volume 01", "volume-1");
            comic.AtualizaDadosUltimoCapitulo("Capítulo 01", "capitulo-1", DateTime.Now);

            return comic;
        }

        private VolumeNovel CarregaVolumeNovel()
        {
            var volumeNovel = new VolumeNovel();
            volumeNovel.AdicionaVolume(
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

            return volumeNovel;
        }

        private VolumeComic CarregaVolumeComic()
        {
            var volumeComic = new VolumeComic();
            volumeComic.AdicionaVolume(
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                "1",
                "https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg",
                "volume-1",
                "",
                "",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01"),
                Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"));

            return volumeComic;
        }

        private CapituloNovel CarregaCapituloNovelIlustracoes()
        {
            var capituloNovelIlustracao = new CapituloNovel();
            capituloNovelIlustracao.AdicionaCapitulo(
                    Guid.Parse("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                    "Ilustrações",
                    "",
                    1,
                    "",
                    RetornaConteudoNovelIlustracoes(),
                    "ilustracoes",
                    "Bravo",
                    "Bravo",
                    DateTime.Now,
                    DateTime.Now,
                    Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01", "Ilustracoes"),
                    true,
                    "",
                    "",
                    "",
                    Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"));

            return capituloNovelIlustracao;
        }

        private CapituloNovel CarregaCapituloNovel()
        {
            var capituloNovelIlustracao = new CapituloNovel();
            capituloNovelIlustracao.AdicionaCapitulo(
                    Guid.Parse("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                    "1",
                    "",
                    2,
                    "País dos Magos",
                    RetornaConteudoNovel(),
                    "capitulo-1-pais-dos-magos",
                    "Bravo",
                    "Bravo",
                    DateTime.Now,
                    DateTime.Now,
                    Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01", "Ilustracoes"),
                    false,
                    "",
                    "",
                    "",
                    Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"));

            return capituloNovelIlustracao;
        }

        private CapituloComic CarregaCapítuloComic()
        {
            var capituloComic = new CapituloComic();
            capituloComic.AdicionaCapitulo(
                Guid.Parse("08dba6c0-f903-469b-866c-223f5ab45e56"),
                "1",
                1,
                "",
                "",
                RetornaConteudoManga(),
                "capitulo-1",
                "Bravo",
                "Bravo",
                DateTime.Now,
                DateTime.Now,
                Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01", "Capitulo01"),
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"));

            return capituloComic;
        }

        private string RetornaConteudoNovelIlustracoes()
        {
            return @"[{\""Id\"": 1,\""Ordem\"": 1,\""Alt\"" = \""Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01\"",\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg\""},{\""Id\"": 2,\""Ordem\"": 2,\""Alt\"" = \""MJ_V1_ilust_01\"",\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_01.jpg\""},{\""Id\"": 3,\""Ordem\"": 3,\""Alt\"" = \""MJ_V1_ilust_02\"",\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_02.jpg\""},{\""Id\"": 4,\""Ordem\"": 4,\""Alt\"" = \""MJ_V1_ilust_03\"",\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_03.jpg\""},{\""Id\"": 5,\""Ordem\"": 5,\""Alt\"" = \""MJ_V1_ilust_04\"",\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V1_ilust_04.jpg\""}]";
        }

        private string RetornaConteudoNovel()
        {
            return @"
            <p>Era um país tranquilo, cercado por montanhas proibidas e escondido atrás de muros altos. Ninguém do mundo exterior poderia visitar.</p>
            <p>Acima de uma face rochosa brilhando com o calor da luz do sol, uma única vassoura voava pelo ar quente. A pessoa que a pilotava era uma linda jovem. Ela usava um robe preto e um chapéu pontudo, e seus cabelos cinzentos voavam ao vento. Se alguém estivesse por perto, viraria-se para olhar, imaginando com um suspiro quem seria aquela beldade a voar...</p>
            <p>Isso aí. Eu mesma.</p>
            <p>Ah, era uma piada.</p>
            <p>— Quase lá...</p>
            <p>O muro alto parecia ter sido esculpido na própria montanha. Olhando um pouco para baixo, vi o portão e guiei minha vassoura na direção dele.</p>
            <p>Com certeza foi trabalhoso, mas suponho que as pessoas que moravam aqui o haviam planejado dessa maneira – para impedir que as pessoas entrassem por engano. Afinal, não há como alguém caminhar por um lugar desses sem uma boa razão.</p>
            <p>Desci da minha vassoura bem em frente ao portão. Um sentinela local, aparentemente conduzindo inspeções de imigração, aproximou-se de mim.</p>
            <p>Depois de me olhar lentamente da cabeça aos pés e examinar o broche no meu peito, sorriu alegremente.</p>
            <p>— Bem-vinda ao País dos Magos. Por aqui, Madame Bruxa.</p>
            <p>— Hmm? Você não precisa testar se posso fazer magia ou não?</p>
            <p>Ouvi dizer que quem visitava este país tinha que provar sua capacidade mágica antes de entrar; qualquer pessoa que não alcançasse um determinado nível teria seu acesso negado.</p>
            <p>— Eu a vi voando. E, além disso, esse broche que está usando indica que é uma bruxa. Então, por favor, continue em frente.</p>
            <p><em>Ah sim, é mesmo. Ser capaz de voar em uma vassoura é um dos pré-requisitos mínimos para a entrada. É claro que puderam ver minha aproximação lá da guarita. Que boba que fui!</em></p>
            <p>Depois de me inclinar um pouco para o guarda, passei pelo portão enorme. Aqui estava o País dos Magos. Usuários iniciantes de magia, aprendizes e bruxas de pleno direito – desde que pudessem usar magia, estariam autorizados a entrar neste país curioso, enquanto todos os outros seriam impedidos.</p>
            <p>Ao passar pelo imenso portão, duas placas estranhas, lado a lado, chamaram minha atenção. Olhei para elas um pouco confusa.</p>
            <p>A primeira mostrava um mago montado em uma vassoura, com um círculo ao seu redor. Aquela ao lado mostrava a imagem de um soldado andando, com um triângulo em sua volta.</p>
            <p><em>O que há com essas placas?</em></p>
            <p>Eu soube a resposta assim que olhei para cima – acima do monte de casas de tijolos e sob o sol cintilante, magos de todos os tipos atravessavam o céu em todas as direções.</p>
            <p><em>Entendo. Deve ser uma regra nos países em que permitem apenas a entrada de magos – quase todo mundo está voando em uma vassoura, por isso poucas pessoas escolhem andar.</em></p>
            <p>Satisfeita com minha explicação para as placas, peguei minha vassoura e me sentei de lado. Com um impulso, levantei suavemente no ar em uma demonstração viva do desenho da placa.</p>
            ";
        }

        private string RetornaConteudoManga()
        {
            return @"[{\""Id\"": 1,\""Ordem\"": 1,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/0-46.jpg\""},{\""Id\"": 2,\""Ordem\"": 2,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/0-47.jpg\""},{\""Id\"": 3,\""Ordem\"": 3,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/1-60.jpg\""},{\""Id\"": 4,\""Ordem\"": 4,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/2-60.jpg\""},{\""Id\"": 5,\""Ordem\"": 5,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/3-61.jpg\""},{\""Id\"": 6,\""Ordem\"": 6,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/4-61.jpg\""},{\""Id\"": 7,\""Ordem\"": 7,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/5-61.jpg\""},{\""Id\"": 8,\""Ordem\"": 8,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/6-61.jpg\""},{\""Id\"": 9,\""Ordem\"": 9,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/7-61.jpg\""},{\""Id\"": 10,\""Ordem\"": 10,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/8-61.jpg\""},{\""Id\"": 10,\""Ordem\"": 10,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/9-61.jpg\""},{\""Id\"": 10,\""Ordem\"": 10,\""Url\"": \""http://tsundoku.com.br/wp-content/uploads/2022/01/10-108.jpg\""}]";
        }
    }
}
