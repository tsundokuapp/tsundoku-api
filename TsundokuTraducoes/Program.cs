using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Net;
using System.Text;
using TsundokuTraducoes.Api;
using TsundokuTraducoes.Api.Extensions;
using TsundokuTraducoes.Api.Helpers;
using TsundokuTraducoes.Data.Configuration;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Helpers.Configuration;

var _connectionStringConfig = new ConnectionStringConfig();
var _acessoExternoTinify = new AcessoExternoTinify();
var _acessoExternoAws = new AcessoExternoAws();
var _jwtConfiguration = new JwtConfiguration();

var builder = WebApplication.CreateBuilder(args);
_connectionStringConfig.ConnectionString = builder.Configuration.GetConnectionString("Default");
SourceConnection.SetaConnectionStringConfig(_connectionStringConfig);

_acessoExternoTinify.ApiKeyTinify = builder.Configuration.GetSection("ApiTinify").GetValue<string>("ApiKey");

_acessoExternoAws.AwsAccessKeyId = builder.Configuration.GetSection("ApiAws").GetValue<string>("AwsAccessKeyId");
_acessoExternoAws.AwsSecretAccessKey = builder.Configuration.GetSection("ApiAws").GetValue<string>("AwsSecretAccessKey");
_acessoExternoAws.BucketName = builder.Configuration.GetSection("ApiAws").GetValue<string>("BucketName");
_acessoExternoAws.DistributionId = builder.Configuration.GetSection("ApiAws").GetValue<string>("DistributionId");
_acessoExternoAws.DistributionDomainName = builder.Configuration.GetSection("ApiAws").GetValue<string>("DistributionDomainName");

_jwtConfiguration.SecretToken = builder.Configuration.GetSection("JwtConfiguration").GetValue<string>("SecretToken");

ConfigurationExternal.SetaAcessoExterno(_acessoExternoTinify, _acessoExternoAws);

builder.Services.AddSqlConnection(_connectionStringConfig.ConnectionString);

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers()
    .AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling =
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

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("tsundokuApp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();

//definindo configurações de autenticação
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", token =>
{
    token.RequireHttpsMetadata = false;
    token.SaveToken = true;
    token.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        //Mesma chave feita na classe Token Service
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretToken)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
    token.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            // Ignora a resposta padrão do middleware
            context.HandleResponse();

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Message = "Falha na autenticação",
                Details = "Token de acesso inválido ou ausente."
            };

            await context.Response.WriteAsJsonAsync(response);
        },
        OnForbidden = async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = "Acesso Negado",
                Details = "Você não possui permissão para acessar este recurso."
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Tsundoku", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = $"Insira o token de autenticação JWT no formato: Bearer \"12345abcdef\". "
    });   
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();
LoadConfiguration(app);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger/index.html"));
app.MapGet("/api/", () => Results.Redirect("/swagger/index.html"));
app.MapGet("/api/obras/", () => Results.Redirect("/swagger/index.html"));

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ContextBase>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

void LoadConfiguration(WebApplication app)
{
    var connectionStrings = new Configuration.ConnectionStrings();
    app.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
    Configuration.ConnectionString = connectionStrings;
}

app.Run();

public partial class Program { }