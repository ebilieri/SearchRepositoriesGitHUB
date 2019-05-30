# SearchRepositoriesGitHUB
Buscar Repositorios do GitHUB

Este projeto tem o objetivo de pesquisar repositórios do GitHUB e arzenar os dados em uma banco de dados local ou na nuvem

** Configurar os valores no arquivo appsettings.json para funcionar corretamente


{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GitHUBSearchDB;Trusted_Connection=True;MultipleActiveResultSets=true",
    "AzureConnection": "Server=server;Database=GitHUBDB;User Id=userod; Password=pass;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",

  "ApiConnections": {
    "DefautUrlSearch": "https://api.github.com/search/repositories?q={0}"
  },
  "User": {
    "Usuario": "user",
    "Senha": "pass"
  },
  "Token": "token",
  "ClientID": "cliente_d"
}

