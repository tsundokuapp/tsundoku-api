using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Data.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Helpers.DTOs.Admin;

namespace TsundokuTraducoes.Tests.Infrastructure.Data.Repositories.Generos
{
    public class GeneroRepositoryTestes
    {
        private readonly DbContextOptions<ContextBase> _options;

        public GeneroRepositoryTestes()
        {
            _options = new DbContextOptionsBuilder<ContextBase>()
                .UseInMemoryDatabase(databaseName: "TestDatabaseGeneroTeste")
                .Options;
        }

        #region => TESTES - GENEROS

        [Fact]
        public async Task RepositoryGenero_DeveCadastrarGenero()
        {
            // Arrange
            var generoRepositoryFactory = new GeneroRepositoryFactory();
            var genero = generoRepositoryFactory.GerarGenero("Ação", "acao");

            await using (var context = new ContextBase(_options))
            {
                IGeneroRepository repository = new GeneroRepository(context);

                // Act
                await repository.AdicionaGenero(genero);
                var retorno = await repository.AlteracoesSalvas();
                var resultado = await repository.RetornaGeneroPorId(genero.Id);

                // Assert 
                Assert.True(retorno);
                Assert.NotNull(resultado);
                Assert.Equal("Ação", resultado.Descricao);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryGenero_DeveCadastrarUmaListaDeGeneros()
        {
            // Arrange
            var generoRepositoryFactory = new GeneroRepositoryFactory();
            var dicionarioGeneros = new Dictionary<string, string>()
            {
                { "Ação","acao" },
                { "Aventura","aventura" },
                { "Fantasia","fantasia" },
            };

            var listaGenero = new List<Genero>();

            await using (var context = new ContextBase(_options))
            {
                IGeneroRepository repository = new GeneroRepository(context);

                foreach (var genero in dicionarioGeneros)
                {
                    // Act
                    var generoMock = new Genero() { Descricao = genero.Key, Slug = genero.Value };
                    await repository.AdicionaGenero(generoMock);
                    await repository.AlteracoesSalvas();
                    listaGenero.Add(generoMock);
                }

                // Assert 
                Assert.True(listaGenero.Count > 0);
                Assert.Equal(3, listaGenero.Count);
                Assert.NotNull(listaGenero.Where(x => x.Descricao == "Ação").FirstOrDefault());
                Assert.NotNull(listaGenero.Where(x => x.Descricao == "Aventura").FirstOrDefault());
                Assert.NotNull(listaGenero.Where(x => x.Descricao == "Fantasia").FirstOrDefault());
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryGenero_DeveAtualizarGenero()
        {
            // Arrange
            var generoRepositoryFactory = new GeneroRepositoryFactory();
            var genero = generoRepositoryFactory.GerarGenero("Ação", "acao");

            await using (var context = new ContextBase(_options))
            {
                IGeneroRepository repository = new GeneroRepository(context);

                // Act
                await repository.AdicionaGenero(genero);
                var retorno = await repository.AlteracoesSalvas();

                var generoDTO = new GeneroDTO() { Id = genero.Id, Descricao = "Fantasia Teste" };
                var generoAtualizado = repository.AtualizaGenero(generoDTO);

                // Assert 
                Assert.True(retorno);
                Assert.NotNull(generoAtualizado);
                Assert.Equal("Fantasia Teste", generoAtualizado.Descricao);
                Assert.Equal("fantasia-teste", generoAtualizado.Slug);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryGenero_DeveExcluirGenero()
        {
            // Arrange
            var generoRepositoryFactory = new GeneroRepositoryFactory();
            var genero = generoRepositoryFactory.GerarGenero("Ação", "acao");

            await using (var context = new ContextBase(_options))
            {
                IGeneroRepository repository = new GeneroRepository(context);

                // Act
                context.Generos.Add(genero);
                context.SaveChanges();

                repository.ExcluiGenero(genero);
                var generoExcluido = await repository.AlteracoesSalvas();

                var resultadoGeneroExcluido = await repository.RetornaGeneroPorId(genero.Id);

                // Assert
                Assert.True(generoExcluido);
                Assert.Null(resultadoGeneroExcluido);
                Assert.Equal(0, context.Novels.Count());
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryGenero_DeveRetornarGeneroExistentePorSlug()
        {
            // Arrange
            var generoRepositoryFactory = new GeneroRepositoryFactory();
            var genero = generoRepositoryFactory.GerarGenero("Ação", "acao");

            await using (var context = new ContextBase(_options))
            {
                IGeneroRepository repository = new GeneroRepository(context);

                // Act
                context.Generos.Add(genero);
                context.SaveChanges();

                var resultadoGeneroExistente = await repository.RetornaGeneroExistente(genero.Slug);

                // Assert
                Assert.NotNull(resultadoGeneroExistente);
                Assert.Equal(1, context.Generos.Count());
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