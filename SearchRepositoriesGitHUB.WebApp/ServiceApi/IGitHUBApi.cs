using SearchRepositoriesGitHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.WebApp.ServiceApi
{
    public interface IGitHUBApi
    {
        Task<Search> GetRepositories(string uriEndPoint);
    }
}
