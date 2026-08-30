using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TsundokuTraducoes.Api.Extensions;
using TsundokuTraducoes.Data.Configuration;
using TsundokuTraducoes.Data.Context;
using TsundokuTraducoes.Helpers.Configuration;

// Carregar variáveis de ambiente do arquivo .env
DotNetEnv.Env.Load();

var _connectionStringConfig = new ConnectionStringConfig();
var _acessoExternoTinify = new AcessoExternoTinify();
var _acessoExternoAws = new AcessoExternoAws();
var _jwtConfiguration = new JwtConfiguration();

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

var CertificatePassword = builder.Configuration
    .GetSection("CertificateSettings")
    .GetValue<string>("Password");
var CertificatePath = builder.Configuration
    .GetSection("CertificateSettings")
    .GetValue<string>("Path");

_acessoExternoTinify.ApiKeyTinify = builder.Configuration.GetSection("ApiTinify").GetValue<string>("ApiKey");

_acessoExternoAws.AwsAccessKeyId = builder.Configuration.GetSection("ApiAws").GetValue<string>("AwsAccessKeyId");
_acessoExternoAws.AwsSecretAccessKey = builder.Configuration.GetSection("ApiAws").GetValue<string>("AwsSecretAccessKey");
_acessoExternoAws.BucketName = builder.Configuration.GetSection("ApiAws").GetValue<string>("BucketName");
_acessoExternoAws.DistributionId = builder.Configuration.GetSection("ApiAws").GetValue<string>("DistributionId");
_acessoExternoAws.DistributionDomainName = builder.Configuration.GetSection("ApiAws").GetValue<string>("DistributionDomainName");

_jwtConfiguration.SecretToken = builder.Configuration.GetSection("JwtConfiguration").GetValue<string>("SecretToken");

ConfigurationExternal.SetaAcessoExterno(_acessoExternoTinify, _acessoExternoAws);

_connectionStringConfig.ConnectionString = builder.Configuration.GetConnectionString("Default");
SourceConnection.SetaConnectionStringConfig(_connectionStringConfig);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(allowedOrigins ?? Array.Empty<string>())
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSqlConnection(_connectionStringConfig.ConnectionString!);
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(token =>
{
    token.RequireHttpsMetadata = false;
    token.SaveToken = true;
    token.MapInboundClaims = false;
    token.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = "roles",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtConfiguration.SecretToken)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };

    token.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var identity = context.Principal?.Identity as System.Security.Claims.ClaimsIdentity;
            if (identity != null)
            {
                var claimsToAdd = new List<System.Security.Claims.Claim>();
                var claimsToRemove = new List<System.Security.Claims.Claim>();

                foreach (var claim in identity.Claims.ToList())
                {
                    if (claim.Value.StartsWith("[") && claim.Value.EndsWith("]"))
                    {
                        try
                        {
                            var values = System.Text.Json.JsonSerializer.Deserialize<string[]>(claim.Value);
                            if (values != null)
                            {
                                claimsToRemove.Add(claim);
                                foreach (var value in values)
                                {
                                    claimsToAdd.Add(new System.Security.Claims.Claim(claim.Type, value));
                                }
                            }
                        }
                        catch { }
                    }
                }

                foreach (var claim in claimsToRemove)
                    identity.RemoveClaim(claim);
                foreach (var claim in claimsToAdd)
                    identity.AddClaim(claim);
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddControllers().AddNewtonsoftJson(
    option => option.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    if (builder.Environment.IsProduction() &&
        !string.IsNullOrWhiteSpace(CertificatePath) &&
        !string.IsNullOrWhiteSpace(CertificatePassword) &&
        File.Exists(CertificatePath))
    {
        options.ListenAnyIP(8080, listenOptions =>
        {
            listenOptions.UseHttps(CertificatePath, CertificatePassword);
        });
    }
    else
    {
        options.ListenAnyIP(8080);
    }
});

var app = builder.Build();
LoadConfiguration(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

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

static void LoadConfiguration(WebApplication app)
{
    var connectionStrings = new TsundokuTraducoes.Api.Configuration.ConnectionStrings();
    app.Configuration.GetSection("ConnectionStrings").Bind(connectionStrings);
    TsundokuTraducoes.Api.Configuration.ConnectionString = connectionStrings;
}

public partial class Program { }
