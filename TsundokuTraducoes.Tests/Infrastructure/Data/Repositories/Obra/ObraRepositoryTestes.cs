using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Data.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Services;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Tests.Infrastructure.Data.Repositories.Obra
{
    public class ObraRepositoryTestes
    {
        private readonly DbContextOptions<ContextBase> _options;

        public ObraRepositoryTestes() 
        {
            _options = new DbContextOptionsBuilder<ContextBase>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;            
        }

        [Fact]
        public async Task RepositoryObra_DeveCadastrarUmaNovel()
        {
            // Arrange
            var id = Guid.NewGuid();
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            novel.Id = id;

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retorno = await repository.AlteracoesSalvas();
                var resultado = repository.RetornaNovelPorId(id);

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
            var id = Guid.NewGuid();
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            novel.Id = id;

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraCadastrada = repository.RetornaNovelPorId(id);

                var obraDTO = new ObraDTO { Id = id, Alias = "Bruxinha" };
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
            var id = Guid.NewGuid();
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            novel.Id = id;

            await using (var context = new ContextBase(_options))
            {
                IObraRepository repository = new ObraRepository
                    (context,
                    new GeneroDeParaRepository(context),
                    new GeneroRepository(context));

                // Act
                await repository.AdicionaNovel(novel);
                var retornoObraSalva = await repository.AlteracoesSalvas();
                var resultadoObraCadastrada = repository.RetornaNovelPorId(id);
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
            var id = Guid.NewGuid();
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            novel.Id = id;
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
                var resultadoObraCadastrada = repository.RetornaNovelPorId(id);
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
            var id = Guid.NewGuid();
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            novel.Id = id;

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
            var id = Guid.NewGuid();
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            novel.Id = id;
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
            var id = Guid.NewGuid();
            var obraRepositoryFactory = new ObraRepositoryFactory();
            var novel = obraRepositoryFactory.GerarNovel();
            novel.Id = id;
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

        private async Task Dispose()
        {
            await using var context = new ContextBase(_options);
            await context.Database.EnsureDeletedAsync();
        }
    }
}
