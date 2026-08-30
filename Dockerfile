# Build stage - usa SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /src

# Copiar arquivos de projeto
COPY ["TsundokuTraducoes/TsundokuTraducoes.Api.csproj", "TsundokuTraducoes/"]
COPY ["TsundokuTraducoes.Data/TsundokuTraducoes.Data.csproj", "TsundokuTraducoes.Data/"]
COPY ["TsundokuTraducoes.Domain/TsundokuTraducoes.Domain.csproj", "TsundokuTraducoes.Domain/"]
COPY ["TsundokuTraducoes.Entities/TsundokuTraducoes.Entities.csproj", "TsundokuTraducoes.Entities/"]
COPY ["TsundokuTraducoes.Helpers/TsundokuTraducoes.Helpers.csproj", "TsundokuTraducoes.Helpers/"]
COPY ["TsundokuTraducoes.Services/TsundokuTraducoes.Services.csproj", "TsundokuTraducoes.Services/"]

# Restaurar dependências
RUN dotnet restore "TsundokuTraducoes/TsundokuTraducoes.Api.csproj"

# Copiar código-fonte completo
COPY . .

# Compilar e publicar
WORKDIR /src/TsundokuTraducoes
RUN dotnet publish "TsundokuTraducoes.Api.csproj" \
    -c Release \
    -o /app/publish

# Runtime stage - apenas runtime para imagem menor
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

# Instalar curl para health checks
RUN apk add --no-cache curl

WORKDIR /app

# Copiar aplicação publicada do build
COPY --from=build /app/publish .

# Copiar .env se existir (para usar localmente ou em docker-compose)
COPY .env* ./

# Copiar certificados se existirem
COPY TsundokuTraducoes.Helpers/Certificados/ ./Certificados/

# Variáveis de ambiente padrão
ENV DOTNET_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Port padrão
EXPOSE 8080


# Entrypoint
ENTRYPOINT ["dotnet", "TsundokuTraducoes.Api.dll"]
