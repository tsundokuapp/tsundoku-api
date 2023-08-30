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

- Para realizar os testes de crud é necessário usar o Postman (Ou algum app de requisição de seu interesse). Caso use o Postman, existe uma collection e variáveis de ambiente disponível no repósitorio.
Basta importar os arquivos jsons disponíveis e utilizar o mesmo.

## Iniciando o projeto

- Copie o arquivo appsettingsExample.json e altere seu nome para appsettings.json, adicionando a connectionString do banco local ou remoto.
- Adicionar diretórios wwwroot/image (_Enquanto salvar arquivos locais_)

- instalar o pacote dotnet-ef > ```dotnet tool install --global dotnet-ef``` (_Geralmente é necessário no VSCode_)
  - Rodar o comando ```update-database``` 
    - Visual Studio Code > ```dotnet ef database update```
    
- E em seguida rodar o projeto para subir a api
  - Visual Studio Code > ```dotnet run```