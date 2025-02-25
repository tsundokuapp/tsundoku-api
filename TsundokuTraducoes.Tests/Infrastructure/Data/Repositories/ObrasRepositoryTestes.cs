using Microsoft.EntityFrameworkCore;
using System.Linq;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Data.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Services;
using TsundokuTraducoes.Entities.Entities.Generos;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Helpers;
using TsundokuTraducoes.Helpers.DTOs.Public.Request;
using TsundokuTraducoes.Tests.Infrastructure.Data.Repositories;

namespace TsundokuTraducoes.Infrastructure.Data.Repositories;

public class ObrasRepositoryTestes
{
    private Novel GerarNovel()
    {
        var novel = new Novel();
        novel.AdicionaNovel(
            Guid.NewGuid(),
            "Bruxa Errante, a Jornada dos Testes",
            "Majo no Tabitabi, The Journey of Elaina, The Witch's Travels, 魔女の旅々",
            "Bruxa Errante",
            "Shiraishi Jougi",
            "Azure",
            "2017",
            "bruxa-errante-a-jornada-dos-testes",
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
            "Em Andamento",
            "Light Novel",
            "japonesa",
            "Uma obra muito boa",
            false,
            true);

        return novel;
    }

    private Comic GerarComic()
    {
        var comic = new Comic();
        comic.AdicionaComic(
            Guid.NewGuid(),
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
            "Em Andamento",
            "Mangá",
            "japonesa",
            "",
            false);

        return comic;
    }

    [Fact]
    public async Task ObterNovelPorId_DeveRetornarNovelQuandoIdValido()
    {
        // Arrange
        var id = Guid.NewGuid();
        var novel = GerarNovel();
        novel.Id = id;

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
                (context, 
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));

