# Docker - Guia de Uso

Este projeto possui um Dockerfile e docker-compose.yml modernizados e otimizados para ASP.NET Core 8.0.

O projeto **aceita variáveis de ambiente (ENV)** em qualquer ambiente (dev, staging, prod).

## Pré-requisitos

- Docker e Docker Compose instalados
- Arquivo `.env` configurado (opcional - há um template em `.env.example`)

## Configuração Rápida

### Opção 1: Usar Docker Compose (Recomendado)

```bash
# 1. Copiar arquivo de exemplo
cp .env.example .env

# 2. Editar .env com suas configurações
# Altere valores como JWT_SECRET_TOKEN, ConnectionString, etc.

# 3. Executar
docker-compose up --build

# 4. Acessar em http://localhost:8080
```

### Opção 2: Usar Desenvolvimento Local com .env.local

Um arquivo `.env.local` já existe com configurações para desenvolvimento local:

```bash
# Instalar dependências
dotnet restore

# Executar aplicação (carrega .env.local automaticamente)
dotnet run
```

## Como as Variáveis de Ambiente Funcionam

O projeto carrega variáveis de ambiente nesta ordem (última tem prioridade):

1. **appsettings.json** - Valores padrão (versionado)
2. **.env.local** - Variáveis locais (development)
3. **Variáveis de Ambiente do Sistema** - Reais (production)
4. **docker-compose.yml** - Variáveis do compose (docker)

Isso significa que você pode:
- ✅ Usar valores padrão do `appsettings.json`
- ✅ Sobrescrever com `.env` ou `.env.local`
- ✅ Sobrescrever com variáveis do sistema
- ✅ Usar docker-compose.yml para produção

## Variáveis de Ambiente Importantes

### Banco de Dados
```
ConnectionStrings__Default="Server=localhost;Port=3306;Database=DbTsundoku;Uid=tsun;Pwd=1234;"
```

### JWT - Segurança
```
JwtConfiguration__SecretToken=""
```

### CORS - Origens permitidas
```
Cors__AllowedOrigins__0="http://localhost:3000"
Cors__AllowedOrigins__1="http://localhost:5173"
```

### AWS S3 (Opcional)
```
ApiAws__AwsAccessKeyId="sua-chave"
ApiAws__AwsSecretAccessKey="sua-secret"
ApiAws__BucketName="seu-bucket"
ApiAws__DistributionId="seu-id"
ApiAws__DistributionDomainName="seu-domain"
```

### Tinify - Compressão de Imagens (Opcional)
```
ApiTinify__ApiKey="sua-api-key"
```

### Certificados SSL (Opcional)
```
CertificateSettings__Path="TsundokuTraducoes.Helpers/Certificados/aspnetapp.pfx"
CertificateSettings__Password="sua-senha"
```

### Logging
```
Logging__LogLevel__Default="Information"
Logging__LogLevel__Microsoft="Warning"
```

### Ambiente de Execução
```
ASPNETCORE_ENVIRONMENT="Development"  # ou Production
```

## Estrutura do Docker Compose

### MySQL
- **Imagem**: mysql:8.0
- **Usuário**: `${MYSQL_USER:-tsun}`
- **Senha**: `${MYSQL_PASSWORD:-1234}`
- **Database**: `${MYSQL_DATABASE:-DbTsundoku}`
- **Port**: `${MYSQL_PORT:-3306}`

### API (ASP.NET Core)
- **Port**: `${API_PORT:-8080}`
- **Ambiente**: `${ASPNETCORE_ENVIRONMENT:-Production}`
- **Todas as variáveis** do arquivo `.env` são passadas para o container

## Comandos Úteis

### Iniciar tudo
```bash
docker-compose up --build
```

### Iniciar em background
```bash
docker-compose up -d --build
```

### Ver logs em tempo real
```bash
docker-compose logs -f api
docker-compose logs -f mysql
```

### Parar
```bash
docker-compose down
```

### Remover volumes (limpa banco de dados)
```bash
docker-compose down -v
```

### Reconstruir imagem
```bash
docker-compose build --no-cache
```

### Acessar shell do container
```bash
docker-compose exec api /bin/sh
```

### Acessar MySQL
```bash
docker-compose exec mysql mysql -utsun -p1234 DbTsundoku
```

## Build Manual

