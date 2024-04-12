using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TsundokuTraducoes.Api;
using TsundokuTraducoes.Api.Extensions;
using TsundokuTraducoes.Data.Configuration;
using TsundokuTraducoes.Helpers.Configuration;


var _connectionStringConfig = new ConnectionStringConfig();
var _acessoExterno = new AcessoExterno();

var builder = WebApplication.CreateBuilder(args);
_connectionStringConfig.ConnectionString = builder.Configuration.GetConnectionString("Default");
SourceConnection.SetaConnectionStringConfig(_connectionStringConfig);

_acessoExterno.ApiKeyTinify = builder.Configuration.GetSection("ApiTinify").GetValue<string>("ApiKey");
ConfigurationExternal.SetaAcessoExterno(_acessoExterno);

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
    endpoints.MapGet("/", () => "Você perdeu alguma coisa aqui? Volte agora de onde veio...");
    endpoints.MapGet("/api/", () => "Reveja suas decisões, você não deveria estar aqui...");
    endpoints.MapGet("/api/obras/", () => "Você ainda não reviu suas decisões...");
});

app.Run();

void LoadConfiguration(WebApplication app)
{
    var connectionStrings = new Configuration.ConnectionStrings();
    app.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
    Configuration.ConnectionString = connectionStrings;
}

public partial class Program { }