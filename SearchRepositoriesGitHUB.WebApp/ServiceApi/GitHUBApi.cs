using Microsoft.Extensions.Configuration;
using Octokit;
using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.WebApp.ServiceApi
{
    public class GitHUBApi : IGitHUBApi
    {
        private readonly IConfiguration _configuration;
        private readonly IGitHubService _gitHubService;

        public GitHUBApi(IGitHubService gitHubService, IConfiguration configuration)
        {
            _configuration = configuration;
            _gitHubService = gitHubService;
        }

        public async Task<Search> GetRepositories(string uriEndPoint, string clientId)
        {
            Search search = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("SearchGitHub", "1.0"));                
                client.DefaultRequestHeaders.Add("client_id", clientId);                

                using (var response = await client.GetAsync(uriEndPoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        search = await response.Content.ReadAsAsync<Search>();
                    }
                    else
                    {
                        throw new Exception(response.ToString());
                    }
                }
            }

            return search;
        }

        public async Task<int> SearchRepositories(string texto, bool c, bool java, bool javaScript, bool python, bool go)
        {
            int repositorios = 0;
            var clientId = _configuration.GetSection("ClientID").Value;
            var uriEndpoint = string.Format(_configuration.GetSection("ApiConnections").GetSection("DefautUrlSearch").Value, texto);

            if (c)
                uriEndpoint = uriEndpoint + "+language:c#";
            if (java)
                uriEndpoint = uriEndpoint + "+language:java";
            if (javaScript)
                uriEndpoint = uriEndpoint + "+language:javascript";
            if (python)
                uriEndpoint = uriEndpoint + "+language:python";
            if (go)
                uriEndpoint = uriEndpoint + "+language:go";

            // execuar endpoint api github
            var search = await GetRepositories(uriEndpoint, clientId);

            if (search != null)
            {
                foreach (var item in search.Repositorios)
                {
                    _gitHubService.Salvar(item);
                }

                repositorios = search.Repositorios.Count;
            }

            return repositorios;
        }

        public async Task<int> SearchRepositoriesByOctoKit(string texto, string linguagem)
        {
            int repositorios = 0;
            var usuario = _configuration.GetSection("User").GetSection("Usuario").Value;
            var senha = _configuration.GetSection("User").GetSection("Senha").Value;

            var githubClient = new GitHubClient(new ProductHeaderValue("SearchRepositoriesGitHUB"));
            var basicAuth = new Credentials(usuario, senha);
            githubClient.Credentials = basicAuth;

            // Pesquisar repositgorios
            var request = new SearchRepositoriesRequest(texto);

            // linguagem
            if (!string.IsNullOrWhiteSpace(linguagem))
            {
                switch (linguagem.ToUpper())
                {
                    case "C":
                        {
                            request.Language = Language.CSharp;
                            break;
                        }
                    case "JAVA":
                        {
                            request.Language = Language.Java;
                            break;
                        }
                    case "JAVASCRIPT":
                        {
                            request.Language = Language.JavaScript;
                            break;
                        }
                    case "PYTHON":
                        {
                            request.Language = Language.Python;
                            break;
                        }
                    case "GO":
                        {
                            request.Language = Language.Go;
                            break;
                        }
                }
            }


            //request resultado
            var result = await githubClient.Search.SearchRepo(request);

            if (result != null)
            {
                foreach (var item in result.Items)
                {
                    var repositorio = new Repositorio();

                    repositorio.Id = item.Id;
                    repositorio.Name = item.Name;
                    repositorio.FullName = item.FullName;
                    repositorio.Description = item.Description;
                    repositorio.HtmlUrl = item.HtmlUrl;
                    repositorio.Language = item.Language;

                    _gitHubService.Salvar(repositorio);
                }

                repositorios = result.Items.Count;
            }

            return repositorios;
        }
    }
}