```bash
# Build da imagem
docker build -t tsundoku-api:latest .

# Executar container
docker run -p 8080:8080 \
  -e "ConnectionStrings__Default=Server=localhost;Port=3306;Database=DbTsundoku;Uid=tsun;Pwd=1234;" \
  -e "JwtConfiguration__SecretToken=sua-chave" \
  tsundoku-api:latest
```

## Para Desenvolvimento Local

Use o arquivo `.env.local` que já está configurado:

```bash
# Verificar que está lá
ls .env.local

# Editar se necessário
code .env.local

# Executar aplicação (carrega .env.local automaticamente)
cd TsundokuTraducoes
dotnet run
```

## Para Produção

### Com Docker Compose:

```bash
# 1. Criar um arquivo .env.production com valores seguros
cp .env.example .env.production

# 2. Editar com valores reais (mudar JWT_SECRET_TOKEN, passwords, etc.)
vi .env.production

# 3. Executar
export $(cat .env.production | grep -v '#' | xargs)
docker-compose -f docker-compose.yml up -d

# Ou usar docker-compose com arquivo específico:
docker-compose --env-file .env.production up -d
```

### Com Kubernetes:

```yaml
apiVersion: v1
kind: Secret
metadata:
  name: tsundoku-secrets
type: Opaque
stringData:
  ConnectionStrings__Default: "Server=mysql;Port=3306;Database=DbTsundoku;Uid=tsun;Pwd=SENHA_SEGURA;"
  JwtConfiguration__SecretToken: "CHAVE_SECRETA_MUITO_SEGURA"

---
apiVersion: apps/v1
kind: Deployment
metadata:
  name: tsundoku-api
spec:
  containers:
  - name: api
    image: tsundoku-api:latest
    envFrom:
    - secretRef:
        name: tsundoku-secrets
```

## Exemplo Completo de .env para Produção

```bash
# Banco de Dados - Production
ConnectionStrings__Default="Server=prod.db.server.com;Port=3306;Database=tsundoku_prod;Uid=prod_user;Pwd=PASSWORD_MUITO_SEGURA_12345;"

# JWT - Production
JwtConfiguration__SecretToken="CHAVE_SECRETA_ALEATORIA_SUPER_LONGA_E_SEGURA_12345678901234567890"

# CORS - Production
Cors__AllowedOrigins__0="https://seu-dominio.com"
Cors__AllowedOrigins__1="https://www.seu-dominio.com"

# AWS
ApiAws__AwsAccessKeyId="AKIAIOSFODNN7EXAMPLE"
ApiAws__AwsSecretAccessKey="wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY"
ApiAws__BucketName="seu-bucket-prod"
ApiAws__DistributionId="E123EXAMPLE456"
ApiAws__DistributionDomainName="https://d123example456.cloudfront.net/"

# Tinify
ApiTinify__ApiKey="l3kjtQV97Qq3Rp0wqQHpx3QFYld4qTBc"

# Certificado SSL
CertificateSettings__Path="/app/certificados/cert.pfx"
CertificateSettings__Password="SENHA_CERTIFICADO_SEGURA"

# Ambiente
ASPNETCORE_ENVIRONMENT="Production"
```

## Troubleshooting

### "Cannot find .env file"
Use `.env.example` como template ou deixe vazio - o projeto funciona com appsettings.json como fallback.

### "Connection refused"
Verifique:
1. MySQL está rodando: `docker-compose ps`
2. ConnectionString usa hostname correto (mysql no docker, localhost local)
3. Credenciais estão corretas

### "Invalid JWT"
Certifique-se que `JwtConfiguration__SecretToken` é igual em todas as instâncias (backend e frontend).

### Secrets expostos accidentalmente
Se commitar um `.env` com secrets:
```bash
git rm --cached .env
echo ".env" >> .gitignore
git commit -m "Remove .env file"
```

### Mudar configuration sem rebuild
```bash
# Editar .env
vi .env

# Reiniciar apenas o container da API
docker-compose restart api
```

## Deploy com CI/CD

### GitHub Actions

```yaml
name: Deploy

on:
  push:
    branches: [main]

env:
  ConnectionStrings__Default: ${{ secrets.DB_CONNECTION_STRING }}
  JwtConfiguration__SecretToken: ${{ secrets.JWT_SECRET }}

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Deploy
        run: docker-compose up -d
```