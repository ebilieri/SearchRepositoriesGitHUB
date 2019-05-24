using SearchRepositoriesGitHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.WebApp.ServiceApi
{
    public class GitHUBApi : IGitHUBApi
    {
        public async Task<Search> GetRepositories(string uriEndPoint)
        {
            Search search = null;
            using (var client = new HttpClient())
            {                
                using (var response = await client.GetAsync(uriEndPoint))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        search = await response.Content.ReadAsAsync<Search>();
                    }
                    else
                    {
                        //var msgRetorno = await response.Content.ReadAsAsync<Search>();
                        throw new Exception("Nenhum repositório localizado");
                    }
                }
            }

            return search;
        }
    }
}
