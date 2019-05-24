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
                var token = "b092dfb58ba906ddc8608b8ed3f6cd7e0466dae0";

                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

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
    }
}
