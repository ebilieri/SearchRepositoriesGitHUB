using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;
using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Services;
using SearchRepositoriesGitHUB.WebApp.Models;
using SearchRepositoriesGitHUB.WebApp.ServiceApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IItemsService _itemsService;
        private readonly IConfiguration _configuration;
        private readonly IGitHUBApi _gitHUBApi;

        public SearchController(IItemsService itemsService, IGitHUBApi gitHUBApi, IConfiguration confuguration)
        {
            _itemsService = itemsService;
            _gitHUBApi = gitHUBApi;
            _configuration = confuguration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string texto, bool c, bool java, bool javaScript, bool python, bool go)
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
                        foreach (var item in search.Items)
                        {
                            _itemsService.Salvar(item);
                        }

                        if (search.Items.Count > 0)
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


        public async Task<IActionResult> Repositorios(int currentPage = 1, string filtro = null)
        {
            SearchModel model = new SearchModel(_itemsService);
            model.CurrentPage = currentPage;
            model.Filtro = filtro;

            await model.OnGetAsync(filtro);

            return View(model);
        }
    }
}
