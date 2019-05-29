using SearchRepositoriesGitHUB.Models;
using System.Threading.Tasks;

namespace SearchRepositoriesGitHUB.WebApp.ServiceApi
{
    public interface IGitHUBApi
    {
        Task<Search> GetRepositories(string uriEndPoint, string token);
        Task<int> SearchRepositories(string texto, bool c, bool java, bool javaScript, bool python, bool go);
        Task<int> SearchRepositoriesByOctoKit(string texto, string linguagem);
    }
}
