using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TsundokuTraducoes.Api.Models.Capitulo;
using TsundokuTraducoes.Api.Models.DePara;
using TsundokuTraducoes.Api.Models.Generos;
using TsundokuTraducoes.Api.Models.Obra;
using TsundokuTraducoes.Api.Models.Volume;
using TsundokuTraducoes.Api.Utilidades;

namespace TsundokuTraducoes.Api.Data
{
    public class TsundokuContext : DbContext
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
        public TsundokuContext(DbContextOptions<TsundokuContext> options) : base(options) { }

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
               .HasData(new List<Genero>()
               {
                    new Genero { Id = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"), Descricao = "Aventura", Slug = "aventura"},
                    new Genero { Id = Guid.Parse("64329027-9111-418c-a6ff-842689916083"), Descricao = "Seinen", Slug = "seinen"},
                    new Genero { Id = Guid.NewGuid(), Descricao = "Ação", Slug = "acao"},
                    new Genero { Id = Guid.NewGuid(), Descricao = "Comédia", Slug = "comedia"},
                    new Genero { Id = Guid.NewGuid(), Descricao = "Drama", Slug = "drama"},
                    new Genero { Id = Guid.NewGuid(), Descricao = "Slice of Life", Slug = "slice-of-life"},
                    new Genero { Id = Guid.NewGuid(), Descricao = "Isekai", Slug = "isekai"},
                    new Genero { Id = Guid.NewGuid(), Descricao = "Harém", Slug = "harem"},
                    new Genero { Id = Guid.NewGuid(), Descricao = "Horror", Slug = "horror"},
                    new Genero { Id = Guid.NewGuid(), Descricao = "Fantasia", Slug = "fantasia"},
               });

            modelBuilder.Entity<GeneroNovel>()
                .HasData(new GeneroNovel
                {
                    GeneroId = Guid.Parse("707d2ef9-7fb7-451b-b3fc-be668664a7b0"),
                    NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                });

            modelBuilder.Entity<GeneroComic>()
                .HasData(new GeneroComic
                {
                    GeneroId = Guid.Parse("64329027-9111-418c-a6ff-842689916083"),
                    ComicId = Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                });

            modelBuilder.Entity<Novel>()
                .HasData(new Novel
                {
                    Id = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8"),
                    Titulo = "Bruxa Errante, a Jornada de Elaina",
                    TituloAlternativo = "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
                    Alias = "Bruxa Errante",
                    Autor = "Shiraishi Jougi",
                    Artista = "Azure",
                    Ano = "2017",
                    Slug = "bruxa-errante-a-jornada-de-elaina",
                    Visualizacoes = 0,
                    UsuarioInclusao = "Bravo",
                    UsuarioAlteracao = "",
                    ImagemCapaPrincipal = "https://tsundoku.com.br/wp-content/uploads/2021/12/MJ_V8_Capa.jpg",
                    Sinopse = "A Bruxa, Sim, eu.",
                    DataInclusao = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    EhObraMaiorIdade = false,
                    EhRecomendacao = false,
                    CodigoCorHexaObra = "#81F7F3",
                    ImagemBanner = "https://tsundoku.com.br/wp-content/uploads/2021/12/testeBanner.jpg",
                    CargoObraDiscord = "@Bruxa Errante, a Jornada de Elaina",
                    DiretorioImagemObra = Diretorios.RetornaDiretorioImagemCriado("BruxaErrante"),
                    StatusObraSlug = "em-andamento",
                    TipoObraSlug = "light-novel",
                    NacionalidadeSlug = "japonesa",
                    ImagemCapaUltimoVolume = "",
                    NumeroUltimoVolume = "",
                    SlugUltimoVolume = "",
                    NumeroUltimoCapitulo = "",
                    SlugUltimoCapitulo = "",
                    DataAtualizacaoUltimoCapitulo = null,
                });

            modelBuilder.Entity<Comic>()
                .HasData(new Comic
                {
                    Id = Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb"),
                    Titulo = "Hatsukoi Losstime",
                    TituloAlternativo = "初恋ロスタイム",
                    Alias = "Hatsukoi Losstime",
                    Autor = "Nishina Yuuki",
                    Artista = "Nanora & Zerokich",
                    Ano = "2019",
                    Slug = "hatsukoi-losstime",
                    Visualizacoes = 0,
                    UsuarioInclusao = "Bravo",
                    UsuarioAlteracao = "",
                    ImagemCapaPrincipal = "https://tsundoku.com.br/wp-content/uploads/2022/01/cover_hatsukoi_vol2.jpg",
                    Sinopse = "Em um mundo onde apenas duas pessoas se moviam...",
                    DataInclusao = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    EhObraMaiorIdade = false,
                    EhRecomendacao = false,
                    CodigoCorHexaObra = "#01DFD7",
                    ImagemBanner = "https://tsundoku.com.br/wp-content/uploads/2022/01/HatsukoiEmbed.jpg",
                    CargoObraDiscord = "@Hatsukoi Losstime",
                    DiretorioImagemObra = Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime"),
                    StatusObraSlug = "em-andamento",
                    TipoObraSlug = "comic",
                    NacionalidadeSlug = "japonesa",
                    ImagemCapaUltimoVolume = "",
                    NumeroUltimoVolume = "",
                    SlugUltimoVolume = "",
                    NumeroUltimoCapitulo = "",
                    SlugUltimoCapitulo = "",
                    DataAtualizacaoUltimoCapitulo = null,
                });

            modelBuilder.Entity<VolumeNovel>()
                .HasData(new VolumeNovel
                {
                    Id = Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                    Titulo = "",
                    Numero = "1",
                    Sinopse = "",
                    ImagemVolume = "https://tsundoku.com.br/wp-content/uploads/2021/01/Tsundoku-Traducoes-Majo-no-Tabitabi-Capa-Volume-01.jpg",
                    Slug = "volume-1",
                    UsuarioInclusao = "Bravo",
                    DataInclusao = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    DiretorioImagemVolume = Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01"),
                    NovelId = Guid.Parse("97722a6d-2210-434b-ae48-1a3c6da4c7a8")
                });

            modelBuilder.Entity<VolumeComic>()
                .HasData(new VolumeComic
                {
                    Id = Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                    Titulo = "",
                    Numero = "1",
                    Sinopse = "",
                    ImagemVolume = "https://tsundoku.com.br/wp-content/uploads/2022/01/Hatsukoi_cover.jpg",
                    Slug = "volume-1",
                    UsuarioInclusao = "Bravo",
                    DataInclusao = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    DiretorioImagemVolume = Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01"),
                    ComicId = Guid.Parse("3d6a759d-8c9e-4891-9f0e-89b8d99821cb")
                });

            modelBuilder.Entity<CapituloNovel>()
                .HasData(new CapituloNovel
                {
                    Id = Guid.Parse("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                    Numero = "Ilustrações",
                    Parte = null,
                    Titulo = null,
                    ConteudoNovel = RetornaConteudoNovelIlustracoes(),
                    Slug = "ilustracoes",
                    UsuarioInclusao = "Bravo",
                    DataInclusao =  DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    DiretorioImagemCapitulo = Diretorios.RetornaDiretorioImagemCriado("BruxaErrante", "Volume01", "Ilustracoes"),
                    OrdemCapitulo = 1,
                    EhIlustracoesNovel = true,
                    VolumeId = Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                    Tradutor = null,
                    Revisor = null,
                    QC = null,
                });

            modelBuilder.Entity<CapituloNovel>()
                .HasData(new CapituloNovel
                {
                    Id = Guid.Parse("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                    Numero = "1",
                    Parte = null,
                    Titulo = null,
                    ConteudoNovel = RetornaConteudoNovel(),
                    Slug = "capitulo-1-pais-dos-magos",
                    UsuarioInclusao = "Bravo",
                    DataInclusao = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    DiretorioImagemCapitulo = null,
                    OrdemCapitulo = 2,
                    EhIlustracoesNovel = true,
                    VolumeId = Guid.Parse("08dba651-c8ee-460a-8b4a-56573c446d2a"),
                    Tradutor = null,
                    Revisor = null,
                    QC = null,
                });

            modelBuilder.Entity<CapituloComic>()
                .HasData(new CapituloComic
                {
                    Id = Guid.Parse("08dba6c0-f903-469b-866c-223f5ab45e56"),
                    Numero = "1",
                    Parte = null,
                    Titulo = null,
                    ListaImagens = RetornaConteudoManga(),
                    Slug = "capitulo-1",
                    UsuarioInclusao = "Bravo",
                    DataInclusao = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    DiretorioImagemCapitulo = Diretorios.RetornaDiretorioImagemCriado("HatsukoiLosstime", "Volume01", "Capitulo01"),
                    OrdemCapitulo = 1,
                    VolumeId = Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50")
                });
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
