using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Data.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Services;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Tests.Infrastructure.Data.Repositories.Obra
{
    public class ObraRepositoryTestes
    {
        private readonly DbContextOptions<ContextBase> _options;

        public ObraRepositoryTestes() 
        {
            _options = new DbContextOptionsBuilder<ContextBase>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseRepository")
                .Options;            
        }

        #region => TESTES REPOSITORY - NOVEL        

        [Fact]
        public async Task RepositoryObra_DeveCadastrarUmaNovel()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retorno = await repository.AlteracoesSalvas();
                var resultado = repository.RetornaNovelPorId(novel.Id);

                // Assert
                Assert.True(retorno);
                Assert.NotNull(resultado);
                Assert.Equal("Bruxa Errante, a Jornada dos Testes", resultado.Titulo);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveAtualizarUmaNovel()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraCadastrada = repository.RetornaNovelPorId(novel.Id);

                var obraDTO = new ObraDTO { Id = novel.Id, Alias = "Bruxinha" };
                var obraAtualizada = repository.AtualizaNovel(obraDTO);

                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(resultadoObraCadastrada);
                Assert.Equal(resultadoObraCadastrada.Id, obraAtualizada.Id);
                Assert.Equal("Bruxinha", obraAtualizada.Alias);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveRetornarUmaListaDeNovels()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraCadastrada = repository.RetornaNovelPorId(novel.Id);
                var retorno = repository.RetornaListaNovels();


                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(resultadoObraCadastrada);
                Assert.True(retorno.Count > 0);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveRetornarUmaNovelPorSlugValido()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            var slug = "bruxa-errante-a-jornada-dos-testes";

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraCadastrada = repository.RetornaNovelPorId(novel.Id);
                var retorno = repository.RetornaNovelPorSlug(slug);

                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(resultadoObraCadastrada);
                Assert.NotNull(retorno);
                Assert.Equal(slug, retorno.Slug);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveExcluirUmaNovel()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                context.Novels.Add(novel);
                context.SaveChanges();                               
                                
                repository.ExcluiNovel(novel);
                var obraExcluida = await repository.AlteracoesSalvas();

                var resultadoObraExcluida = repository.RetornaNovelPorId(novel.Id);

                // Assert
                Assert.True(obraExcluida);
                Assert.Null(resultadoObraExcluida);
                Assert.Equal(0, context.Novels.Count());
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveRetornarUmaNovelExistente()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            var titulo = "Bruxa Errante, a Jornada dos Testes";

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraExistente = repository.RetornaNovelExistente(titulo);

                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(resultadoObraExistente);
                Assert.Equal(titulo, resultadoObraExistente.Titulo);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveInserirUmaListaDeGenerosNaNovel()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            var listaGenero = new List<string> { "Aventura", "Fantasia", "Seinen" };

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retornoObraSalva = await repository.AlteracoesSalvas();

                await repository.InsereGenerosNovel(novel, listaGenero, true);

                var retornoObraComListaGenero = repository.RetornaNovelPorId(novel.Id);

                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(retornoObraComListaGenero);
                Assert.True(retornoObraComListaGenero.GenerosNovel.Count == listaGenero.Count);
            }

            await Dispose();
        }

        #endregion

        #region => TESTES REPOSITORY - COMIC        

        [Fact]
        public async Task RepositoryObra_DeveCadastrarUmaComic()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var comic = obraRepositoryFactory.GerarComic();

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaComic(comic);
                var retorno = await repository.AlteracoesSalvas();
                var resultado = repository.RetornaComicPorId(comic.Id);

                // Assert
                Assert.True(retorno);
                Assert.NotNull(resultado);
                Assert.Equal("Hatsukoi Losstime", resultado.Titulo);
            }

            await Dispose();
        }

        [Fact]        
        public async Task RepositoryObra_DeveAtualizarUmaComic()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var comic = obraRepositoryFactory.GerarComic();

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                context.Comics.Add(comic);
                context.SaveChanges();
                
                var resultadoObraCadastrada = repository.RetornaComicPorId(comic.Id);

                var obraDTO = new ObraDTO { Id = comic.Id, Alias = "Tristeza do Bunas" };
                var obraAtualizada = repository.AtualizaComic(obraDTO);
                var retornoObraSalva = await repository.AlteracoesSalvas();

                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(resultadoObraCadastrada);
                Assert.Equal(resultadoObraCadastrada.Id, obraAtualizada.Id);
                Assert.Equal("Tristeza do Bunas", obraAtualizada.Alias);
            }           

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveRetornarUmaListaDeComics()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var comic = obraRepositoryFactory.GerarComic();

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaComic(comic);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraCadastrada = repository.RetornaComicPorId(comic.Id);
                var retorno = repository.RetornaListaComics();


                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(resultadoObraCadastrada);
                Assert.True(retorno.Count > 0);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveRetornarUmaComicPorSlugValido()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var comic = obraRepositoryFactory.GerarComic();
            var slug = "hatsukoi-losstime";

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaComic(comic);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraCadastrada = repository.RetornaComicPorId(comic.Id);
                var retorno = repository.RetornaComicPorSlug(slug);

                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(resultadoObraCadastrada);
                Assert.NotNull(retorno);
                Assert.Equal(slug, retorno.Slug);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveExcluirUmaComic()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var comic = obraRepositoryFactory.GerarComic();

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                context.Comics.Add(comic);
                context.SaveChanges();

                repository.ExcluiComic(comic);
                var obraExcluida = await repository.AlteracoesSalvas();

                var resultadoObraExcluida = repository.RetornaComicPorId(comic.Id);

                // Assert
                Assert.True(obraExcluida);
                Assert.Null(resultadoObraExcluida);
                Assert.Equal(0, context.Comics.Count());
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveRetornarUmaComicExistente()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var comic = obraRepositoryFactory.GerarComic();
            var titulo = "Hatsukoi Losstime";

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaComic(comic);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraExistente = repository.RetornaComicExistente(titulo);

                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(resultadoObraExistente);
                Assert.Equal(titulo, resultadoObraExistente.Titulo);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryObra_DeveInserirUmaListaDeGenerosNaComic()
        {
            // Arrange
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var comic = obraRepositoryFactory.GerarComic();
            var listaGenero = new List<string> { "Romance", "Vida Escolar", "Seinen" };

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaComic(comic);
                var retornoObraSalva = await repository.AlteracoesSalvas();

                await repository.InsereGenerosComic(comic, listaGenero, true);

                var retornoObraComListaGenero = repository.RetornaComicPorId(comic.Id);

                // Assert
                Assert.True(retornoObraSalva);
                Assert.NotNull(retornoObraComListaGenero);
                Assert.True(retornoObraComListaGenero.GenerosComic.Count == listaGenero.Count);
            }

            await Dispose();
        }

        #endregion

        #region => DISPOSE

        private async Task Dispose()
        {
            await using var context = new ContextBase(_options);
            await context.Database.EnsureDeletedAsync();
        }

        #endregion

    }
}
