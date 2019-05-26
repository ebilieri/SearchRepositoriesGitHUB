using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Octokit;
using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Services;
using SearchRepositoriesGitHUB.WebApp.Models;
using SearchRepositoriesGitHUB.WebApp.ServiceApi;
using System;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGitHubService _gitHubService;        
        private readonly IConfiguration _configuration;
        private readonly IGitHUBApi _gitHUBApi;

        public SearchController(IGitHubService gitHubService, IGitHUBApi gitHUBApi, IConfiguration confuguration)
        {
            _gitHubService = gitHubService;            
            _gitHUBApi = gitHUBApi;
            _configuration = confuguration;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region GitHub API Http
        public IActionResult HttpIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HttpIndex(string texto, bool c, bool java, bool javaScript, bool python, bool go)
        {
            string message;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                try
                {
                    var token = _configuration.GetSection("Token").Value;
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
                    var search = await _gitHUBApi.GetRepositories(uriEndpoint, token);

                    if (search != null)
                    {
                        foreach (var item in search.Repositorios)
                        {
                            _gitHubService.Salvar(item);
                        }

                        if (search.Repositorios.Count > 0)
                            return RedirectToAction("Repositorios", new { filtro = texto });
                        else
                            ModelState.AddModelError(string.Empty, "Nenhum repositório encontrado");
                    }
                }
                catch (Exception ex)
                {
                    message = string.Format("Atenção: {0}", ex.Message);
                    ModelState.AddModelError(string.Empty, message);
                }
            }

            return View();
        }

        #endregion



        #region GitHUB OctoKit DLL
        public IActionResult OctoKitIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OctoKitIndex(string texto, string linguagem)
        {
            string message;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                try
                {
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

                        if (result.Items.Count > 0)
                            return RedirectToAction("Repositorios", new { filtro = texto });
                        else
                            ModelState.AddModelError(string.Empty, "Nenhum repositório encontrado");
                    }
                }
                catch (Exception ex)
                {
                    message = string.Format("Atenção: {0}", ex.Message);
                    ModelState.AddModelError(string.Empty, message);
                }
            }

            return View();
        }

        #endregion


        public async Task<IActionResult> Repositorios(int currentPage = 1, string filtro = null)
        {
            SearchModel model = new SearchModel(_gitHubService);
            model.CurrentPage = currentPage;
            model.Filtro = filtro;

            await model.OnGetAsync(filtro);

            return View(model);
        }        
    }
}
