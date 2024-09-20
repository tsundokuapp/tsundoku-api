# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/TsundokuTraducoes/TsundokuTraducoes.Api.csproj", "TsundokuTraducoes/"]
COPY ["/TsundokuTraducoes.Data/TsundokuTraducoes.Data.csproj", "TsundokuTraducoes.Data/"]
COPY ["/TsundokuTraducoes.Domain/TsundokuTraducoes.Domain.csproj", "TsundokuTraducoes.Domain/"]
COPY ["/TsundokuTraducoes.Entities/TsundokuTraducoes.Entities.csproj", "TsundokuTraducoes.Entities/"]
COPY ["/TsundokuTraducoes.Helpers/TsundokuTraducoes.Helpers.csproj", "TsundokuTraducoes.Helpers/"]
COPY ["/TsundokuTraducoes.Services/TsundokuTraducoes.Services.csproj", "TsundokuTraducoes.Services/"]
# COPY ["/TsundokuTraducoes.Entities.Tests/TsundokuTraducoes.Entities.Tests.csproj", "TsundokuTraducoes.Entities.Tests/"]
# COPY ["/TsundokuTraducoes.Integration.Tests/TsundokuTraducoes.Integration.Tests.csproj", "TsundokuTraducoes.Integration.Tests/"]

RUN dotnet restore "./TsundokuTraducoes/TsundokuTraducoes.Api.csproj"
COPY . .

WORKDIR "/src/TsundokuTraducoes"
RUN dotnet build "./TsundokuTraducoes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TsundokuTraducoes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certificados
COPY TsundokuTraducoes.Helpers/Certificados/aspnetapp.pfx /app/certificados

ENTRYPOINT [ "dotnet", "TsundokuTraducoes.Api.dll" ]