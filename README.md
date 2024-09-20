## Tsundoku Traduções V2 - API
Tsundoku é um site voltado para leitura de Light Novels e Mangás, que já está ativo no link [Tsundoku](https://tsundoku.com.br/).
Este repo é a atualização backend do site para uma estrutura mais moderna e única com objetivos de solucionar problemas e limitações enfrentados no site atual.

## Contribuindo

A Tsundoku adoraria sua ajuda, apesar do esforço, o projeto é grande e muitos pontos ainda estão por serem decididos. Toda a ajuda é bem vinda e caso se interesse, segue alguns passos necessários para a contribuição:

### Primeiros passos 

- Fazer um fork do projeto
- Clonar o projeto em sua máquina 
- Rodar o comando "git pull" para se certificar das alterações

## Orientações sobre Pull Requests

Tente seguir essas orientações o tanto quanto possível para minimizar o trabalho de todos.

**1.** Antes de iniciar o processo de contribuição,  **crie uma nova branch**  para fazer suas alterações.

Alguns exemplos:
-   Para novas features:  `git checkout -b feat/tabela-usuarios`
-   Para correções:  `git checkout -b fix/campo-retornando-null`

**2.**  Após realizar as alterações, é hora de fazer um commit com uma mensagem coerente do que foi feito. Exemplo:
```
git add --all
git commit -am ‘fix(usuario): adiciona campo id externo na tabela user’
git push origin fix/usuario
```
o exemplo segue o padrão do Conventional Commits, se não estiver habituado com Conventional Commits, não se preocupe, basta que a mensagem seja clara e direta.
 Leia sobre em: [Conventional Commits](https://www.conventionalcommits.org/pt-br/v1.0.0/)

**3.**  Envie um  _Pull Request_  com as alterações feitas, fazendo referência para o  `main`  do repositório oficial.

**4.**  Sua contribuição será analisada. Em alguns casos pediremos algumas alterações antes de dar merge.

Após o merge:
-   Delete a branch utilizada:
```
git checkout main
git push origin --delete <nome_da_branch>
git branch -D <nome_da_branch>
```
-   Atualize seu repositório com o repositório oficial:
```
git fetch upstream
git rebase upstream/main
git push -f origin main
```
**5.**  Quando iniciar uma nova contribuição, repita o processo do inicio, criando uma nova branch.


## Lembrente

- Evite mexer em arquivos desconexos dentro de um mesmo commit
- Não altere versões de libs ou ferramentas.
- Se precisar de ajuda, pergunte diretamente informando o problema que está tendo.


## Postman

- Para realizar os testes de crud é necessário usar o Postman (Ou algum app de requisição de seu interesse). Caso use o Postman, existe uma collection e variáveis de ambiente disponível.
  - Diretório das collections no Drive: [Collections](https://drive.google.com/drive/folders/1bhmK9wYH26zVlEMj0mxudu7KL6fvR8-t?usp=sharing)
- Basta importar, no Postam, o arquivo json disponível e utilizar.


## Iniciando o projeto (ambiente windows)

- Copie o arquivo appsettingsExample.json e altere seu nome para appsettings.json, adicionando a connectionString do banco local ou remoto.
- Adicionar diretórios wwwroot/image (_Enquanto salvar arquivos locais_)

- instalar o pacote dotnet-ef > ```dotnet tool install --global dotnet-ef``` (_Geralmente é necessário no VSCode_)
  - Rodar o comando ```update-database``` 
    - Visual Studio Code > ```dotnet ef database update```
    
- E em seguida rodar o projeto para subir a api
  - Visual Studio Code > ```dotnet run```

## Iniciando o projeto (ambiente docker)

Comandos docker:

- Após realizar o clone dentro do ambiente Docker, acessar a seguinte pasta:
  - cd tsundoku-api/

- Criando uma rede para comunicação entre os containers
  - ```docker network create --driver bridge tsundokuapi-bridge```

- Subindo banco MySql 
Criando um container MySql com a nova rede
Lembrando que esse nome vai no arquivo appconfig que está no Drive
  - ```docker run --name=mysql -e MYSQL_ROOT_PASSWORD=1234 -d --network tsundokuapi-bridge mysql:5.6```

- O certificado já está no projeto, caso precise gerar um novo, procedimentos abaixo:
Gerar certificados locais para poder liberar porta https
Ambiente windows
  - ```dotnet dev-certs https -ep C:\Users\edsra\.aspnet\https\aspnetapp.pfx -p tsundokuapi```
    - Onde "C:\Users\edsra\" é a pasta raíz da máquina.
    - Onde "-p tsundokuapi" senha do certificado

  - ```dotnet dev-certs https --trust```

- Outros ambientes pode ser consultado no artigo [aqui](https://learn.microsoft.com/pt-br/aspnet/core/security/docker-https?view=aspnetcore-8.0).

- Buildar imagem
  - ```docker build -t tsundokuapi:1.0 -f Dockerfile .```
     - Onde "tsundokuapi:1.0" seria o nome da imagem e versão

- criando um container tsundokuapi com a nova rede
```docker run --rm -it -p 8080:80 -p 8081:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORTS=8081 -e ASPNETCORE_Kestrel__Certificates__Default__Password="tsundokuapi" -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Path=/app/certificados/aspnetapp.pfx -v \TsundokuTraducoes\.aspnet\https:/https/ --name tsundoku-api --network tsundokuapi-bridge tsundokuapi:1.0```

- Acessar BD 
   - Usar id do container do banco de dados
      - ```docker exec -it id_imagem bash```

   - Onde -p1234 seria a senha do banco já definida para acessar
      - ```mysql -u root -p1234```

  - Lista as tabelas
     - ```show databases;```

  - Seta a tabela para poder realizar as consultas sql
    - ```use DbTsundoku;```

  - Lista as tabelas
    - ```show tables;```

  - Consulta de tabelas
    - ```select * from Novels;```

## Testes Unitários (Entidades)

**1º** Passo

- Chegar ou estar na raiz do projeto
  - Basicamente acessar a pasta principal que foi clonada

**2º** Passo

- Chegar até a pasta do projeto
  - Exemplo de comando: ```cd TsundokuTraducoes.Entities.Tests``` 

**3º** Passo

- Estando na pasta do projeto executar o comando de teste
  - Exemplo de comando: ```dotnet test```
 

## Testes de Integração (Cruds e afins)

**1º** Passo

- Chegar ou estar na raiz do projeto
  - Basicamente acessar a pasta principal que foi clonada

**2º** Passo

- Chegar até a pasta do projeto
  - Exemplo de comando: ```cd TsundokuTraducoes.Integration.Tests```
- Adicionar o arquivo App.Config no projeto para adicionar a ApiKey da api para otimizar as imagens

**3º** Passo

- Estando na pasta do projeto executar o comando de teste
  - Exemplo de comando: ```dotnet test```
 
## Observações sobre os testes

- Até o momento eu não achei informações de rodar os testes de forma separada via comando.
- Para mais informações, consultar a documentação do pacote: [Xunit.Net](https://xunit.net/docs/getting-started/netcore/cmdline)
