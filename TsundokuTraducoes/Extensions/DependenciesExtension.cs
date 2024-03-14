﻿using Microsoft.Extensions.DependencyInjection;
using TsundokuTraducoes.Data.Context;
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
            services.AddDbContext<ContextBase>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICapituloRepository, CapituloRepository>();
            services.AddScoped<IGeneroDeParaRepository, GeneroDeParaRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IObrasRepository, ObrasRepository>();
            services.AddScoped<IObraRepository, ObraRepository>();
            services.AddScoped<IVolumeRepository, VolumeRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGeneroDeParaAppService, GeneroDeParaAppService>();
            services.AddScoped<IImagemAppService, ImagemAppService>();
            services.AddScoped<IObraAppService, ObraAppService>();
            services.AddScoped<IObrasAppService, ObrasAppService>();
            services.AddScoped<IVolumeAppService, VolumeAppService>();
            services.AddScoped<ICapituloAppService, CapituloAppService>();

            services.AddScoped<ICapituloService, CapituloService>();
            services.AddScoped<IGeneroDeParaService, GeneroDeParaService>();
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<IObrasService, ObrasServices>();
            services.AddScoped<IObraService, ObraService>();
            services.AddScoped<IVolumeService, VolumeService>();
        }
    }
}