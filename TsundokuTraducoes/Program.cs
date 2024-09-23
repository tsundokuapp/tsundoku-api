using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using TsundokuTraducoes.Api;
using TsundokuTraducoes.Api.Extensions;
using TsundokuTraducoes.Data.Configuration;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Helpers.Configuration;


var _connectionStringConfig = new ConnectionStringConfig();
var _acessoExternoTinify = new AcessoExternoTinify();
var _acessoExternoAws = new AcessoExternoAws();

var builder = WebApplication.CreateBuilder(args);
_connectionStringConfig.ConnectionString = builder.Configuration.GetConnectionString("Default");
SourceConnection.SetaConnectionStringConfig(_connectionStringConfig);

_acessoExternoTinify.ApiKeyTinify = builder.Configuration.GetSection("ApiTinify").GetValue<string>("ApiKey");

_acessoExternoAws.AwsAccessKeyId = builder.Configuration.GetSection("ApiAws").GetValue<string>("AwsAccessKeyId");
_acessoExternoAws.AwsSecretAccessKey = builder.Configuration.GetSection("ApiAws").GetValue<string>("AwsSecretAccessKey");
_acessoExternoAws.BucketName = builder.Configuration.GetSection("ApiAws").GetValue<string>("BucketName");
_acessoExternoAws.DistributionId = builder.Configuration.GetSection("ApiAws").GetValue<string>("DistributionId");
_acessoExternoAws.DistributionDomainName = builder.Configuration.GetSection("ApiAws").GetValue<string>("DistributionDomainName");

ConfigurationExternal.SetaAcessoExterno(_acessoExternoTinify, _acessoExternoAws);

builder.Services.AddSqlConnection(_connectionStringConfig.ConnectionString);

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers().AddNewtonsoftJson(
    option => option.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
LoadConfiguration(app);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", () => "Você perdeu alguma coisa aqui? Volte agora de onde veio...");
    endpoints.MapGet("/api/", () => "Reveja suas decisões, você não deveria estar aqui...");
    endpoints.MapGet("/api/obras/", () => "Você ainda não reviu suas decisões...");
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ContextBase>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

void LoadConfiguration(WebApplication app)
{
    var connectionStrings = new Configuration.ConnectionStrings();
    app.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
    Configuration.ConnectionString = connectionStrings;
}

public partial class Program { }