            var serviceAdmin = new ObraService(repositoryAdmin);
            await serviceAdmin.AdicionaNovel(novel);
            
            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterNovelPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.Id);
        }
        
        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }

    [Fact]
    public async Task ObterNovelPorId_DeveRetornarNullQuandoNovelNaoExiste()
    {
        // Arrange
        var idGeradoExternamente = Guid.NewGuid();
        var novel = GerarNovel();

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;


        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
            (context, 
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));
            
            var serviceAdmin = new ObraService(repositoryAdmin);
            await serviceAdmin.AdicionaNovel(novel);
            
            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterNovelPorId(idGeradoExternamente);

            // Assert
            Assert.Null(resultado);
        }
        
        // Clean up
        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }

    [Fact]
    public async Task ObterNovelPorSlug_DeveRetornarNovelQuandoSlugValido()
    {
        // Arrange
        var slug = "slug-teste";
        var novel = GerarNovel();
        novel.Slug = slug;

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;


        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
                (context, 
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));
            
            var serviceAdmin = new ObraService(repositoryAdmin);
            await serviceAdmin.AdicionaNovel(novel);
            
            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterNovelPorSlug(slug);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(slug, resultado.Slug);
        }
        
        // Clean up
        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }

    [Fact]
    public async Task ObterNovelPorSlug_DeveRetornarNullQuandoNovelNaoExiste()
    {
        // Arrange
        var slug = "slug-teste";
        // slug da novel = 'bruxa-errante-a-jornada-dos-testes'
        var novel = GerarNovel();

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;


        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
            (context, 
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));
            
            var serviceAdmin = new ObraService(repositoryAdmin);
            await serviceAdmin.AdicionaNovel(novel);
            
            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterNovelPorSlug(slug);

            // Assert
            Assert.Null(resultado);
        }
        
        // Clean up
        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
 
    [Fact]
    public void TrataRetornoNovelUnica_DeveRetornarObjetoEsperado()
    {
        // Arrange
        var novel = GerarNovel();
        
        // Act
        var resultado = ObrasRepository.TrataRetornoNovelUnica(novel);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Bruxa Errante, a Jornada dos Testes", resultado.Titulo);
        Assert.Equal("Light Novel", resultado.TipoObra);
        Assert.Equal("Shiraishi Jougi", resultado.Autor);
    }
    
    [Fact]
    public async Task ObterComicPorId_DeveRetornarComicQuandoIdValido()
    {
        // Arrange
        var id = Guid.NewGuid();
        var comic = GerarComic();
        comic.Id = id;

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
            (context, 
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));

            var serviceAdmin = new ObraService(repositoryAdmin);
            await serviceAdmin.AdicionaComic(comic);
            
            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterComicPorId(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.Id);
        }
        
        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
    
    [Fact]
    public async Task ObterComicPorId_DeveRetornarNullQuandoComicNaoExiste()
    {
        // Arrange
        var idGeradoExternamente = Guid.NewGuid();
        var comic = GerarComic();

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;


        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
            (context, 
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));
            
            var serviceAdmin = new ObraService(repositoryAdmin);
            await serviceAdmin.AdicionaComic(comic);
            
            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterComicPorId(idGeradoExternamente);

            // Assert
            Assert.Null(resultado);
        }
        
        // Clean up
        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
    
    [Fact]
    public async Task ObterComicPorSlug_DeveRetornarNullQuandoComicNaoExiste()
    {
        // Arrange
        var slug = "slug-teste";
        // slug da comic = 'hatsukoi-losstime'
        var comic = GerarComic();

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;


        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
            (context, 
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));
            
            var serviceAdmin = new ObraService(repositoryAdmin);
            await serviceAdmin.AdicionaComic(comic);
            
            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterComicPorSlug(slug);

            // Assert
            Assert.Null(resultado);
        }
        
        // Clean up
        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
 
    [Fact]
    public void TrataRetornoComicUnica_DeveRetornarObjetoEsperado()
    {
        // Arrange
        var comic = GerarComic();
        
        // Act
        var resultado = ObrasRepository.TrataRetornoComicUnica(comic);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Hatsukoi Losstime", resultado.Titulo);
        Assert.Equal("Mangá", resultado.TipoObra);
        Assert.Equal("Nishina Yuuki", resultado.Autor);
    }

    [Fact]
    public async Task ObterListaNovel_ComUmGenero_DeveRetornarComGeneroEsperado()
    {
        // Arrange
        var id = Guid.NewGuid();
        var novel = GerarNovel();
        var obrasRepositoryMock = new ObrasRepositoryFactory();
        novel.Id = id;
        var genero = obrasRepositoryMock.GerarGenero("Fantasia");

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
                (context,
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));

            var serviceAdmin = new ObraService(repositoryAdmin);
            await context.Generos.AddAsync(genero);
            await context.SaveChangesAsync();

            await serviceAdmin.AdicionaNovel(novel);
            await serviceAdmin.InsereGenerosNovel(novel, ["Fantasia"], true);

            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterListaNovels(new RequestObras());

            // Assert
            Assert.True(resultado.Any());
            Assert.Equal(id, resultado[0].Id);
            Assert.Equal("Fantasia", resultado[0].ListaGeneros[0]);
        }

        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }

    [Fact]
    public async Task ObterListaNovel_ComListaGenero_DeveRetornarComGenerosEsperados()
    {
        // Arrange
        var id = Guid.NewGuid();
        var novel = GerarNovel();
        var obrasRepositoryMock = new ObrasRepositoryFactory();
        novel.Id = id;

        List<string> listaGeneros = ["Fantasia", "Aventure", "Seinen"];        
        var generosMockados = new List<Genero>();

        foreach (var genero in listaGeneros)
        {
            generosMockados.Add(obrasRepositoryMock.GerarGenero(genero));
        }

        var options = new DbContextOptionsBuilder<ContextBase>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        await using (var context = new ContextBase(options))
        {
            IObraRepository repositoryAdmin = new ObraRepository
                (context,
                new GeneroDeParaRepository(context),
                new GeneroRepository(context));

            var serviceAdmin = new ObraService(repositoryAdmin);

            foreach (var genero in generosMockados)
            {
                await context.Generos.AddAsync(genero);
                await context.SaveChangesAsync();
            }            

            await serviceAdmin.AdicionaNovel(novel);
            await serviceAdmin.InsereGenerosNovel(novel, listaGeneros, true);


            // Act
            var repositoryPublic = new ObrasRepository(context);
            var resultado = await repositoryPublic.ObterListaNovels(new RequestObras());

            // Assert
            Assert.True(resultado.Any());
            Assert.Equal(id, resultado[0].Id);
            Assert.Contains(listaGeneros[0], resultado[0].ListaGeneros);
            Assert.Contains(listaGeneros[1], resultado[0].ListaGeneros);
            Assert.Contains(listaGeneros[2], resultado[0].ListaGeneros);
        }

        await using (var context = new ContextBase(options))
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
}