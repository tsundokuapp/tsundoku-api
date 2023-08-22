using System;
using TsundokuTraducoes.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TsundokuTraducoes.Api.Services;
using TsundokuTraducoes.Api.Utilidades;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using TsundokuTraducoes.Api.Services.Interfaces;
using TsundokuTraducoes.Api.Repository.Interfaces;
using TsundokuTraducoes.Api.Repository;

namespace TsundokuTraducoes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TsundokuContext>(
                context => context.UseMySql(Constantes.StringDeConexao, ServerVersion.AutoDetect(Constantes.StringDeConexao))
            );
            services.AddScoped<IObraService, ObraService>();
            services.AddScoped<IVolumeService, VolumeService>();
            services.AddScoped<ICapituloService, CapituloService>();
            services.AddScoped<IInfosObrasServices, InfosObrasService>();
            services.AddScoped<IObraRepository, ObraRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IVolumeRepository, VolumeRepository>();
            services.AddScoped<ICapituloRepository, CapituloRepository>();
            services.AddScoped<IInfosObrasRepository, InfosObrasRepository>();
            services.AddControllers().AddNewtonsoftJson(
                option => option.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue;
            });
            services.AddCors();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("api/", () => "API no ar...");
            });
        }
    }
}
