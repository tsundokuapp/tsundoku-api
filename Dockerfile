FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . ./ 
COPY ["/TsundokuTraducoes/TsundokuTraducoes.Api.csproj", "TsundokuTraducoes/"]
COPY ["/TsundokuTraducoes.Data/TsundokuTraducoes.Data.csproj", "TsundokuTraducoes.Data/"]
COPY ["/TsundokuTraducoes.Domain/TsundokuTraducoes.Domain.csproj", "TsundokuTraducoes.Domain/"]
COPY ["/TsundokuTraducoes.Entities/TsundokuTraducoes.Entities.csproj", "TsundokuTraducoes.Entities/"]
COPY ["/TsundokuTraducoes.Helpers/TsundokuTraducoes.Helpers.csproj", "TsundokuTraducoes.Helpers/"]
COPY ["/TsundokuTraducoes.Services/TsundokuTraducoes.Services.csproj", "TsundokuTraducoes.Services/"]

COPY . ./

RUN dotnet build "/src/TsundokuTraducoes/TsundokuTraducoes.Api.csproj" -c Release -o /app
RUN dotnet build "/src/TsundokuTraducoes.Data/TsundokuTraducoes.Data.csproj" -c Release -o /app
RUN dotnet build "/src/TsundokuTraducoes.Domain/TsundokuTraducoes.Domain.csproj" -c Release -o /app
RUN dotnet build "/src/TsundokuTraducoes.Entities/TsundokuTraducoes.Entities.csproj" -c Release -o /app
RUN dotnet build "/src/TsundokuTraducoes.Helpers/TsundokuTraducoes.Helpers.csproj" -c Release -o /app
RUN dotnet build "/src/TsundokuTraducoes.Services/TsundokuTraducoes.Services.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "/src/TsundokuTraducoes/TsundokuTraducoes.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TsundokuTraducoes.Api.dll"]