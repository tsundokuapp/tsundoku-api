using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers;

namespace TsundokuTraducoes.Entities.Tests.Capitulos
{
    public class CapituloNovelTestes
    {
        [Fact]
        public void CriarCapituloNovelIlustracoesValido()
        {
            var capituloNovelIlustracoes = new CapituloNovel();
            capituloNovelIlustracoes.AdicionaCapitulo(
                    Guid.Parse("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                    "Ilustrações",
                    "",
                    0,
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

            Assert.Equal("Ilustrações", capituloNovelIlustracoes.Numero);
            Assert.NotEmpty(capituloNovelIlustracoes.ConteudoNovel);
            Assert.NotNull(capituloNovelIlustracoes);
        }

        [Fact]
        public void DeveFalharAoCriarSemConteudoDasIlustracoes()
        {
            var capituloNovelIlustracoes = new CapituloNovel();
            capituloNovelIlustracoes.AdicionaCapitulo(
                    Guid.Parse("08dba6b4-3619-4cc6-8857-0bbe53a6f670"),
                    "Ilustra",
                    "",
                    0,
                    "",
                    "",
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
                        
            Assert.Empty(capituloNovelIlustracoes.ConteudoNovel);
        }

        [Fact]
        public void CriarCapituloNovelValido()
        {
            var capituloNovelIlustracoes = new CapituloNovel();
            capituloNovelIlustracoes.AdicionaCapitulo(
                    Guid.Parse("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                    "1",
                    "",
                    1,
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

            Assert.Equal("capitulo-1-pais-dos-magos", capituloNovelIlustracoes.Slug);
            Assert.NotEmpty(capituloNovelIlustracoes.ConteudoNovel);
            Assert.NotNull(capituloNovelIlustracoes);
        }

        [Fact]
        public void DeveFalharAoCriarSemConteudoDoCapitulo()
        {
            var capituloNovelIlustracoes = new CapituloNovel();
            capituloNovelIlustracoes.AdicionaCapitulo(
                    Guid.Parse("08dba6bb-8faf-4ce3-85d7-7cfe5b59648b"),
                    "1",
                    "",
                    1,
                    "País",
                    "",
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
                        
            Assert.Empty(capituloNovelIlustracoes.ConteudoNovel);
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
    }
}
