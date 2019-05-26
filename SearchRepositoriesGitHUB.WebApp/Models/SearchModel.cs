using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SearchRepositoriesGitHUB.Models;
using SearchRepositoriesGitHUB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.WebApp.Models
{
    public class SearchModel : PageModel
    {
        private readonly IGitHubService _gitHubService;       

        public SearchModel(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int Count { get; set; }

        public int PageSize { get; set; } = 10;

        public bool ShowPrevious => CurrentPage > 1;

        public bool ShowNext => CurrentPage < TotalPages;

        public bool ShowFirst => CurrentPage != 1;

        public bool ShowLast => CurrentPage != TotalPages;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public List<Repositorio> Data { get; set; }
        
        public string Filtro { get; set; }

        public async Task OnGetAsync(string filtro)
        {
            Data = await _gitHubService.GetPaginatedResult(filtro, CurrentPage, PageSize);
            Count = await _gitHubService.GetCount(filtro);
        }

        
    }
}
