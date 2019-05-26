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
        public async Task<Search> GetRepositories(string uriEndPoint, string clientId)
        {
            Search search = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("SearchGitHub", "1.0"));
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("client_id", clientId);

                // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

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
