using AutoMapper;
using Moq;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Entities.Entities.Capitulo;
using TsundokuTraducoes.Helpers.DTOs.Admin.Retorno;
using TsundokuTraducoes.Helpers.DTOs.Public.Retorno;
using TsundokuTraducoes.Services.AppServices;
using TsundokuTraducoes.Services.AppServices.Interfaces;
using TsundokuTraducoes.Services.Profiles;

namespace TsundokuTraducoes.TestsServices.AppServices;

public class VolumeAppServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IVolumeService> _volumeServiceMock;
    private readonly Mock<IObraService> _obraServiceMock;
    private readonly Mock<IImagemAppService> _imagemAppServiceMock;

    private readonly VolumeAppService _volumeAppService;

    public VolumeAppServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new VolumeProfile());
            cfg.AddProfile(new CapituloProfile());
            cfg.AddProfile(new ObraProfile());
            cfg.AddProfile(new GeneroProfile());
        });

        _mapper = config.CreateMapper();
        _volumeServiceMock = new Mock<IVolumeService>();
        _obraServiceMock = new Mock<IObraService>();
        _imagemAppServiceMock = new Mock<IImagemAppService>();

        _volumeAppService = new VolumeAppService(
            _mapper,
            _volumeServiceMock.Object,
            _obraServiceMock.Object,
            _imagemAppServiceMock.Object
        );
    }

    [Fact]
    public void TrataRetornoVolumeNovel_ListaCapitulo_DeveRetornarObjetoMapeadoCorretamente()
    {
        // Arrange
        var appServicesFactory = new AppServicesFactory();
        var idVolumeCriado = Guid.NewGuid();
        var idCapituloCriado = Guid.NewGuid();
        var volumeNovel = appServicesFactory.GerarVolumeNovel(idVolumeCriado);

        volumeNovel.DataInclusao = new DateTime(2024, 1, 1, 10, 30, 0);
        volumeNovel.DataAlteracao = new DateTime(2024, 1, 2, 14, 45, 0);
        volumeNovel.UsuarioAlteracao = "UsuarioTeste";
        volumeNovel.UsuarioInclusao = "UsuarioTeste";
        volumeNovel.ListaCapitulo = new List<CapituloNovel>
            {
                appServicesFactory.GerarCapituloNovel(idCapituloCriado, idVolumeCriado)
            };


        var retornoEsperado = new RetornoVolume()
        {
            DataInclusao = "01/01/2024 10:30:00",
            DataAlteracao = "02/01/2024 14:45:00",
            UsuarioAlteracao = "UsuarioTeste",
            UsuarioInclusao = "UsuarioTeste",
            ListaCapitulo = new List<RetornoCapituloNoVolume>
                {
                    new RetornoCapituloNoVolume()
                    {
                        Id = idCapituloCriado,
                        VolumeId = idVolumeCriado,
                        Titulo = "Capítulo 1",
                        Numero = "1",
                        Parte = "1",
                        OrdemCapitulo = 1,
                        Slug = "capitulo-1",
                        DataInclusao = new DateTime(2024, 1, 1, 10, 30, 0),
                        Publicado = true,
                    }
                }
        };

        // Act
        var retorno = _volumeAppService.TrataRetornoVolumeNovel(volumeNovel);
        var numeroDeCamposDaListaCapitulo = retorno.ListaCapitulo.First().GetType().GetProperties().Length;

        // Assert
        Assert.NotNull(retorno);
        Assert.Equal(retornoEsperado.DataInclusao, retorno.DataInclusao);
        Assert.Equal(retornoEsperado.DataAlteracao, retorno.DataAlteracao);
        Assert.Equal(retornoEsperado.UsuarioAlteracao, retorno.UsuarioAlteracao);
        Assert.Equal(retornoEsperado.ListaCapitulo.Count, retorno.ListaCapitulo.Count);
        Assert.Equal(9, numeroDeCamposDaListaCapitulo);

        Assert.Equal(retornoEsperado.ListaCapitulo.First().Id, retorno.ListaCapitulo.First().Id);
        Assert.Equal(retornoEsperado.ListaCapitulo.First().VolumeId, retorno.ListaCapitulo.First().VolumeId);
        Assert.Equal(retornoEsperado.ListaCapitulo.First().Titulo, retorno.ListaCapitulo.First().Titulo);
        Assert.Equal(retornoEsperado.ListaCapitulo.First().Numero, retorno.ListaCapitulo.First().Numero);
        Assert.Equal(retornoEsperado.ListaCapitulo.First().Parte, retorno.ListaCapitulo.First().Parte);
        Assert.Equal(retornoEsperado.ListaCapitulo.First().OrdemCapitulo, retorno.ListaCapitulo.First().OrdemCapitulo);
        Assert.Equal(retornoEsperado.ListaCapitulo.First().Slug, retorno.ListaCapitulo.First().Slug);
        Assert.Equal(retornoEsperado.ListaCapitulo.First().DataInclusao, retorno.ListaCapitulo.First().DataInclusao);
        Assert.Equal(retornoEsperado.ListaCapitulo.First().Publicado, retorno.ListaCapitulo.First().Publicado);
    }
}