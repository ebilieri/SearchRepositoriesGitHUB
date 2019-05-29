using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IGitHUBApi _gitHUBApi;

        public SearchController(IGitHubService gitHubService, IGitHUBApi gitHUBApi)
        {
            _gitHubService = gitHubService;
            _gitHUBApi = gitHUBApi;
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
                    int repositorios = await _gitHUBApi.SearchRepositories(texto, c, java, javaScript, python, go);

                    if (repositorios > 0)
                        return RedirectToAction("Repositorios", new { filtro = texto });
                    else
                        ModelState.AddModelError(string.Empty, "Nenhum repositório encontrado");
                }
                catch (Exception ex)
                {
                    message = string.Format("Atenção: {0}", ex.Message);
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Informe um valor para pesquisar");
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
                    int repositorios = await _gitHUBApi.SearchRepositoriesByOctoKit(texto, linguagem);

                    if (repositorios > 0)
                        return RedirectToAction("Repositorios", new { filtro = texto });
                    else
                        ModelState.AddModelError(string.Empty, "Nenhum repositório encontrado");
                }
                catch (Exception ex)
                {
                    message = string.Format("Atenção: {0}", ex.Message);
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Informe um valor para pesquisar");
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
