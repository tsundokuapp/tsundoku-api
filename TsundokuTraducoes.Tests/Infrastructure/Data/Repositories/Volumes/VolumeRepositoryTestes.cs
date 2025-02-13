using Microsoft.EntityFrameworkCore;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Data.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Repositories;

namespace TsundokuTraducoes.Tests.Infrastructure.Data.Repositories.Volumes
{
    public class VolumeRepositoryTestes
    {
        private readonly DbContextOptions<ContextBase> _options;

        public VolumeRepositoryTestes()
        {
            _options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabaseRepositoryVolumes")
            .Options;
        }

        #region => TESTES - VOLUME NOVEL

        [Fact]
        public async Task RepositoryVolume_DeveCadastrarVolumeNovel()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeNovel = volumeRepositoryFactory.GerarVolumeNovel(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                repository.AdicionaVolumeNovel(volumeNovel);
                var retorno = await repository.AlteracoesSalvas();
                var resultado = repository.RetornaVolumeNovelPorId(volumeNovel.Id);

                // Assert 
                Assert.True(retorno);
                Assert.NotNull(resultado);
                Assert.Equal("1", resultado.Numero);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryVolume_DeveAtualizarVolumeNovel()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeNovel = volumeRepositoryFactory.GerarVolumeNovel(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                context.VolumesNovel.Add(volumeNovel);
                context.SaveChanges();
                
                var volumeDTO = volumeRepositoryFactory.GerarVolumeNovelDTO(volumeNovel.Id, idObra);

                var volumeNovelAtualizado = repository.AtualizaVolumeNovel(volumeDTO);
                var retorno = await repository.AlteracoesSalvas();

                var resultado = repository.RetornaVolumeNovelPorId(volumeNovel.Id);

                // Assert 
                Assert.NotNull(resultado);
                Assert.Equal("1", resultado.Numero);
                Assert.True(retorno);
                Assert.NotNull(volumeNovelAtualizado);
                Assert.Equal("Elaina, a bruxa cinzenta", volumeNovelAtualizado.Titulo);
                Assert.Equal("A adorável bruxinha Elaina e suas viagens", volumeNovelAtualizado.Sinopse);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryVolume_DeveRetornarVolumeNovelExistente()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeNovel = volumeRepositoryFactory.GerarVolumeNovel(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                context.VolumesNovel.Add(volumeNovel);
                context.SaveChanges();

                var volumeDTO = volumeRepositoryFactory.GerarVolumeNovelDTO(volumeNovel.Id, idObra);

                var volumeNovelExistente = repository.RetornaVolumeNovelExistente(volumeDTO);

                // Assert 
                Assert.NotNull(volumeNovelExistente);
                Assert.Equal("volume-1", volumeNovelExistente.Slug);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryVolume_DeveRetornarUmaListaVolumeNovel()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeNovel = volumeRepositoryFactory.GerarVolumeNovel(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                context.VolumesNovel.Add(volumeNovel);
                context.SaveChanges();

                var listaVolumeNovel = repository.RetornaListaVolumesNovel(idObra); 

                // Assert 
                Assert.True(listaVolumeNovel.Count > 0);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryVolume_DeveExcluirVolumeNovel()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeNovel = volumeRepositoryFactory.GerarVolumeNovel(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                context.VolumesNovel.Add(volumeNovel);
                context.SaveChanges();

                repository.ExcluiVolumeNovel(volumeNovel);
                var retorno = await repository.AlteracoesSalvas();
                var resultado = repository.RetornaVolumeNovelPorId(volumeNovel.Id);

                // Assert 
                Assert.True(retorno);
                Assert.Null(resultado);
            }

            await Dispose();
        }

        #endregion

        #region => TESTES - VOLUME COMIC

        [Fact]
        public async Task RepositoryVolume_DeveCadastrarVolumeComic()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeComic = volumeRepositoryFactory.GerarVolumeComic(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                repository.AdicionaVolumeComic(volumeComic);
                var retorno = await repository.AlteracoesSalvas();
                var resultado = repository.RetornaVolumeComicPorId(volumeComic.Id);

                // Assert 
                Assert.True(retorno);
                Assert.NotNull(resultado);
                Assert.Equal("1", resultado.Numero);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryVolume_DeveAtualizarVolumeComic()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeComic = volumeRepositoryFactory.GerarVolumeComic(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                context.VolumesComic.Add(volumeComic);
                context.SaveChanges();

                var volumeDTO = volumeRepositoryFactory.GerarVolumeComicDTO(volumeComic.Id, idObra);

                var volumeComicAtualizado = repository.AtualizaVolumeComic(volumeDTO);
                var retorno = await repository.AlteracoesSalvas();

                var resultado = repository.RetornaVolumeComicPorId(volumeComic.Id);

                // Assert 
                Assert.NotNull(resultado);
                Assert.Equal("1", resultado.Numero);
                Assert.True(retorno);
                Assert.NotNull(volumeComicAtualizado);
                Assert.Equal("Da mesma forma que todos já adoraram heróis em sua infância, um certo jovem admirava aqueles que agiam nas sombras.", volumeComicAtualizado.Sinopse);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryVolume_DeveRetornarVolumeComicExistente()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeComic = volumeRepositoryFactory.GerarVolumeComic(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                context.VolumesComic.Add(volumeComic);
                context.SaveChanges();

                var volumeDTO = volumeRepositoryFactory.GerarVolumeComicDTO(volumeComic.Id, idObra);

                var volumeComicExistente = repository.RetornaVolumeComicExistente(volumeDTO);

                // Assert 
                Assert.NotNull(volumeComicExistente);
                Assert.Equal("volume-1", volumeComicExistente.Slug);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryVolume_DeveRetornarUmaListaVolumeComic()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeComic = volumeRepositoryFactory.GerarVolumeComic(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                context.VolumesComic.Add(volumeComic);
                context.SaveChanges();

                var listaVolumeComic = repository.RetornaListaVolumesComic(idObra);

                // Assert 
                Assert.True(listaVolumeComic.Count > 0);
            }

            await Dispose();
        }

        [Fact]
        public async Task RepositoryVolume_DeveExcluirVolumeComic()
        {
            // Arrange
            var volumeRepositoryFactory = new VolumeRepositoryFactory();
            var idObra = Guid.NewGuid();
            var volumeComic = volumeRepositoryFactory.GerarVolumeComic(idObra);

            await using (var context = new ContextBase(_options))
            {
                IVolumeRepository repository = new VolumeRepository(context);

                // Act
                context.VolumesComic.Add(volumeComic);
                context.SaveChanges();

                repository.ExcluiVolumeComic(volumeComic);
                var retorno = await repository.AlteracoesSalvas();
                var resultado = repository.RetornaVolumeComicPorId(volumeComic.Id);

                // Assert 
                Assert.True(retorno);
                Assert.Null(resultado);
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
