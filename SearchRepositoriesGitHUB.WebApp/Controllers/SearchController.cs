using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Services;
using SearchRepositoriesGitHUB.WebApp.ServiceApi;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Index(string texto)
        {
            var listTeams = new List<Item>();
            string message;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                try
                {
                    var uriEndpoint = string.Format(_configuration.GetSection("ApiConnections").GetSection("DefautUrlSearch").Value, texto);

                    var search = await _gitHUBApi.GetRepositories(uriEndpoint);

                    if (search != null)
                    {
                        //ViewBag.Campeonato = football.Competition.Name;
                        // ViewBag.CampeonatoId = football.Competition.Id;
                        listTeams = search.Items;
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


        
    }
}
