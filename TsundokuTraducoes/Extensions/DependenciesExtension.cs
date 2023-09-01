using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TsundokuTraducoes.Api.Data;
using TsundokuTraducoes.Api.Repository;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Services;
using TsundokuTraducoes.Api.Services.Interfaces;

namespace TsundokuTraducoes.Api.Extensions
{
    public static class DependenciesExtension
    {
        public static void AddSqlConnection(
        this IServiceCollection services,
        string connectionString)
        {
            services.AddDbContext<TsundokuContext>(
            context => context.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IObraRepository, ObraRepository>();
            services.AddTransient<IGeneroRepository, GeneroRepository>();
            services.AddTransient<IVolumeRepository, VolumeRepository>();
            services.AddTransient<ICapituloRepository, CapituloRepository>();
            services.AddTransient<IInfosObrasRepository, InfosObrasRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IObraService, ObraService>();
            services.AddTransient<IVolumeService, VolumeService>();
            services.AddTransient<ICapituloService, CapituloService>();
            services.AddTransient<IInfosObrasServices, InfosObrasService>();
            services.AddTransient<IImagemService, ImagemService>();
        }
    }
}