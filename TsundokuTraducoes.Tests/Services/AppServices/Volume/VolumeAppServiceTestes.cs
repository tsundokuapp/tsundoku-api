using AutoMapper;
using HttpContextMoq;
using HttpContextMoq.Extensions;
using Moq;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Obra;
using TsundokuTraducoes.Entities.Entities.Volume;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices;
using TsundokuTraducoes.Services.Profiles;

namespace TsundokuTraducoes.Tests.Services.AppServices.Volume
{
    public class VolumeAppServiceTestes
    {
        private readonly IMapper _mapper;
        private readonly Mock<IVolumeComicService> _volumeComicServiceMock;
        private readonly Mock<IObraService> _ObraServiceMock;
        private readonly VolumeComicAppService _volumeComicAppServiceMock;
        private readonly Mock<IVolumeNovelService> _volumeNovelServiceMock;
        private readonly VolumeNovelAppService _volumeNovelAppServiceMock;

        public VolumeAppServiceTestes()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VolumeProfile());
                cfg.AddProfile(new CapituloProfile());
                cfg.AddProfile(new ObraProfile());
                cfg.AddProfile(new GeneroProfile());
            });

            _mapper = config.CreateMapper();
            _volumeComicServiceMock = new Mock<IVolumeComicService>();
            _ObraServiceMock = new Mock<IObraService>();
            _volumeNovelServiceMock = new Mock<IVolumeNovelService>();

            _volumeComicAppServiceMock = new VolumeComicAppService(
               _volumeComicServiceMock.Object,
               _ObraServiceMock.Object,
               _mapper
            );

            _volumeNovelAppServiceMock = new VolumeNovelAppService(
               _volumeNovelServiceMock.Object,
               _ObraServiceMock.Object,
               _mapper
            );
        }

        #region => TESTES VOLUME COMIC APP SERVICE

        [Fact]
        public void DeveRetornarVolume_ComLinksParaProximoEAnterior_NaBuscaPorIdObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = volumeAppServiceFactory.GerarComic(IdObra);
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}?skip=1";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeComic = new List<RetornoVolumeComic>();

            listaVolumesComic
                .ForEach(volumeComic => retornoListaRetornoVolumeComic
                    .Add(_volumeComicAppServiceMock
                        .TrataRetornoVolumeComic(volumeComic)
                    )
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesComics(httpContext, retornoListaRetornoVolumeComic, 1, null);

            Assert.Equal(6, objetoRetorno.Data.Count);
            Assert.Equal(7, objetoRetorno.Total);
            Assert.Contains($"{comic.Id}?Skip=7&Take=6", objetoRetorno.Proxima);
            Assert.Contains($"{comic.Id}?Skip=0&Take=6", objetoRetorno.Anterior);
        }

        [Fact]
        public void DeveRetornarVolume_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = volumeAppServiceFactory.GerarComic(IdObra);
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}?skip=0&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeComic = new List<RetornoVolumeComic>();

            listaVolumesComic
                .ForEach(volumeComic => retornoListaRetornoVolumeComic
                    .Add(_volumeComicAppServiceMock
                        .TrataRetornoVolumeComic(volumeComic)
                    )
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesComics(httpContext, retornoListaRetornoVolumeComic, 0, 6);

            Assert.Equal(6, objetoRetorno.Data.Count);
            Assert.Equal(7, objetoRetorno.Total);
            Assert.Contains($"{comic.Id}?Skip=6&Take=6", objetoRetorno.Proxima);
            Assert.Null(objetoRetorno.Anterior);
        }

        [Fact]
        public void DeveRetornarVolume_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = volumeAppServiceFactory.GerarComic(IdObra);
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}?skip=6&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeComic = new List<RetornoVolumeComic>();

            listaVolumesComic
                .ForEach(volumeComic => retornoListaRetornoVolumeComic
                    .Add(_volumeComicAppServiceMock
                        .TrataRetornoVolumeComic(volumeComic)
                    )
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesComics(httpContext, retornoListaRetornoVolumeComic, 6, 6);

            Assert.Single(objetoRetorno.Data);
            Assert.Equal(7, objetoRetorno.Total);
            Assert.Null(objetoRetorno.Proxima);
            Assert.Contains($"{comic.Id}?Skip=0&Take=6", objetoRetorno.Anterior);
        }

        [Fact]
        public void DeveRetornarFalse_QuandoVolumeNaoEncontrado_NaBuscaPorIdObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsVolumes = [];
            List<Guid> listaIdsCapitulos = [];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = volumeAppServiceFactory.GerarComic(IdObra);
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}?skip=6&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeComic = new List<RetornoVolumeComic>();

            listaVolumesComic
                .ForEach(volumeComic => retornoListaRetornoVolumeComic
                    .Add(_volumeComicAppServiceMock
                        .TrataRetornoVolumeComic(volumeComic)
                    )
                );

            var result = _volumeComicAppServiceMock.ValidaExisteListaVolume(listaVolumesComic);

            Assert.False(result.IsSuccess);
            Assert.Contains("Lista de volumes não encontrado para essa obra!", result.Errors[0].Message);
        }

        [Fact]
        public async Task DeveRetornarNull_QuandoObraNaoEncontrada_NaBuscaPorIdObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = null;
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            // Mock
            _ObraServiceMock.Setup(x => x.RetornaComicPorId(IdObra)).Returns(comic);

            // Assert
            var result = await _volumeComicAppServiceMock.ObterVolumesComicPorIdObra(IdObra);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ValueOrDefault);
            Assert.Contains("Obra não encontrada!", result.Errors[0].Message);
        }


        [Fact]
        public void DeveRetornarVolume_ComLinksParaProximoEAnterior_NaBuscaPorSlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = volumeAppServiceFactory.GerarComic(IdObra, slugObra);
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}?skip=1";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeComic = new List<RetornoVolumeComic>();

            listaVolumesComic
                .ForEach(volumeComic => retornoListaRetornoVolumeComic
                    .Add(_volumeComicAppServiceMock
                        .TrataRetornoVolumeComic(volumeComic)
                    )
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesComics(httpContext, retornoListaRetornoVolumeComic, 1, null);

            Assert.Equal(6, objetoRetorno.Data.Count);
            Assert.Equal(7, objetoRetorno.Total);
            Assert.Contains($"{comic.Slug}?Skip=7&Take=6", objetoRetorno.Proxima);
            Assert.Contains($"{comic.Slug}?Skip=0&Take=6", objetoRetorno.Anterior);
        }

        [Fact]
        public void DeveRetornarVolume_ComLinksParaProximoEAnteriorNull_NaBuscaPorSlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = volumeAppServiceFactory.GerarComic(IdObra, slugObra);
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}?skip=0&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeComic = new List<RetornoVolumeComic>();

            listaVolumesComic
                .ForEach(volumeComic => retornoListaRetornoVolumeComic
                    .Add(_volumeComicAppServiceMock
                        .TrataRetornoVolumeComic(volumeComic)
                    )
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesComics(httpContext, retornoListaRetornoVolumeComic, 0, 6);

            Assert.Equal(slugObra, comic.Slug);
            Assert.Equal(6, objetoRetorno.Data.Count);
            Assert.Equal(7, objetoRetorno.Total);
            Assert.Contains($"{comic.Slug}?Skip=6&Take=6", objetoRetorno.Proxima);
            Assert.Null(objetoRetorno.Anterior);
        }

        [Fact]
        public void DeveRetornarVolume_ComLinksParaAnteriorEProximoNull_NaBuscaPorSlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = volumeAppServiceFactory.GerarComic(IdObra, slugObra);
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Slug}?skip=6&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeComic = new List<RetornoVolumeComic>();

            listaVolumesComic
                .ForEach(volumeComic => retornoListaRetornoVolumeComic
                    .Add(_volumeComicAppServiceMock
                        .TrataRetornoVolumeComic(volumeComic)
                    )
                );

            var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesComics(httpContext, retornoListaRetornoVolumeComic, 6, 6);

            Assert.Equal(slugObra, comic.Slug);
            Assert.Single(objetoRetorno.Data);
            Assert.Equal(7, objetoRetorno.Total);
            Assert.Contains($"{comic.Slug}?Skip=0&Take=6", objetoRetorno.Anterior);
            Assert.Null(objetoRetorno.Proxima);
        }

        [Fact]
        public void DeveRetornarFalse_QuandoVolumeNaoEncontrado_NaBuscaPorIdVolumeESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsVolumes = [];
            List<Guid> listaIdsCapitulos = [];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = volumeAppServiceFactory.GerarComic(IdObra, slugObra);
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{comic.Id}?skip=6&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeComic = new List<RetornoVolumeComic>();

            listaVolumesComic
                .ForEach(volumeComic => retornoListaRetornoVolumeComic
                    .Add(_volumeComicAppServiceMock
                        .TrataRetornoVolumeComic(volumeComic)
                    )
                );

            var result = _volumeComicAppServiceMock.ValidaExisteListaVolume(listaVolumesComic);

            Assert.False(result.IsSuccess);
            Assert.Contains("Lista de volumes não encontrado para essa obra!", result.Errors[0].Message);
        }

        [Fact]
        public async Task DeveRetornarNull_QuandoObraNaoEncontrada_NaBuscaPorIdVolumeESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-comic-teste";

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Comic comic = null;
            List<VolumeComic> listaVolumesComic = volumeAppServiceFactory.GerarListaVolumeComic(IdObra, listaIdsVolumes, listaIdsCapitulos);

            // Mock
            _ObraServiceMock.Setup(x => x.RetornaComicPorSlug(slugObra)).Returns(comic);

            // Assert
            var result = await _volumeComicAppServiceMock.ObterVolumesComicPorIdObra(IdObra);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ValueOrDefault);
            Assert.Contains("Obra não encontrada!", result.Errors[0].Message);
        }

        #endregion

        #region => TESTES VOLUME NOVEL APP SERVICE

        //[Fact]
        //public void DeveRetornarVolumeNovel_ComLinksParaProximoEAnterior_NaBuscaPorIdObra()
        //{
        //    // Arrange
        //    var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

        //    List<Guid> listaIdsVolumes =
        //    [
        //        Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
        //        Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
        //        Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
        //        Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
        //        Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
        //        Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
        //        Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
        //    ];

        //    List<Guid> listaIdsCapitulos =
        //    [
        //        Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
        //        Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
        //        Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
        //        Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
        //    ];

        //    var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
        //    Novel novel = volumeAppServiceFactory.GerarNovel(IdObra);
        //    List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

        //    var scheme = "https";
        //    var host = "localhost";
        //    var path = $"/mock/{novel.Id}?skip=1";
        //    var url = $"{scheme}://{host}/{path}";
        //    var httpContext = new HttpContextMock().SetupUrl(url);

        //    // Assert
        //    var retornoListaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

        //    listaVolumesNovel
        //        .ForEach(volumeNovel => retornoListaRetornoVolumeNovel
        //            .Add(_volumeNovelAppServiceMock
        //                .TrataRetornoVolumeNovel(volumeNovel)
        //            )
        //        );

        //    var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(httpContext, retornoListaRetornoVolumeNovel, 1, null);

        //    Assert.Equal(6, objetoRetorno.Data.Count);
        //    Assert.Equal(7, objetoRetorno.Total);
        //    Assert.Contains($"{novel.Id}?Skip=7&Take=6", objetoRetorno.Proxima);
        //    Assert.Contains($"{novel.Id}?Skip=0&Take=6", objetoRetorno.Anterior);
        //}

        //[Fact]
        //public void DeveRetornarVolumeNovel_ComLinksParaProximoEAnteriorNull_NaBuscaPorIdObra()
        //{
        //    // Arrange
        //    var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

        //    List<Guid> listaIdsVolumes =
        //    [
        //        Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
        //        Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
        //        Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
        //        Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
        //        Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
        //        Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
        //        Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
        //    ];

        //    List<Guid> listaIdsCapitulos =
        //    [
        //        Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
        //        Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
        //        Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
        //        Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
        //    ];

        //    var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
        //    Novel novel = volumeAppServiceFactory.GerarNovel(IdObra);
        //    List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

        //    var scheme = "https";
        //    var host = "localhost";
        //    var path = $"/mock/{novel.Id}?skip=0&take=6";
        //    var url = $"{scheme}://{host}/{path}";
        //    var httpContext = new HttpContextMock().SetupUrl(url);

        //    // Assert
        //    var retornoListaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

        //    listaVolumesNovel
        //        .ForEach(volumeNovel => retornoListaRetornoVolumeNovel
        //            .Add(_volumeNovelAppServiceMock
        //                .TrataRetornoVolumeNovel(volumeNovel)
        //            )
        //        );

        //    var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(httpContext, retornoListaRetornoVolumeNovel, 0, 6);

        //    Assert.Equal(6, objetoRetorno.Data.Count);
        //    Assert.Equal(7, objetoRetorno.Total);
        //    Assert.Contains($"{novel.Id}?Skip=6&Take=6", objetoRetorno.Proxima);
        //    Assert.Null(objetoRetorno.Anterior);
        //}

        //[Fact]
        //public void DeveRetornarVolumeNovel_ComLinksParaAnteriorEProximoNull_NaBuscaPorIdObra()
        //{
        //    // Arrange
        //    var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

        //    List<Guid> listaIdsVolumes =
        //    [
        //        Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
        //        Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
        //        Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
        //        Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
        //        Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
        //        Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
        //        Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
        //    ];

        //    List<Guid> listaIdsCapitulos =
        //    [
        //        Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
        //        Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
        //        Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
        //        Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
        //    ];

        //    var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
        //    Novel novel = volumeAppServiceFactory.GerarNovel(IdObra);
        //    List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

        //    var scheme = "https";
        //    var host = "localhost";
        //    var path = $"/mock/{novel.Id}?skip=6&take=6";
        //    var url = $"{scheme}://{host}/{path}";
        //    var httpContext = new HttpContextMock().SetupUrl(url);

        //    // Assert
        //    var retornoListaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

        //    listaVolumesNovel
        //        .ForEach(volumeNovel => retornoListaRetornoVolumeNovel
        //            .Add(_volumeNovelAppServiceMock
        //                .TrataRetornoVolumeNovel(volumeNovel)
        //            )
        //        );

        //    var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(httpContext, retornoListaRetornoVolumeNovel, 6, 6);

        //    Assert.Single(objetoRetorno.Data);
        //    Assert.Equal(7, objetoRetorno.Total);
        //    Assert.Null(objetoRetorno.Proxima);
        //    Assert.Contains($"{novel.Id}?Skip=0&Take=6", objetoRetorno.Anterior);
        //}

        [Fact]
        public void DeveRetornarFalse_QuandoVolumeNovelNaoEncontrado_NaBuscaPorIdObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsVolumes = [];
            List<Guid> listaIdsCapitulos = [];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Novel novel = volumeAppServiceFactory.GerarNovel(IdObra);
            List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Id}?skip=6&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

            listaVolumesNovel
                .ForEach(volumeNovel => retornoListaRetornoVolumeNovel
                    .Add(_volumeNovelAppServiceMock
                        .TrataRetornoVolumeNovel(volumeNovel)
                    )
                );

            var result = _volumeNovelAppServiceMock.ValidaExisteListaVolume(listaVolumesNovel);

            Assert.False(result.IsSuccess);
            Assert.Contains("Lista de volumes não encontrado para essa obra!", result.Errors[0].Message);
        }

        [Fact]
        public async Task DeveRetornarNull_QuandoObraNovelNaoEncontrada_NaBuscaPorIdObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Novel novel = null;
            List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

            // Mock
            _ObraServiceMock.Setup(x => x.RetornaNovelPorId(IdObra)).Returns(novel);

            // Assert
            var result = await _volumeNovelAppServiceMock.ObterVolumesNovelPorIdObra(IdObra);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ValueOrDefault);
            Assert.Contains("Obra não encontrada!", result.Errors[0].Message);
        }


        //[Fact]
        //public void DeveRetornarVolumeNovel_ComLinksParaProximoEAnterior_NaBuscaPorSlugObra()
        //{
        //    // Arrange
        //    var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
        //    var slugObra = "slug-novel-teste";

        //    List<Guid> listaIdsVolumes =
        //    [
        //        Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
        //        Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
        //        Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
        //        Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
        //        Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
        //        Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
        //        Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
        //    ];

        //    List<Guid> listaIdsCapitulos =
        //    [
        //        Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
        //        Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
        //        Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
        //        Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
        //    ];

        //    var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
        //    Novel novel = volumeAppServiceFactory.GerarNovel(IdObra, slugObra);
        //    List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

        //    var scheme = "https";
        //    var host = "localhost";
        //    var path = $"/mock/{novel.Slug}?skip=1";
        //    var url = $"{scheme}://{host}/{path}";
        //    var httpContext = new HttpContextMock().SetupUrl(url);

        //    // Assert
        //    var retornoListaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

        //    listaVolumesNovel
        //        .ForEach(volumeNovel => retornoListaRetornoVolumeNovel
        //            .Add(_volumeNovelAppServiceMock
        //                .TrataRetornoVolumeNovel(volumeNovel)
        //            )
        //        );

        //    var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(httpContext, retornoListaRetornoVolumeNovel, 1, null);

        //    Assert.Equal(6, objetoRetorno.Data.Count);
        //    Assert.Equal(7, objetoRetorno.Total);
        //    Assert.Contains($"{novel.Slug}?Skip=7&Take=6", objetoRetorno.Proxima);
        //    Assert.Contains($"{novel.Slug}?Skip=0&Take=6", objetoRetorno.Anterior);
        //}

        //[Fact]
        //public void DeveRetornarVolumeNovel_ComLinksParaProximoEAnteriorNull_NaBuscaPorSlugObra()
        //{
        //    // Arrange
        //    var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
        //    var slugObra = "slug-novel-teste";

        //    List<Guid> listaIdsVolumes =
        //    [
        //        Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
        //        Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
        //        Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
        //        Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
        //        Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
        //        Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
        //        Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
        //    ];

        //    List<Guid> listaIdsCapitulos =
        //    [
        //        Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
        //        Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
        //        Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
        //        Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
        //    ];

        //    var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
        //    Novel novel = volumeAppServiceFactory.GerarNovel(IdObra, slugObra);
        //    List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

        //    var scheme = "https";
        //    var host = "localhost";
        //    var path = $"/mock/{novel.Slug}?skip=0&take=6";
        //    var url = $"{scheme}://{host}/{path}";
        //    var httpContext = new HttpContextMock().SetupUrl(url);

        //    // Assert
        //    var retornoListaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

        //    listaVolumesNovel
        //        .ForEach(volumeNovel => retornoListaRetornoVolumeNovel
        //            .Add(_volumeNovelAppServiceMock
        //                .TrataRetornoVolumeNovel(volumeNovel)
        //            )
        //        );

        //    var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(httpContext, retornoListaRetornoVolumeNovel, 0, 6);

        //    Assert.Equal(slugObra, novel.Slug);
        //    Assert.Equal(6, objetoRetorno.Data.Count);
        //    Assert.Equal(7, objetoRetorno.Total);
        //    Assert.Contains($"{novel.Slug}?Skip=6&Take=6", objetoRetorno.Proxima);
        //    Assert.Null(objetoRetorno.Anterior);
        //}

        //[Fact]
        //public void DeveRetornarVolumeNovel_ComLinksParaAnteriorEProximoNull_NaBuscaPorSlugObra()
        //{
        //    // Arrange
        //    var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
        //    var slugObra = "slug-novel-teste";

        //    List<Guid> listaIdsVolumes =
        //    [
        //        Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
        //        Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
        //        Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
        //        Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
        //        Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
        //        Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
        //        Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
        //    ];

        //    List<Guid> listaIdsCapitulos =
        //    [
        //        Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
        //        Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
        //        Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
        //        Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
        //    ];

        //    var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
        //    Novel novel = volumeAppServiceFactory.GerarNovel(IdObra, slugObra);
        //    List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

        //    var scheme = "https";
        //    var host = "localhost";
        //    var path = $"/mock/{novel.Slug}?skip=6&take=6";
        //    var url = $"{scheme}://{host}/{path}";
        //    var httpContext = new HttpContextMock().SetupUrl(url);

        //    // Assert
        //    var retornoListaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

        //    listaVolumesNovel
        //        .ForEach(volumeNovel => retornoListaRetornoVolumeNovel
        //            .Add(_volumeNovelAppServiceMock
        //                .TrataRetornoVolumeNovel(volumeNovel)
        //            )
        //        );

        //    var objetoRetorno = RequestHelper.CriarObjetoRetonoVolumesNovels(httpContext, retornoListaRetornoVolumeNovel, 6, 6);

        //    Assert.Equal(slugObra, novel.Slug);
        //    Assert.Single(objetoRetorno.Data);
        //    Assert.Equal(7, objetoRetorno.Total);
        //    Assert.Contains($"{novel.Slug}?Skip=0&Take=6", objetoRetorno.Anterior);
        //    Assert.Null(objetoRetorno.Proxima);
        //}

        [Fact]
        public void DeveRetornarFalse_QuandoVolumeNovelNaoEncontrado_NaBuscaPorIdVolumeESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-novel-teste";

            List<Guid> listaIdsVolumes = [];
            List<Guid> listaIdsCapitulos = [];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Novel novel = volumeAppServiceFactory.GerarNovel(IdObra, slugObra);
            List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

            var scheme = "https";
            var host = "localhost";
            var path = $"/mock/{novel.Id}?skip=6&take=6";
            var url = $"{scheme}://{host}/{path}";
            var httpContext = new HttpContextMock().SetupUrl(url);

            // Assert
            var retornoListaRetornoVolumeNovel = new List<RetornoVolumeNovel>();

            listaVolumesNovel
                .ForEach(volumeNovel => retornoListaRetornoVolumeNovel
                    .Add(_volumeNovelAppServiceMock
                        .TrataRetornoVolumeNovel(volumeNovel)
                    )
                );

            var result = _volumeNovelAppServiceMock.ValidaExisteListaVolume(listaVolumesNovel);

            Assert.False(result.IsSuccess);
            Assert.Contains("Lista de volumes não encontrado para essa obra!", result.Errors[0].Message);
        }

        [Fact]
        public async Task DeveRetornarNull_QuandoObraNovelNaoEncontrada_NaBuscaPorIdVolumeESlugObra()
        {
            // Arrange
            var IdObra = Guid.Parse("00000000-1111-2222-3333-444444444444");
            var slugObra = "slug-novel-teste";

            List<Guid> listaIdsVolumes =
            [
                Guid.Parse("08dba651-ec33-4964-8f67-eecd4cbaea50"),
                Guid.Parse("08dd6104-d05d-49a9-8a66-62ca7b07acdc"),
                Guid.Parse("08dd6b39-c3d8-48a9-86fe-a2146c233c9f"),
                Guid.Parse("08dd6b39-cbd2-44a1-8847-30d8228716e5"),
                Guid.Parse("08dd6b3c-35f9-4f1d-829c-ca86d5f95cf2"),
                Guid.Parse("08dd6b3c-3c21-4e49-84ec-d3981a2e7f8c"),
                Guid.Parse("08dd6b3c-428f-485f-899f-dbad73d19023")
            ];

            List<Guid> listaIdsCapitulos =
            [
                Guid.Parse("0000000a-111b-222c-333d-44444444444e"),
                Guid.Parse("000000aa-11bb-22cc-33dd-4444444444ee"),
                Guid.Parse("00000aaa-1bbb-2ccc-3ddd-444444444eee"),
                Guid.Parse("0000aaaa-bbbb-cccc-dddd-44444444eeee")
            ];

            var volumeAppServiceFactory = new VolumeAppServiceFactoryTestes();
            Novel novel = null;
            List<VolumeNovel> listaVolumesNovel = volumeAppServiceFactory.GerarListaVolumeNovel(IdObra, listaIdsVolumes, listaIdsCapitulos);

            // Mock
            _ObraServiceMock.Setup(x => x.RetornaNovelPorSlug(slugObra)).Returns(novel);

            // Assert
            var result = await _volumeNovelAppServiceMock.ObterVolumesNovelPorIdObra(IdObra);

            Assert.False(result.IsSuccess);
            Assert.Null(result.ValueOrDefault);
            Assert.Contains("Obra não encontrada!", result.Errors[0].Message);
        }

        #endregion
    }
}