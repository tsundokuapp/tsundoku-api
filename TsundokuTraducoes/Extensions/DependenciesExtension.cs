using Microsoft.Extensions.DependencyInjection;
using TsundokuTraducoes.Api.Repository;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Data.Context.Interface;
using TsundokuTraducoes.Data.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Repositories;
using TsundokuTraducoes.Domain.Interfaces.Services;
using TsundokuTraducoes.Domain.Services;
using TsundokuTraducoes.Services.AppServices;
using TsundokuTraducoes.Services.AppServices.Interfaces;

namespace TsundokuTraducoes.Api.Extensions
{
    public static class DependenciesExtension
    {
        public static void AddSqlConnection(
        this IServiceCollection services,
        string stringConnection)
        {
            services.AddDbContextFactory<ContextBase>();
            services.AddScoped<IContextBase>(provider => provider.GetService<ContextBase>());
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IObraRepositoryOld, ObraRepositoryOld>();
            services.AddTransient<IGeneroRepositoryOld, GeneroRepositoryOld>();
            services.AddTransient<IVolumeRepositoryOld, VolumeRepositoryOld>();
            services.AddTransient<ICapituloRepositoryOld, CapituloRepositoryOld>();
            services.AddTransient<IInfosObrasRepositoryOld, InfosObrasRepositoryOld>();
            
            services.AddScoped<ICapituloRepository, CapituloRepository>();
            services.AddScoped<IGeneroDeParaRepository, GeneroDeParaRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IInfosObrasRepository, InfosObrasRepository>();
            services.AddScoped<IObraRepository, ObraRepository>();
            services.AddScoped<IVolumeRepository, VolumeRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IObraServiceOld, ObraServiceOld>();
            services.AddTransient<IVolumeServiceOld, VolumeServiceOld>();
            services.AddTransient<ICapituloServiceOld, CapituloServiceOld>();
            services.AddTransient<IInfosObrasServicesOld, InfosObrasServiceOld>();
            services.AddTransient<IImagemServiceOld, ImagemServiceOld>();
            services.AddTransient<IValidacaoTratamentoObrasServiceOld, ValidacaoTratamentoObrasServiceOld>();

            services.AddScoped<IGeneroDeParaAppService, GeneroDeParaAppService>();
            services.AddScoped<IImagemAppService, ImagemAppService>();
            services.AddScoped<IObraAppService, ObraAppService>();
            services.AddScoped<IVolumeAppService, VolumeAppService>();

            services.AddScoped<ICapituloService, CapituloService>();
            services.AddScoped<IGeneroDeParaService, GeneroDeParaService>();
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<IInfosObrasServices, InfosObrasServices>();
            services.AddScoped<IObraService, ObraService>();
            services.AddScoped<IVolumeService, VolumeService>();
        }
    }
}